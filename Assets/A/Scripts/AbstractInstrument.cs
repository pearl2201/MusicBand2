using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MidiSheetMusic;
using UnityEngine;
using Spine.Unity;
namespace Assets.A.Scripts
{
    public abstract class AbstractInstrument : MonoBehaviour, InstrumentEventHandler
    {

        public Type_Instrument typeInstrument;

        public MusicBandManager manager;

        public MidiTrack trackMidi;

        public AudioSource audioSource;

        private AudioClip audioClip;

        public bool isPlaying;

        private int nextNoteIndex;
        private int nextNoteImpulse;
        public SkeletonAnimation ske;
        [SerializeField]
        protected tk2dSprite btnActiveState;
        public void Init(MidiTrack _midiTrack, AudioClip _audioClip)
        {

            this.trackMidi = _midiTrack;

            this.audioClip = _audioClip;

            audioSource.clip = audioClip;
            btnActiveState.SetSprite("instrument_off");
            ResetNextImpulse();
        }

        public void Click()
        {
            if (isPlaying)
            {
                OnRemoveInstrumentEvent();
            }
            else
            {
                OnPickUpInstrumentEvent();
            }
        }

        public void OnPickUpInstrumentEvent()
        {
            if (!isPlaying)
            {
                manager.OnPickUpInstrument(this);
            }

        }

        public void OnRemoveInstrumentEvent()
        {
            if (isPlaying)
                manager.OnRemoveInstrument(this);
        }




        public void CheckTime(int lastImpulse, int currImpulse)
        {
            if (lastImpulse == 0 && nextNoteIndex != 0)
            {
                nextNoteIndex = 0;
                nextNoteImpulse = trackMidi.Notes[0].StartTime;
            }
            if (currImpulse < nextNoteImpulse)
            {
                return;
            }
            if (lastImpulse != 0)
            {
                List<MidiNote> listNotePlay = new List<MidiNote>();
                float duration = 0;
                int startTime = 0;
                float minDuration = 10000;
                for (int i = nextNoteIndex; i < trackMidi.Notes.Count; i++)
                {

                    if (trackMidi.Notes[i].StartTime > lastImpulse && trackMidi.Notes[i].StartTime <= currImpulse)
                    {
                        listNotePlay.Add(trackMidi.Notes[i]);
                        minDuration = trackMidi.Notes[i].Duration > minDuration ? minDuration : trackMidi.Notes[i].Duration;
                        duration = trackMidi.Notes[i].Duration > duration ? trackMidi.Notes[i].Duration : duration;
                        startTime = trackMidi.Notes[i].StartTime;
                    }
                    else if (trackMidi.Notes[i].StartTime > currImpulse)
                    {
                        nextNoteIndex = i;
                        nextNoteImpulse = trackMidi.Notes[i].StartTime;
                        break;
                    }
                }
                if (listNotePlay.Count > 0)
                {
                    //  Debug.Log("currImpulse: " + currImpulse);
                    if (minDuration != duration || listNotePlay.Count == 1)
                    {
                        OnNoteOn(listNotePlay.ToArray(), duration);
                    }
                    else
                    {
                        if (nextNoteImpulse <= startTime)
                        {
                            OnNoteOn(listNotePlay.ToArray(), duration * listNotePlay.Count);
                        }
                        else if (startTime + duration * listNotePlay.Count <= nextNoteImpulse)
                        {
                            OnNoteOn(listNotePlay.ToArray(), duration * listNotePlay.Count);
                        }
                        else
                        {
                            duration = nextNoteImpulse - (startTime);
                            OnNoteOn(listNotePlay.ToArray(), duration);
                        }

                    }
                }
            }
            else
            {
                List<MidiNote> listNotePlay = new List<MidiNote>();
                float duration = 0;
                float startTime = 0;
                float minDuration = 10000;
                for (int i = nextNoteIndex; i < trackMidi.Notes.Count; i++)
                {

                    if (trackMidi.Notes[i].StartTime >= lastImpulse && trackMidi.Notes[i].StartTime <= currImpulse)
                    {
                        listNotePlay.Add(trackMidi.Notes[i]);
                        minDuration = trackMidi.Notes[i].Duration > minDuration ? minDuration : trackMidi.Notes[i].Duration;
                        duration = trackMidi.Notes[i].Duration > duration ? trackMidi.Notes[i].Duration : duration;
                        startTime = trackMidi.Notes[i].StartTime;
                    }
                    else if (trackMidi.Notes[i].StartTime > currImpulse)
                    {
                        nextNoteIndex = i;
                        nextNoteImpulse = trackMidi.Notes[i].StartTime;
                        break;
                    }
                }
                if (listNotePlay.Count > 0)
                {
                    //  Debug.Log("currImpulse: " + currImpulse);
                    if (minDuration != duration || listNotePlay.Count == 1)
                    {
                        OnNoteOn(listNotePlay.ToArray(), duration);
                    }
                    else
                    {
                        if (nextNoteImpulse <= startTime)
                        {
                            OnNoteOn(listNotePlay.ToArray(), duration * listNotePlay.Count);
                        }
                        else if (startTime + duration * listNotePlay.Count <= nextNoteImpulse)
                        {
                            OnNoteOn(listNotePlay.ToArray(), duration * listNotePlay.Count);
                        }
                        else
                        {
                            duration = nextNoteImpulse - (startTime);
                            OnNoteOn(listNotePlay.ToArray(), duration);
                        }
                    }

                }
            }

        }

        public void ResetNextImpulse()
        {

        }
        public abstract void OnNoteOn(MidiNote[] note, float duration);

        public virtual void PickUpInstrumentSuccess(float time)
        {
            isPlaying = true;
            btnActiveState.SetSprite("instrument_on");
            audioSource.loop = true;
            audioSource.Play();
            audioSource.time = time;

        }

        public virtual void RemoveInstrumentSuccess()
        {

            btnActiveState.SetSprite("instrument_off");
            isPlaying = false;
            audioSource.loop = false;
            audioSource.Stop();
        }
    }
}

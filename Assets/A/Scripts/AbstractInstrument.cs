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
            }else
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
            List<MidiNote> listNotePlay = new List<MidiNote>();
            if (lastImpulse < nextNoteImpulse && currImpulse >= nextNoteImpulse)
            {

                // read note tu currIndex -> end note

                // if end note -> ResetNextImpuse

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

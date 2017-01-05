using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MidiSheetMusic;
using UnityEngine;
namespace Assets.A.Scripts
{
    public abstract class AbstractInstrument : MonoBehaviour, InstrumentEventHandler
    {

        public Type_Instrument typeInstrument;

        public MusicBandManager manager;

        public MidiTrack trackMidi;

        public AudioSource audioSource;

        public AudioClip audioClip;

        public bool isPlaying;

        private int nextNoteIndex;
        private int nextNoteImpulse;


        public void Init(MusicBandManager _manager, MidiTrack _midiTrack, AudioSource _audioSource, AudioClip _audioClip, Type_Instrument _typeInstrument)
        {
            this.manager = _manager;
            this.trackMidi = _midiTrack;
            this.audioSource = _audioSource;
            this.audioClip = _audioClip;
            this.typeInstrument = _typeInstrument;
            audioSource.clip = audioClip;
            ResetNextImpulse();
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

        public abstract void PickUpInstrumentSuccess();

        public abstract void RemoveInstrumentSuccess();
    }
}

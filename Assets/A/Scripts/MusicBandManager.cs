using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MidiSheetMusic;
using Assets.A.Scripts.Instruments;

namespace Assets.A.Scripts
{
    public class MusicBandManager : MonoBehaviour
    {


        [SerializeField]
        private MidiFileItem midiFileItem;

        [SerializeField]
        private List<AbstractInstrument> listInstrumentActives;
        [SerializeField]
        private AbstractInstrument[] arrInstruments;

        private bool breakMusic;

        private int lastImpulse;


        // Use this for initialization
        void Start()
        {

            MidiFile midi = new MidiFile(Application.dataPath + midiFileItem.pathMidiFile);
            Debug.Log("midi.count track: " + midi.Tracks.Count);
            listInstrumentActives = new List<AbstractInstrument>();
           
            for (int i = 0; i < MusicBandConfig.NO_INSTRUMENT; i++)
            {
                if (i == (int)Type_Instrument.VIOLIN)
                {
                    arrInstruments[i].Init(midi.Tracks[i], midiFileItem.violin);
                }
                else if (i == (int)Type_Instrument.PIANO)
                {
                    arrInstruments[i].Init(midi.Tracks[i], midiFileItem.piano);
                }
                else if (i == (int)Type_Instrument.DRUM)
                {
                    arrInstruments[i].Init(midi.Tracks[i], midiFileItem.drum);
                }
                else if (i == (int)Type_Instrument.SAXOPHONE)
                {
                    arrInstruments[i].Init(midi.Tracks[i], midiFileItem.saxophone);
                }
                else if (i == (int)Type_Instrument.CELLO)
                {
                    arrInstruments[i].Init(midi.Tracks[i], midiFileItem.cello);
                }

                /*
                Debug.Log("i: " + i);
                GameObject go = new GameObject();
                go.transform.SetParent(transform);
                go.AddComponent<AudioSource>();
                AudioSource audioSource = go.GetComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.loop = true;
                Type_Instrument typeInstrument = Type_Instrument.VIOLIN;
                if (i == (int)Type_Instrument.VIOLIN)
                {
                    typeInstrument = Type_Instrument.VIOLIN;
                    go.AddComponent<Violin>();
                    AbstractInstrument instrumentScript = go.GetComponent<Violin>();
                    arrInstruments[i] = instrumentScript;
                    instrumentScript.Init(this, midi.Tracks[i], go.GetComponent<AudioSource>(), midiFileItem.violin, typeInstrument);

                }
                else if (i == (int)Type_Instrument.PIANO)
                {
                    typeInstrument = Type_Instrument.PIANO;
                    go.AddComponent<Piano>();
                    AbstractInstrument instrumentScript = go.GetComponent<Piano>();
                    arrInstruments[i] = instrumentScript;
                    instrumentScript.Init(this, midi.Tracks[i], go.GetComponent<AudioSource>(), midiFileItem.piano, typeInstrument);
                }
                else if (i == (int)Type_Instrument.DRUM)
                {
                    typeInstrument = Type_Instrument.DRUM;
                    go.AddComponent<Drum>();
                    AbstractInstrument instrumentScript = go.GetComponent<Drum>();
                    arrInstruments[i] = instrumentScript;
                    instrumentScript.Init(this, midi.Tracks[i], go.GetComponent<AudioSource>(), midiFileItem.drum, typeInstrument);
                }
                else if (i == (int)Type_Instrument.SAXOPHONE)
                {
                    typeInstrument = Type_Instrument.SAXOPHONE;
                    go.AddComponent<Saxophone>();
                    AbstractInstrument instrumentScript = go.GetComponent<Saxophone>();
                    arrInstruments[i] = instrumentScript;
                    instrumentScript.Init(this, midi.Tracks[i], go.GetComponent<AudioSource>(), midiFileItem.saxophone, typeInstrument);
                }
                else if (i == (int)Type_Instrument.CELLO)
                {
                    typeInstrument = Type_Instrument.CELLO;
                    go.AddComponent<Cello>();
                    AbstractInstrument instrumentScript = go.GetComponent<Cello>();
                    arrInstruments[i] = instrumentScript;
                    instrumentScript.Init(this, midi.Tracks[i], go.GetComponent<AudioSource>(), midiFileItem.cello, typeInstrument);
                }
                go.name = typeInstrument.ToString();
                */
            }
            
        }

        IEnumerator IEUpdateInstrument()
        {
            while (listInstrumentActives.Count > 0 && !breakMusic)
            {
                for (int i = 0; i < listInstrumentActives.Count; i++)
                {
                    /// convert time to impulse
                    int currImpulse = 0;
                    if (currImpulse < lastImpulse)
                    {
                        lastImpulse = 0;
                    }
                    listInstrumentActives[i].CheckTime(lastImpulse, currImpulse);
                }
                yield return null;
            }
        }

        public void OnPickUpInstrument(AbstractInstrument instrument)
        {
            Debug.Log("on pick up isntrument: " + instrument.typeInstrument.ToString());
            if (!listInstrumentActives.Contains(instrument))
            {

                if (listInstrumentActives.Count > 0)
                {
                 
          

                    instrument.PickUpInstrumentSuccess(listInstrumentActives[0].audioSource.time);
                    listInstrumentActives.Add(instrument);
                }
                else
                {

                 
                    breakMusic = false;
                    listInstrumentActives.Add(instrument);
                    instrument.PickUpInstrumentSuccess(0);
                    StartCoroutine(IEUpdateInstrument());
                }
            }

        }


        public void OnRemoveInstrument(AbstractInstrument instrument)
        {
            Debug.Log("remove isntrment: " + instrument.typeInstrument.ToString());
            if (listInstrumentActives.Contains(instrument))
            {
                listInstrumentActives.Remove(instrument);
               
                instrument.RemoveInstrumentSuccess();
                if (listInstrumentActives.Count == 0)
                {
                    breakMusic = true;
                }

            }
        }
    }
}
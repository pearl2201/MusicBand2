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

        private AbstractInstrument[] arrInstruments;

        private bool breakMusic;

        private int lastImpulse;


        // Use this for initialization
        void Start()
        {

            MidiFile midi = new MidiFile(Application.dataPath + midiFileItem.pathMidiFile);
            Debug.Log("midi.count track: " + midi.Tracks.Count);
            listInstrumentActives = new List<AbstractInstrument>();
            arrInstruments = new AbstractInstrument[MusicBandConfig.NO_INSTRUMENT];
            for (int i = 0; i < MusicBandConfig.NO_INSTRUMENT; i++)
            {
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
            }
            arrInstruments[0].OnPickUpInstrumentEvent();
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
                    instrument.isPlaying = true;
                    instrument.audioSource.loop = true;
                    instrument.audioSource.time = listInstrumentActives[0].audioSource.time;

                    instrument.audioSource.Play();
                    instrument.PickUpInstrumentSuccess();
                    listInstrumentActives.Add(instrument);
                }
                else
                {

                    instrument.isPlaying = true;
                    instrument.audioSource.loop = true;
                    instrument.audioSource.time = 0;
                    instrument.audioSource.Play();
                    breakMusic = false;
                    listInstrumentActives.Add(instrument);
                    instrument.PickUpInstrumentSuccess();
                    StartCoroutine(IEUpdateInstrument());
                }
            }

        }


        public void OnRemoveInstrument(AbstractInstrument instrument)
        {
            if (listInstrumentActives.Contains(instrument))
            {
                listInstrumentActives.Remove(instrument);
                instrument.isPlaying = false;
                instrument.audioSource.loop = false;
                instrument.audioSource.Stop();
                instrument.RemoveInstrumentSuccess();
                if (listInstrumentActives.Count == 0)
                {
                    breakMusic = true;
                }

            }
        }
    }
}
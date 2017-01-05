using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MidiSheetMusic;
using Assets.A.Scripts.Instruments;
using System.IO;
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

        private MidiFile midi;

        // Use this for initialization
        void Start()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, midiFileItem.pathMidiFile);


            if (Application.isEditor)
            {
                filePath = "file:///" + filePath;
            }
            if (filePath.Contains("://"))
            {
                WWW www = new WWW(filePath);
                while (!www.isDone)
                {

                }

                midi = new MidiFile(www.bytes, midiFileItem.pathMidiFile);


            }
            else
            {
                Debug.Log("error");
            }

            Debug.Log("midi.count track: " + midi.Tracks.Count);
            listInstrumentActives = new List<AbstractInstrument>();

            for (int i = 0; i < MusicBandConfig.NO_INSTRUMENT; i++)
            {
                if (i == (int)Type_Instrument.VIOLIN)
                {
                    for (int j = 0; j < midi.Tracks[i].Notes.Count; j++)
                    {
                        Debug.Log(midi.Tracks[i].Notes[j].ToString());
                    }
                   
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
                    for (int j = 0; j < midi.Tracks[i].Notes.Count; j++)
                    {
                        Debug.Log(midi.Tracks[i].Notes[j].ToString());
                    }
                }


            }
            //  StartCoroutine(IELoadFileMidi());

        }

        IEnumerator IELoadFileMidi()
        {
            yield return null;
        }
        IEnumerator IEUpdateInstrument()
        {
            while (listInstrumentActives.Count > 0 && !breakMusic)
            {
                int currImpulse = midi.Time.ConvertSecondToPulse(listInstrumentActives[0].audioSource.time);
                if (currImpulse < lastImpulse)
                {
                    lastImpulse = 0;
                }
                for (int i = 0; i < listInstrumentActives.Count; i++)
                {
                    /// convert time to impulse
                    
                    listInstrumentActives[i].CheckTime(lastImpulse, currImpulse);
                    
                }
                lastImpulse = currImpulse;
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



                   
                    listInstrumentActives.Add(instrument);
                    instrument.PickUpInstrumentSuccess(listInstrumentActives[0].audioSource.time);
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


        public MidiFile MIDI
        {
            get
            {
                return midi;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MidiSheetMusic;
using UnityEngine;
namespace Assets.A.Scripts.Instruments
{
    public class Violin : AbstractInstrument
    {


        public override void OnNoteOn(MidiNote[] note, float duration)
        {
            float durInSec = manager.MIDI.Time.ConvertPulseToSecond(duration);

            ske.timeScale = MusicBandConfig.DEFAULT_TIME_ANIM_PER_QUATERNOTE / durInSec;
           
            float docao = 0;
            for (int i = 0; i < note.Length; i++)
            {
                docao += note[i].Number;
            }
            string[] scale = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
            docao = docao / note.Length;
            if (docao > 83)
            {
                ske.state.SetAnimation(0, "Bird_3", false);
            }
            else if (docao < 55)
            {
                ske.state.SetAnimation(0, "Bird_1", false);
            }
            else
            {
                if (docao == 57 || docao == 58 || docao == 64 || docao == 71 || docao == 77 || docao == 78)
                {
                    ske.state.SetAnimation(0, "Bird_1", false);
                }
                else if (docao == 59 || docao == 65 || docao == 66 || docao == 72 || docao == 73 || docao == 79 || docao == 80)
                {
                    ske.state.SetAnimation(0, "Bird_2", false);
                }
                else if (docao == 60 || docao == 61 || docao == 67 || docao == 68 || docao == 81 || docao == 82)
                {
                    ske.state.SetAnimation(0, "Bird_3", false);
                }
                else
                {
                    ske.state.SetAnimation(0, "Bird_1", false);
                }
            }
            Debug.Log("time : " + audioSource.time + " - pulse start: " + note[0].StartTime + " - duration in pulse: " + duration + " durInsec: " + durInSec + " - time Scale: " + ske.timeScale +"- docao: " + docao );
            //ske.state.SetAnimation(0, "Bird_1", false);
        }

        public override void PickUpInstrumentSuccess(float time)
        {
            base.PickUpInstrumentSuccess(time);

        }

        public override void RemoveInstrumentSuccess()
        {
            base.RemoveInstrumentSuccess();
        }
    }
}

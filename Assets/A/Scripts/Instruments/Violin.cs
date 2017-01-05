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
           // Debug.Log("duration in pulse: " + duration + " durInsec: " + durInSec + " - time Scale: " + ske.timeScale);
            ske.state.SetAnimation(0, "Bird_1", false);
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

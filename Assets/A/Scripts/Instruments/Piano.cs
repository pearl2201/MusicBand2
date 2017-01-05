using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MidiSheetMusic;

namespace Assets.A.Scripts.Instruments
{
    public class Piano : AbstractInstrument
    {
        public override void OnNoteOn(MidiNote[] note, float duration)
        {
            float durInSec = manager.MIDI.Time.ConvertSecondToPulse(note[0].Duration);
           ;
            ske.timeScale = MusicBandConfig.DEFAULT_TIME_ANIM_PER_QUATERNOTE/durInSec;
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

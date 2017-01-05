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

        void Update()
        {
         
        }
        public override void OnNoteOn(MidiNote[] note, float duration)
        {

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

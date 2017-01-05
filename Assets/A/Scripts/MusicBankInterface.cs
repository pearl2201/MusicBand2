using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MidiSheetMusic;
using UnityEngine.EventSystems;
using UnityEngine;
namespace Assets.A.Scripts
{
    public interface InstrumentEventHandler: MusicBandIEventHandler
    {
        void OnNoteOn(MidiNote[] note, float duration);
        void CheckTime(int lastImpulse, int currImpulse);
        void PickUpInstrumentSuccess(float timePlay);
        void RemoveInstrumentSuccess();
    }

    public interface MusicBandIEventHandler : IEventSystemHandler { }
}

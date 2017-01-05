using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[System.Serializable]

public class MidiFileItem : ScriptableObject
{
    public string pathMidiFile;
    public AudioClip violin;
    public AudioClip piano;
    public AudioClip saxophone;
    public AudioClip drum;
    public AudioClip cello;
}


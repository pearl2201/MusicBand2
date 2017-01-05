using UnityEngine;
using UnityEditor;
using System.Collections;

public class MIDIAssetDialogue
{


    [MenuItem("Assets/Create/MIDI/MidiFileItem")]
    public static MidiFileItem CreateNewInstrument()
    {

        MidiFileItem asset = ScriptableObject.CreateInstance<MidiFileItem>();

        AssetDatabase.CreateAsset(asset, "Assets/MidiFileItem.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
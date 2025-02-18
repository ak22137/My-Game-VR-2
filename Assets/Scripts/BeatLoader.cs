using System.Collections.Generic;
using UnityEngine;

public class BeatLoader : MonoBehaviour
{
    public TextAsset beatMapFile; // Drag your JSON file here in the Inspector

    private List<float> beats;

    void Start()
    {
        // Parse the JSON file
        BeatMap beatMap = JsonUtility.FromJson<BeatMap>(beatMapFile.text);
        beats = beatMap.beats;

        // Use the beats list in your game logic
        foreach (float beat in beats)
        {
            Debug.Log("Beat at: " + beat);
        }
    }

    // Define a class to match the JSON structure
    [System.Serializable]
    public class BeatMap
    {
        public List<float> beats;
    }
}
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BeatMapConverter : MonoBehaviour
{
    // Drag and drop your .txt file here in the Inspector
    public TextAsset txtFile; // Assign the .txt file in the Inspector

    void Start()
    {
        if (txtFile == null)
        {
            Debug.LogError("No .txt file assigned. Please drag and drop a .txt file into the 'txtFile' field.");
            return;
        }

        // Parse the timestamps
        string[] lines = txtFile.text.Split('\n');
        List<float> beats = new List<float>();

        foreach (string line in lines)
        {
            string trimmedLine = line.Trim();
            if (!string.IsNullOrEmpty(trimmedLine))
            {
                // Extract the first column (timestamp)
                string[] columns = trimmedLine.Split(new char[] { '\t', ' ' }, System.StringSplitOptions.RemoveEmptyEntries); // Split by tab or space
                if (columns.Length > 0 && float.TryParse(columns[0], out float timestamp))
                {
                    beats.Add(timestamp);
                }
            }
        }

        // Create a JSON object
        BeatMap beatMap = new BeatMap { beats = beats };

        // Convert to JSON
        string json = JsonUtility.ToJson(beatMap, true);

        // Save the JSON file to disk
        string path = Application.dataPath + "/Resources/beat_map.json";
        File.WriteAllText(path, json);

        Debug.Log("Conversion complete! JSON file saved at: " + path);
    }

    // Define a class to match the JSON structure
    [System.Serializable]
    public class BeatMap
    {
        public List<float> beats;
    }
}
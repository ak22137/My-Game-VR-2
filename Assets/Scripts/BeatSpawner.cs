using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{
    public AudioSource musicSource;
    public GameObject blockPrefab;
    public Transform[] spawnPoints;
    public Material leftMaterial;
    public Material rightMaterial;
    
    [Header("Movement Settings")]
    public float blockSpeed = 5f;    // Speed in units per second
    public float travelDistance = 10f; // Distance blocks need to travel to reach player
    
    [Range(0, 1)] public float skipProbability = 0.3f;

    private List<float> beatTimestamps = new List<float>();
    private int currentBeatIndex = 0;
    private float songStartTime;

    void Start()
    {
        LoadBeatMap();

        if (musicSource != null)
        {
            musicSource.Play();
            songStartTime = Time.time;
        }

        // Calculate and log the time blocks need to reach player
        float timeToReachPlayer = travelDistance / blockSpeed;
        Debug.Log($"Blocks will take {timeToReachPlayer} seconds to reach player at speed {blockSpeed}");

        StartCoroutine(SpawnBlocks());
    }

    void LoadBeatMap()
    {
        TextAsset beatMapFile = Resources.Load<TextAsset>("beat_map");
        if (beatMapFile != null)
        {
            BeatMap beatMap = JsonUtility.FromJson<BeatMap>(beatMapFile.text);
            beatTimestamps = beatMap.beats;
        }
        else
        {
            Debug.LogError("Could not load beat map file.");
        }
    }

    IEnumerator SpawnBlocks()
    {
        while (currentBeatIndex < beatTimestamps.Count)
        {
            float beatTime = beatTimestamps[currentBeatIndex];
            float currentTime = Time.time - songStartTime;

            bool shouldSkip = Random.value < skipProbability;

            if (!shouldSkip)
            {
                // Calculate time needed for block to reach player using the set distance
                float timeToReachPlayer = travelDistance / blockSpeed;
                
                // Calculate when to spawn the block
                float spawnTime = beatTime - timeToReachPlayer;

                // Wait until it's time to spawn
                while (currentTime < spawnTime)
                {
                    yield return null;
                    currentTime = Time.time - songStartTime;
                }

                // Randomly select a spawn point
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                
                // 50-50 probability for left/right side
                bool isLeftSide = Random.value < 0.5f;
                
                // Spawn the block
                SpawnBlock(randomSpawnPoint, isLeftSide ? "Left" : "Right");
            }

            currentBeatIndex++;
        }
    }

    void SpawnBlock(Transform spawnPoint, string side)
    {
        // Create spawn position using the set distance
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.z = travelDistance; // Set Z position based on travel distance

        GameObject block = Instantiate(blockPrefab, spawnPosition, Quaternion.identity);

        Renderer blockRenderer = block.GetComponent<Renderer>();
        if (blockRenderer != null)
        {
            blockRenderer.material = side == "Left" ? leftMaterial : rightMaterial;
        }

        block.tag = side;

        // Move the block in negative Z direction
        block.GetComponent<Rigidbody>().linearVelocity = Vector3.back * blockSpeed;
    }

    [System.Serializable]
    public class BeatMap
    {
        public List<float> beats;
    }
}
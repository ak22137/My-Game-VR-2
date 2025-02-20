using UnityEngine;

public class Block : MonoBehaviour
{
    // Reference to the prefab to spawn
    public GameObject spawnPrefab;
    public bool Game1 = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Spawn a new object at the same position
            if (Game1 == false)
            {
                SpawnNewObject();
                Debug.Log("Destroying the block and spawning a new one.");
            }
            else
            {

                Destroy(other.gameObject);
            }
            // Destroy both the bullet and the block
            // Destroy the bullet
            Destroy(gameObject);      // Destroy the current block

        }
        else if (other.CompareTag("Sword")) // Replace "OtherTag" with your actual tag
        {
            Destroy(gameObject);
        }
    }

    private void SpawnNewObject()
    {
        if (spawnPrefab != null)
        {
            // Instantiate the new object at the same position and rotation
            GameObject newObject = Instantiate(spawnPrefab, transform.position, transform.rotation);

            // Get the MoveInNegativeZ component from the new object
            MoveInNegativeZ moveScript = newObject.GetComponent<MoveInNegativeZ>();

            // If the spawned object has the MoveInNegativeZ script, set its 'move' bool to true
            if (moveScript != null)
            {
                moveScript.move = true;
            }
        }
        else
        {
            Debug.LogError("Spawn Prefab is not assigned in the Inspector!");
        }
    }
}
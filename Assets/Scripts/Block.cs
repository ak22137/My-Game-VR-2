using UnityEngine;

public class Block : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Destroy both the bullet and the block
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("Destroying the blocks");
        }
    }
}
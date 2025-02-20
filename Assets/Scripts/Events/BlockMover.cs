using UnityEngine;

public class MoveInNegativeZ : MonoBehaviour
{
    // Speed of movement along the -Z axis
    public float speed = 5.0f;
    public bool move = false;
    // Update is called once per frame
    void Update()
    {
        if (!move)
        {
            return;
        }
        // Move the object along the negative Z axis
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
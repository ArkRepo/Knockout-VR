using UnityEngine;

public class HitGloves : MonoBehaviour
{
    private Vector3 previousPosition;
    private float speed;

    void Start()
    {
        // Initialize previousPosition to the gloves' initial position
        previousPosition = transform.position;
    }

    void Update()
    {
        // Calculate the current speed based on the distance traveled since the last frame
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, previousPosition);
        speed = distance / Time.deltaTime;

        // Store the current position for use in the next frame
        previousPosition = currentPosition;
    }

    // Getter method to retrieve the speed from other scripts
    public float GetSpeed()
    {
        return speed;
    }
}

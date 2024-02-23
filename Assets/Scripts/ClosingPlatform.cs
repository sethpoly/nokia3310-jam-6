using UnityEngine;

public class ClosingPlatform : MonoBehaviour
{
    public Transform target1; // First object to move
    public Transform target2; // Second object to move
    public float closingSpeed = 5f; // Speed at which the objects move together

    private Vector3 initialPosition1;
    private Vector3 initialPosition2;
    private bool closing = false;

    void Start()
    {
        // Store initial positions of the objects
        initialPosition1 = target1.position;
        initialPosition2 = target2.position;
        StartClosing();
    }

    void Update()
    {
        // Check if closing is initiated
        if (closing)
        {
            // Calculate the midpoint between initial positions of both objects
            Vector3 midpoint = (initialPosition1 + initialPosition2) / 2f;

            // Move objects towards the midpoint
            target1.position = Vector3.MoveTowards(target1.position, midpoint, closingSpeed * Time.deltaTime);
            target2.position = Vector3.MoveTowards(target2.position, midpoint, closingSpeed * Time.deltaTime);

            // Check if objects have reached the midpoint
            if (Vector3.Distance(target1.position, midpoint) < 5f && Vector3.Distance(target2.position, midpoint) < 5f)
            {
                closing = false; // Stop closing
            }
        }
    }

    // Call this function to initiate the closing action
    public void StartClosing()
    {
        closing = true;
    }

    // Call this function to reset the platforms to their initial positions
    public void ResetPlatforms()
    {
        target1.position = initialPosition1;
        target2.position = initialPosition2;
        closing = false;
    }
}

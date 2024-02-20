using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BalloonString : MonoBehaviour
{
    [Header("Balloon String Transforms")]
    public Transform playerTransform; // Player object with Rigidbody
    public Transform balloonTransform;   // Balloon object

    [Header("Balloon String Settings")]
    [Tooltip("Number of points along the string")]
    [Range(2, 100)] public int numberOfPoints = 10; // Number of points to represent the string
    [Tooltip("Width of the string")]
    public float stringWidth = 0.1f; // Width of the string
    [Tooltip("Delay factor to reduce springiness")]
    public float delayFactor = 0.1f; // Delay factor to reduce springiness

    private LineRenderer lineRenderer;

    void Start()
    {
        // Ensure the LineRenderer component exists
        lineRenderer = GetComponent<LineRenderer>();

        // Set initial parameters
        lineRenderer.startWidth = stringWidth;
        lineRenderer.endWidth = stringWidth;
    }

    void Update()
    {
        // Update the string visualization
        DrawString();

        // Update the balloon's position with a delay
        if (playerTransform != null && balloonTransform != null)
        {
            Vector3 targetPosition = playerTransform.position;
            Vector3 currentPosition = balloonTransform.position;
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, delayFactor);
            balloonTransform.position = new Vector3(newPosition.x, balloonTransform.position.y, newPosition.z);
        }
    }

    void DrawString()
    {
        // Check if both player and balloon transforms are assigned
        if (playerTransform == null || balloonTransform == null)
            return;

        // Update the line renderer's positions
        lineRenderer.positionCount = numberOfPoints + 1;

        // Calculate the positions along the string
        for (int i = 0; i <= numberOfPoints; i++)
        {
            float t = i / (float)numberOfPoints;
            Vector3 point = Vector3.Lerp(playerTransform.position, balloonTransform.position, t);
            lineRenderer.SetPosition(i, point);
        }
    }
}

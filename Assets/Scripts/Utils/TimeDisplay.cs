using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    public Sprite[] numberSprites; // Array to hold sprite images for numbers 0 to 9
    public GameObject[] digitObjects; // Array to hold GameObjects representing individual digits

    private StopWatch stopwatch;

    void Start()
    {
        stopwatch = GetComponent<StopWatch>(); // Get the Stopwatch component attached to the same GameObject
        stopwatch.StartStopwatch();
    }

    void Update()
    {
        // Get the elapsed time as a formatted string in MM:SS format
        string timeString = stopwatch.GetElapsedTimeFormatted();

        // Split the timeString into minutes and seconds
        string[] timeComponents = timeString.Split(':');
        string minutesString = timeComponents[0];
        string secondsString = timeComponents[1];

        // Update the display for each digit
        UpdateDigitDisplay(digitObjects[0], minutesString[0]); // First digit of minutes
        UpdateDigitDisplay(digitObjects[1], minutesString[1]); // Second digit of minutes
        UpdateDigitDisplay(digitObjects[2], secondsString[0]); // First digit of seconds
        UpdateDigitDisplay(digitObjects[3], secondsString[1]); // Second digit of seconds
    }

    // Update the sprite of a digit GameObject based on the character representing the digit
    void UpdateDigitDisplay(GameObject digitObject, char digitChar)
    {
        int digitValue = int.Parse(digitChar.ToString());
        digitObject.GetComponent<SpriteRenderer>().sprite = numberSprites[digitValue];
    }
}

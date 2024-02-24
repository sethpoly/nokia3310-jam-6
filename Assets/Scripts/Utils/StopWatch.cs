using UnityEngine;

public class StopWatch : MonoBehaviour
{
    private bool isRunning = false;
    private float elapsedTime = 0f;

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public string GetElapsedTimeFormatted()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

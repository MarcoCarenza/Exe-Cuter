using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float duration = 10f;        // Time in seconds
    public bool countDown = true;       // Count down or up
    public bool autoStart = true;

    [Header("Events")]
    public UnityEvent onTimerEnd;

    private float currentTime;
    private bool isRunning = false;

    void Start()
    {
        if (countDown)
            currentTime = duration;
        else
            currentTime = 0f;

        if (autoStart)
            StartTimer();
    }

    void Update()
    {
        if (!isRunning) return;

        if (countDown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                onTimerEnd?.Invoke();
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime >= duration)
            {
                currentTime = duration;
                isRunning = false;
                onTimerEnd?.Invoke();
            }
        }
    }

    // --- Public API ---
    public void StartTimer()
    {
        isRunning = true;
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        isRunning = false;
        currentTime = countDown ? duration : 0f;
    }

    public float GetTime()
    {
        return currentTime;
    }
}

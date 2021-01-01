using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private Transform hoursPivot, minutesPivot, secondsPivot;

    private const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;

    private TimeSpan time;

    // Update is called once per frame
    private void Update()
    {
        time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0, 0, hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0, 0, minutesToDegrees * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0, 0, secondsToDegrees * (float)time.TotalSeconds);
    }
}
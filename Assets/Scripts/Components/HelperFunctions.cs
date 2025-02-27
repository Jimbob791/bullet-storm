using System.Collections;
using UnityEngine;

public static class HelperFunctions
{
    public static bool timeChanged = false;
    public static bool tutorial = true;
    public static IEnumerator ModifyTimeForDuration(float multiplier, float duration)
    {
        timeChanged = true;
        Time.timeScale = multiplier;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
        timeChanged = false;
    }

    public static void SetTime(float multiplier)
    {
        timeChanged = multiplier != 1;
        Time.timeScale = multiplier;
    }
}

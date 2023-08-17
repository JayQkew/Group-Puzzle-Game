using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 0; // Start at 0
    public TMP_Text UiTextTimer;

    // Update is called once per frame
    void Update()
    {
        timeValue += Time.deltaTime;
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        UiTextTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

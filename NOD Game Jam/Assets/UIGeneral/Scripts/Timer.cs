using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    private static float DefaultLenght = 60f*5f;
    public static float TimerValue;
    private bool running;
    private static Timer instance;

    public static TextMeshProUGUI timer;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartTimer();
            
    }

    private void Update()
    {
        TimerValue -= Time.deltaTime;
        timer.text = TimerValue.ToString("00.00");
    }

    public static void StartTimer(float Length = -1)
    {
        instance.running = true;
        TimerValue = Length > -1 ? Length : DefaultLenght;
    }

    public static void Hide()
    {
        timer.enabled = false;
    }

    public static void Pause()
    {
        instance.running = false;
    }

}

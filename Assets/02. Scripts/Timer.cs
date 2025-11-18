using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action<float> OnTimeChanged;
    public static event Action OnTimeOver;

    [SerializeField] private float maxTimer = 180f;
    private float currentTime;

    private bool isRunning = true;

    private void Start()
    {
        currentTime = maxTimer;
        OnTimeChanged?.Invoke(currentTime);
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        currentTime -= Time.deltaTime;
        OnTimeChanged?.Invoke(currentTime);

        if(currentTime <= 0f)
        {
            currentTime = 0;
            isRunning = false;

            OnTimeOver?.Invoke();
        }
    }

    public void PauseTimer(bool pause)
    {
        isRunning = !pause;
    }
}

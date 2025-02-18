using System;
using UnityEngine;

public class GoodTimer
{

    private readonly float waitTimeSec;
    private readonly Action callback;

    private readonly float startTime;

    private bool isCanceled = false;

    public GoodTimer(float waitTimeSec, Action callback)
    {
        this.waitTimeSec = waitTimeSec;
        this.callback = callback;

        this.startTime = Time.time;

        TimerManager.RegisterTimer(this);
    }

    public void Cancel()
    {
        isCanceled = true;
    }

    public void InvokeCallback()
    {
        if (!isCanceled)
        {
            callback.Invoke();
        }
    }

    public bool IsFinished()
    {
        return startTime + waitTimeSec <= Time.time;
    }
}
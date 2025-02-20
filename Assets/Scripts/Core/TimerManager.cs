using System;
using UnityEngine;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{

    public static TimerManager instance;

    // TODO a priority queue would be much better here, so we could always
    // dequeue from the front. Insertion becomes logn but the O(n) we do every
    // frame becomes O(1).
    private readonly HashSet<GoodTimer> timers = new HashSet<GoodTimer>();
    private readonly HashSet<GoodTimer> toRemove = new HashSet<GoodTimer>();
    private readonly HashSet<GoodTimer> timersCopy = new HashSet<GoodTimer>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        toRemove.Clear();

        timersCopy.Clear();
        timersCopy.UnionWith(timers);
        foreach (var timer in timersCopy)
        {
            if (timer.IsFinished())
            {
                try
                {
                    timer.InvokeCallback();
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Timer had an exception when executing. Ignoring. ${e}");
                }
                toRemove.Add(timer);
            }
        }

        foreach (var timer in toRemove)
        {
            timers.Remove(timer);
        }
    }

    public static void RegisterTimer(GoodTimer timer)
    {
        instance.timers.Add(timer);
    }

}
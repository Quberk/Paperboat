using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.adjust.sdk;

public class AdjustController : MonoBehaviour
{
    public static AdjustController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CallingAdjustEvent(string token)
    {
        AdjustEvent callEvent = new AdjustEvent(token);
        Adjust.trackEvent(callEvent);
    }
}

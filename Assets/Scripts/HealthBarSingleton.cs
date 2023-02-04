using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// SIGLETON WILL ONLY ALOW ONE INSTANCE OF AN OBJECT TO RUN

public class HealthBarSingleton : MonoBehaviour
{
    private static HealthBarSingleton instance;

    public static HealthBarSingleton Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // If no Player ever existed, we are it.
        if (instance == null)
            instance = this;
        // If one already exist, it's because it came from another level.
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
}
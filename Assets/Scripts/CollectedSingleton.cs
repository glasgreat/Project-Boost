using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedSingleton : MonoBehaviour
{
    private static CollectedSingleton instance;

    public static CollectedSingleton Instance
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


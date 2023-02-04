using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSingleton : MonoBehaviour
{
    private static ScoreSingleton instance;

    public static ScoreSingleton Instance
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

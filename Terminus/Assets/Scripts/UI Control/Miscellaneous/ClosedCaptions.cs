using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Singleton controlling display of closed-captions
/// throughout application.
/// </summary>
public class ClosedCaptions : MonoBehaviour
{
    // singleton support
    static ClosedCaptions instance;

    /// <summary>
    /// Read-access property returning instance of
    /// CC singleton.
    /// </summary>
    public static ClosedCaptions Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if singleton has already initialized as another instance
        if (instance != null && Instance != this)
        {
            // destroy this instance and break
            Destroy(gameObject);
            return;
        }

        // set this object as instance of singleton
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

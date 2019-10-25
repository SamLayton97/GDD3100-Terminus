using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton managing design and size of user's mouse cursor.
/// </summary>
public class CursorManager : MonoBehaviour
{
    // singleton variables
    static CursorManager instance;

    /// <summary>
    /// Static read-access property returning
    /// instance of cursor manager.
    /// </summary>
    public static CursorManager Instance
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

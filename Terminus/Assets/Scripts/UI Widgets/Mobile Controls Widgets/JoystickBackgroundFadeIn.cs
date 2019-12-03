using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls fade-in and fade-out of mobile joystick's background
/// when users holds the holds it down/releases hold
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class JoystickBackgroundFadeIn : MonoBehaviour
{
    // fade in configuration variables
    [Range(0f, 5f)]
    [SerializeField] float fadeInRate = 0f;     // rate at which background fades in and out

    // support variables
    CanvasGroup myCanvasGroup;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant components
        myCanvasGroup = GetComponent<CanvasGroup>();
    }
    
    /// <summary>
    /// Initiates background fade in when user's
    /// pointer depresses over joystick
    /// </summary>
    public void HandlePointerDown()
    {
        Debug.Log("here");
    }

    /// <summary>
    /// Initiates background fade out when user's
    /// pointer releases joystick
    /// </summary>
    public void HandlePointerUp()
    {
        Debug.Log("there");
    }
}

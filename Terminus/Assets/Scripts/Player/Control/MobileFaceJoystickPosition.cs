using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Rotates object to face direction of mobile joystick
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MobileFaceJoystickPosition : MonoBehaviour
{
    // configuration variables
    [SerializeField] float rotationSpeed = 10f;     // rate at which object turns to face joystick direction

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // if game isn't paused
        if (Time.timeScale != 0)
        {
            // 
        }
    }
}

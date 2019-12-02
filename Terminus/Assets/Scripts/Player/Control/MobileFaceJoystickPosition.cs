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

    // rotation support variables
    float faceHorizontal = 0f;
    float faceVertical = 0f;

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // get joystick input
        faceHorizontal = CrossPlatformInputManager.GetAxis("AimHorizontal");
        faceVertical = CrossPlatformInputManager.GetAxis("AimVertical");

        // if game isn't paused and player gave some input
        if (Time.timeScale != 0 && !(faceHorizontal == 0 && faceVertical == 0))
        {
            // turn object to face joystick direction
            Quaternion targetOrientation = new Quaternion();
            targetOrientation.eulerAngles = new Vector3(0, 0, 
                Mathf.Atan2(faceVertical, faceHorizontal) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetOrientation, rotationSpeed);
        }
    }
}

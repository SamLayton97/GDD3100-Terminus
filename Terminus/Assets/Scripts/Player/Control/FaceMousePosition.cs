﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates an object to face mouse position
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class FaceMousePosition : MonoBehaviour
{
    // public variables
    public float rotationSpeed = 10f;   // rate at which object turns to face mouse position

    // Update is called once per frame
    void Update()
    {
        // if game isn't paused
        if (Time.timeScale != 0)
        {
            // find angle between object and mouse position
            Vector2 PCToMouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1)) - transform.position;
            float angleToMouse = Mathf.Atan2(PCToMouse.y, PCToMouse.x) * Mathf.Rad2Deg;

            // turn object to face user's mouse
            Quaternion targetOrientation = new Quaternion();
            targetOrientation.eulerAngles = new Vector3(0, 0, angleToMouse);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetOrientation, rotationSpeed);
        }
    }
}

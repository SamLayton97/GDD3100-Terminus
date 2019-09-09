using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates player character to face mouse position
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // calculate angle between mouse position and player character
        Vector2 PCToMouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)) 
            - transform.position;
        float angleToMouse = Mathf.Atan2(PCToMouse.y, PCToMouse.x) * Mathf.Rad2Deg;

        // rotate object to face mouse position
        transform.Rotate((Vector3.forward * angleToMouse) - transform.rotation.eulerAngles);

    }
}

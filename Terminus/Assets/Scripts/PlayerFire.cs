using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Registers user's fire input, firing weapon/tool in direction
/// of mouse and sending player in opposite direction.
/// Note: Requires player character to have the Rigidbody2D component.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerFire : MonoBehaviour
{
    // private variables
    bool firedLastFrame = false;        // flag to track whether player has fired since last frame (used for semi-automatice weapons)

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if player character registers fire input (mouse 0 by default)
        if (Input.GetAxis("Fire") != 0)
        {
            // if player hasn't fired last frame
            if (!firedLastFrame)
            {
                firedLastFrame = true;
                Debug.Log("Fire");
            }
        }
        // otherwise (no fire input registered)
        else
            // reset fired last frame flag
            firedLastFrame = false;
    }
}

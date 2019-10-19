using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Semi-automatic weapon which fires a single photon
/// each time it registers user input
/// </summary>
public class PhotonThrower : Weapon
{

    void Update()
    {
        Debug.Log(GetComponent<Animator>().GetBool("isShooting"));
    }
}

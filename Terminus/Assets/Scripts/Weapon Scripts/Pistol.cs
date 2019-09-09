using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple semi-automatic pistol used by player
/// </summary>
public class Pistol : Weapon
{
    public override void RegisterShot()
    {
        Debug.Log("Pistol shot registered");
    }
}

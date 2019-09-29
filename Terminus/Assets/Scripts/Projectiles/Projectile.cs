using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls damage-dealing beheviors internal to projectile
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(FaceVelocity))]
public class Projectile : MonoBehaviour
{
    // private variables
    [SerializeField] float damage = 5f;     // damage dealt by projectile on collision

    /// <summary>
    /// Provides public read-access to damage dealt 
    /// by projectile on collision
    /// </summary>
    public float Damage
    {
        get { return damage; }
    }
}

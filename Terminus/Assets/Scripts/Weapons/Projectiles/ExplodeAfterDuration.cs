using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes projectile to explode after pre-defined period of time.
/// Typically used by projectiles who have an area of effect (e.g., bio projectiles).
/// </summary>
public class ExplodeAfterDuration : MonoBehaviour
{
    // public variables
    [Range(0, 15)]
    public float timeToExplosion = 3f;                  // time (in seconds) until object explodes
    public GameObject myExplosion;                      // area of effect object created after duration

    // private variables
    float explosionCounter = 0;

    // Update is called once per frame
    void Update()
    {
        // increment counter by time between frames
        explosionCounter += Time.deltaTime;

        // if counter exceeds limit
        if (explosionCounter >= timeToExplosion)
        {
            // create explosion at object's position
            Instantiate(myExplosion, transform.position, Quaternion.identity);

            // destroy self
            Destroy(gameObject);
        }
    }
}

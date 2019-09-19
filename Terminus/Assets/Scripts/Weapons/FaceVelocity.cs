using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates an object to face velocity it's traveling in
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class FaceVelocity : MonoBehaviour
{
    // private variables
    Rigidbody2D myRigidbody2D;                  // rigidbody component used to find object's current velocity
    Rigidbody2D relativeTo;                     // rigidbody of object to find relative velocity to

    /// <summary>
    /// Property with write access to rigidbody component
    /// of object to find relative velocity from.
    /// Note: If null, this object rotates to face velocity
    /// relative to world.
    /// </summary>
    public Rigidbody2D RelativeTo
    {
        set { relativeTo = value; }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // rotate to face relative velocity
        Vector2 relativeVelocity = myRigidbody2D.velocity.normalized;
        if (relativeTo != null)
        {
            relativeVelocity = relativeVelocity - relativeTo.velocity.normalized;
            Debug.Log(relativeTo.gameObject.name);
        }
        transform.Rotate(new Vector3(0, 0, Mathf.Atan2(relativeVelocity.y, relativeVelocity.x) * Mathf.Rad2Deg));
    }
}

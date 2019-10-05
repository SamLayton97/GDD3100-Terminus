using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows agent to drop certain materials when they die.
/// Note: public DropMaterials() function must be called by
/// agent's AgentHealth component.
/// </summary>
[RequireComponent(typeof(AgentHealth))]
[RequireComponent(typeof(Rigidbody2D))]
public class DropMaterialOnDeath : MonoBehaviour
{
    // serialized variables
    [SerializeField] GameObject materialDropped;        // material prefab dropped by agent on death
    [SerializeField] int maxMaterialsDropped = 2;       // max number of materials dropped by agent on death
    [SerializeField] float dropForceMagnitude = 5f;     // magnitude that agent sends

    // private variables
    Rigidbody2D myRigidBody2D;                          // agent's rigidbody component (used for drops' relative velocity)

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // grab relevant components
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Drops materials when agent dies
    /// </summary>
    public void DropMaterials()
    {
        // for random number of materials dropped by agent
        int amountDropped = Random.Range(0, maxMaterialsDropped + 1);
        for (int i = 0; i < 2; i++)
        {
            // TODO: instantiate material at agent's position, moving it in random direction
            Rigidbody2D currDrop = Instantiate(materialDropped, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            currDrop.velocity = myRigidBody2D.velocity;
            float dropAngle = Random.Range(0, 2 * Mathf.PI);
            currDrop.AddForce(new Vector2(Mathf.Cos(dropAngle), Mathf.Sin(dropAngle)).normalized, ForceMode2D.Impulse);
        }
    }
}

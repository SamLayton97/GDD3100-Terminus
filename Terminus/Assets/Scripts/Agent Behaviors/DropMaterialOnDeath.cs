using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows agent to drop certain materials when they die.
/// Note: public DropMaterials() function must be called by
/// agent's AgentHealth component.
/// </summary>
[RequireComponent(typeof(AgentHealth))]
public class DropMaterialOnDeath : MonoBehaviour
{
    // serialized variables
    [SerializeField] GameObject materialDropped;        // material prefab dropped by agent on death
    [SerializeField] int maxMaterialsDropped = 2;       // max number of materials dropped by agent on death

    /// <summary>
    /// Drops materials when agent dies
    /// </summary>
    public void DropMaterials()
    {
        // for random number of materials dropped by agent
        int amountDropped = Random.Range(0, maxMaterialsDropped + 1);
        for (int i = 0; i < amountDropped; i++)
        {
            // instantiate material at agent's position
            Instantiate(materialDropped, transform.position, Quaternion.identity);

            // TODO: send dropped material moving in random direction

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Adds crafting materials of specific type to player's
/// inventory on collision
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AddCraftingMaterialOnCollision : CraftingMaterialAdder
{
    // serialized variables
    [SerializeField] CraftingMaterials materialToAdd =  // type of crafting material to give to player on collision
        CraftingMaterials.biomass;
    [Range(1, 10)]
    [SerializeField] int amountToAdd = 1;               // amount of specific material to add to player's inventory
    [SerializeField] AudioClipNames collisionSound =    // sound effect played when pickup enters collision
        AudioClipNames.env_pickUpMaterial;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Called when incoming collider makes contact with object's collider
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if other object in collision is on player layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // give player amount of corresponding material types
            addMaterialsEvent.Invoke(materialToAdd, amountToAdd);

            // play pickup sound effect and destroy self
            AudioManager.Play(collisionSound, true);
            Destroy(gameObject);
        }
    }
}

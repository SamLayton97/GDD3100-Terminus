using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds crafting materials of specific type to player's
/// inventory on collision
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class AddCraftingMaterialOnCollision : MonoBehaviour
{
    // serialized variables
    [SerializeField] CraftingMaterials materialToAdd =  // type of crafting material to give to player on collision
        CraftingMaterials.biomass;
    [Range(1, 10)]
    [SerializeField] int amountToAdd = 1;               // amount of specific material to add to player's inventory

    // Start is called before the first frame update
    void Start()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomizes initial sprite used by an object
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class RandomizeInitialSprite : MonoBehaviour
{
    // serialized variables
    [SerializeField] Sprite[] spritesToChoose;      // collection of sprites to choose from on initialization

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // pull random sprite and set it as object's sprite
        GetComponent<SpriteRenderer>().sprite = spritesToChoose[Random.Range(0, spritesToChoose.Length)];
    }
}

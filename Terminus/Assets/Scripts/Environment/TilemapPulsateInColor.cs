using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Lerps tilemap between two colors, creating
/// simple pulsating effect
/// </summary>
[RequireComponent(typeof(Tilemap))]
public class TilemapPulsateInColor : MonoBehaviour
{
    // public variables
    public Color toColor;               // color tilemap pulsates to
    public Color fromColor;             // color tilemap pulsates from
    public float pulsationRate = 1f;    // rate by which tiles pulsate

    // private variables
    Tilemap myTilemap;              // component used to set color of tilemap

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize tilemap color
        myTilemap = GetComponent<Tilemap>();
        myTilemap.color = fromColor;
    }

    // Update is called once per frame
    void Update()
    {
        // lerp tilemap's color, reversing direction as appropriate
        myTilemap.color = Color.Lerp(myTilemap.color, toColor, pulsationRate);
        if (myTilemap.color == toColor)
        {
            Color tempColor = fromColor;
            fromColor = toColor;
            toColor = tempColor;
        }
    }
}

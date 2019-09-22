using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Evaluates player's performance, displaying a metric 
/// on the end-of-level menu
/// </summary>
public class EndOfLevelEvaluator : MonoBehaviour
{
    // public variables
    public Sprite emptyStar;            // default star icon displayed when player fails to meet necessary threshold
    public Color emptyStarColor;        // color of an unfilled star icon
    public Sprite filledStar;           // star icon awared to player when they meet necessary success threshold
    public Color filledStarColor;       // color of filled star icon (usually brighter than unfilled color)

    // evaluation panel elements
    public Image[] stars;               // array of stars players can earn for good performance

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

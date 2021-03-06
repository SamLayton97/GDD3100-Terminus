﻿using System.Collections;
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
    public Sprite filledStar;           // star icon awared to player when they meet necessary success threshold
    public Color filledStarColor;       // color of filled star icon (usually brighter than unfilled color)

    // evaluation panel elements
    public Image[] stars;                   // array of stars players can earn for good performance
    public Text realityLinkStatus;          // text-status of player's link with this reality
    public string[] realityLinkStatuses;    // array of status messages indicating strength of player's link with reality
    public Color[] realityLinkTextColors;   // array of colors to set reality link text to according to strength

    // star thresholds
    public int threeStarThreshold = 70;     // remaining sanity needed to earn 3 stars
    public int twoStarThreshold = 30;       // remaining sanity needed to earn 2 stars
    public int oneStarThreshold = 1;        // remaining sanity needed to earn 1 star

    /// <summary>
    /// Evaluates player according to their remaining sanity
    /// </summary>
    /// <param name="remainingSanity">percentage of sanity player has 
    /// when they end level</param>
    public void Evaluate(float remainingSanity)
    {
        // determine number of stars earned
        int starsEarned = 0;
        if (remainingSanity >= threeStarThreshold)
            starsEarned = 3;
        else if (remainingSanity >= twoStarThreshold)
            starsEarned = 2;
        else if (remainingSanity >= oneStarThreshold)
            starsEarned = 1;

        // set image and color of stars player earned
        for (int i = 0; i < starsEarned; i++)
        {
            stars[i].sprite = filledStar;
            stars[i].color = filledStarColor;
        }

        // set reality link message according to stars earned
        realityLinkStatus.text = realityLinkStatuses[starsEarned];
        realityLinkStatus.color = realityLinkTextColors[starsEarned];
    }
}

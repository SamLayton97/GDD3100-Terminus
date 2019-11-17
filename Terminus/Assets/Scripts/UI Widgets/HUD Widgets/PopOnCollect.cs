using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes ammo meter on HUD to pop when player
/// collects ammo for its corresponding weapon
/// </summary>
public class PopOnCollect : MonoBehaviour
{
    // meter pop support fields
    [SerializeField] RectTransform popTransform;            // controls size of meter pop microinteraction
    CanvasGroup popCanvasGroup;                             // controls visibility of meter pop interaction
    Vector2 popPeakScale = new Vector2();                   // scale meter pop grows to
    IEnumerator popCoroutine;                               // coroutine controlling visibility and scale of pop image
    [Range(0f, 10f)]
    [SerializeField] float popExpandRate = 1f;              // rate at which pop overlay expands to its peak scale
    [Range(0f, 5f)]
    [SerializeField] float popDiminishRate = 1f;            // rate at which pop diminishes after reaching peak scale



}

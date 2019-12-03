using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes button's fill to "pop" when player presses it
/// </summary>
public class PopButtonOnPress : MonoBehaviour
{
    // pop configuration variables
    [SerializeField] RectTransform popTransform;        // controls size of button pop
    [Range(0f, 10f)]
    [SerializeField] float popExpandRate = 1f;          // rate at which pop overlay expands to its peak scale
    [Range(0f, 5f)]
    [SerializeField] float popDiminishRate = 1f;        // rate at which pop diminishes after reaching peak scale

    // pop support variables
    CanvasGroup popCanvasGroup;                         // controls visibility of meter pop interaction
    Vector2 popPeakScale = new Vector2();               // scale meter pop grows to
    IEnumerator popCoroutine;                           // coroutine controlling visibility and scale of pop image
}

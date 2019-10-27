using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays notifications to the player on the HUD.
/// Typically used for alerts like low oxygen.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class Notifications : MonoBehaviour
{
    // display support
    [SerializeField] Text notificationText;
    CanvasGroup myCanvasGroup;

}

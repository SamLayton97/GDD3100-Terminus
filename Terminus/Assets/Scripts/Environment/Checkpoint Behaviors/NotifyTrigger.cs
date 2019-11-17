using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sends brief notification to player when
/// they collide with this checkpoint
/// </summary>
public class NotifyTrigger : MonoBehaviour
{
    // notification config variables
    [SerializeField]
    string message = "";            // notification displayed to player on trigger enter

    /// <summary>
    /// Called when player enters trigger box
    /// </summary>
    /// <param name="other">collision info</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if player's body entered trigger
        if (!other.isTrigger)
        {
            // send notification
            Notifications.Instance.Display(message);

        }
    }
}

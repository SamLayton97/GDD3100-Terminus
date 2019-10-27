using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays notifications to the player on the HUD.
/// Typically used for alerts.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class Notifications : MonoBehaviour
{
    // display support
    [SerializeField] Text notificationText;
    CanvasGroup myCanvasGroup;
    RectTransform myTransform;

    // singleton support
    static Notifications instance;

    public static Notifications Instance
    {
        get { return instance; }
    }


    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // set universally retrievable instance as self
        instance = this;

        // retrieve necessary components
        myCanvasGroup = GetComponent<CanvasGroup>();
        myTransform = GetComponent<RectTransform>();
        if (notificationText == null)
            notificationText = GetComponentInChildren<Text>();
    }

    public void UpdateText(string newText)
    {
        notificationText.text = newText;
    }
}

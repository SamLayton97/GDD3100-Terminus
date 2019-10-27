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
    RectTransform myTransform;
    bool displaying = false;

    // configuration support
    [SerializeField] float growRate = 1f;       // rate at which HUD element grows/shrinks before showing text
    [SerializeField] float displayTime = 1f;    // time notification remains at full size before shrinking

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
        myTransform = GetComponent<RectTransform>();
        if (notificationText == null)
            notificationText = GetComponentInChildren<Text>();

        // initialize notifications panel
        myTransform.localScale = new Vector2(0, myTransform.localScale.y);
        notificationText.text = "";
    }

    void Start()
    {
        Display("Critical Oxygen!");
    }

    /// <summary>
    /// Starts coroutine to display notification on-screen
    /// </summary>
    /// <param name="notification">text displayed to user</param>
    public void Display(string notification)
    {
        // if not already displaying something, show notification
        if (!displaying) StartCoroutine(DrawNotification(notification));
    }

    /// <summary>
    /// Expands panel to full size, displaying notification
    /// before shrinking it back down.
    /// </summary>
    /// <param name="notification">notification displayed to user</param>
    /// <returns></returns>
    IEnumerator DrawNotification(string notification)
    {
        // set text
        displaying = true;
        notificationText.text = notification;

        Debug.Log(myTransform.localScale.x);

        // TODO: expand notification
        while (myTransform.localScale.x < 1)
        {
            myTransform.localScale = new Vector2(myTransform.localScale.x + (Time.deltaTime * growRate), 1);
            Debug.Log("waiting");
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Done!");
        displaying = false;

        // TODO: display notifcation

        // TODO: shrink notification

    }
}

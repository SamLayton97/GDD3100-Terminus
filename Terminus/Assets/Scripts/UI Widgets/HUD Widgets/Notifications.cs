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
    Color textColor;
    Color invisible;

    // configuration support
    [SerializeField] float growRate = 1f;       // rate at which HUD element grows/shrinks before showing text
    [SerializeField] float displayTime = 3f;    // time notification remains at full size before shrinking
    [SerializeField] Color textFlashColor;      // color text flashes to when displayed
    [SerializeField] float flashRate = 5f;      // speed at which notifaction text flashes before disappearing

    // singleton support
    static Notifications instance;

    /// <summary>
    /// Universally-accessible read-access property returning
    /// instance of this notification controller.
    /// </summary>
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

        // retrieve necessary components/info from components
        myTransform = GetComponent<RectTransform>();
        if (notificationText == null)
            notificationText = GetComponentInChildren<Text>();
        textColor = notificationText.color;
        invisible = new Color(textColor.r, textColor.g, textColor.b, 0);

        // initialize notifications panel
        myTransform.localScale = new Vector2(0, myTransform.localScale.y);
        notificationText.color = invisible;
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
        // set text box's content -- determines size of panel
        displaying = true;
        notificationText.text = notification;

        // expand notification
        while (myTransform.localScale.x < 1)
        {
            myTransform.localScale = new Vector2(Mathf.Min(1, myTransform.localScale.x + (Time.deltaTime * growRate)), 1);
            yield return new WaitForEndOfFrame();
        }

        // display flashing notifcation text
        float displayCounter = 0;
        notificationText.color = textColor;
        while (displayCounter < displayTime)
        {
            notificationText.color = Color.Lerp(textColor, textFlashColor, Mathf.PingPong(Time.time * flashRate, 1));
            displayCounter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // hide text and shrink notification
        notificationText.color = invisible;
        while (myTransform.localScale.x > 0)
        {
            myTransform.localScale = new Vector2(Mathf.Max(0, myTransform.localScale.x - (Time.deltaTime * growRate)), 1);
            yield return new WaitForEndOfFrame();
        }
        displaying = false;
    }
}

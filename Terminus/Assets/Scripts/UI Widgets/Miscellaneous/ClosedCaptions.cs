using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Singleton controlling display of closed-captions
/// throughout application.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class ClosedCaptions : MonoBehaviour
{
    // configuration variables
    [Range(0.5f, 5f)]
    [SerializeField] float displayTime = 2f;            // duration captions display on-screen
    [Range(1, 60)]
    [SerializeField] int ccQueueLimit = 50;             // (inclusive) upper bound of characters displayed on caption

    // display support
    CanvasGroup myCanvasGroup;
    [SerializeField] Text ccText;
    IEnumerator coroutineCC;
    List<string> activeCaptions =
        new List<string>();

    // toggle support
    public bool ccEnabled = false;

    // singleton support
    static ClosedCaptions instance;

    /// <summary>
    /// Read-access property returning instance of
    /// CC singleton.
    /// </summary>
    public static ClosedCaptions Instance
    {
        get { return instance; }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if singleton has already initialized as another instance
        if (instance != null && Instance != this)
        {
            // destroy this instance and break
            Destroy(gameObject);
            return;
        }

        // set this object as instance of singleton
        instance = this;
        DontDestroyOnLoad(gameObject);

        // retrieve and initialize necessary components
        myCanvasGroup = GetComponent<CanvasGroup>();
        myCanvasGroup.alpha = 0;
    }

    /// <summary>
    /// Starts coroutine to draw captions on-screen
    /// </summary>
    /// <param name="caption"></param>
    public void DisplayCaptions(string caption)
    {
        // if closed captions are enabled and caption isn't duplicate
        if (ccEnabled && !activeCaptions.Contains(caption))
        {
            // add new caption onto captions box
            coroutineCC = DrawCaption(caption, displayTime);
            if (coroutineCC != null) StartCoroutine(coroutineCC);
        }
    }

    /// <summary>
    /// Coroutine which handles toggling captions box's visibility,
    /// setting its text, and making it disappear after completion.
    /// </summary>
    /// <param name="newCaption">text message to draw</param>
    /// <param name="displayTime">duration caption remains on-screen</param>
    /// <returns></returns>
    IEnumerator DrawCaption(string newCaption, float displayTime)
    {
        // if length of caption plus length of active captions wouldn't exceed max
        if (ccText.text.Length + newCaption.Length + 1 <= ccQueueLimit)
        {
            // display captions box
            myCanvasGroup.alpha = 1;

            // push caption onto list and rewrite text
            activeCaptions.Add(newCaption);
            UpdateCaptionsText();

            // wait display time, and pop caption from captions box
            yield return new WaitForSecondsRealtime(displayTime);
            activeCaptions.RemoveAt(0);
            UpdateCaptionsText();

            // if resulting list is empty, hide captions box
            if (activeCaptions.Count < 1)
                myCanvasGroup.alpha = 0;
        }
        // otherwise, return don't add caption
        else
            yield return null;
    }

    /// <summary>
    /// Writes each active caption onto closed captioning box,
    /// adding spaces as appropriate
    /// </summary>
    void UpdateCaptionsText()
    {
        string result = "";
        foreach (string caption in activeCaptions)
            result += caption + " ";
        ccText.text = result.Substring(0, Mathf.Max(0, result.Length - 1));     // truncate last space character
    }
    
}

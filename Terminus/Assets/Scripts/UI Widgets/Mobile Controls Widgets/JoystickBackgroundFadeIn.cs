using UnityEngine;
using System.Collections;

/// <summary>
/// Controls fade-in and fade-out of mobile joystick's background
/// when users holds the holds it down/releases hold
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class JoystickBackgroundFadeIn : MonoBehaviour
{
    // fade in configuration variables
    [Range(0f, 5f)]
    [SerializeField] float fadeInRate = 0f;     // rate at which background fades in and out

    // support variables
    CanvasGroup myCanvasGroup;
    IEnumerator fadeCoroutine;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant components
        myCanvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Initates fade in/out of background when user's
    /// pointer depresses over and releases joystick
    /// </summary>
    /// <param name="fadeIn">whether background should fade in</param>
    public void ToggleFadeIn(bool fadeIn)
    {
        // start/restart fade coroutine
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = ControlFade(fadeIn);
        StartCoroutine(fadeCoroutine);
    }

    /// <summary>
    /// Coroutine which handles fade in/out of 
    /// joystick background highlight
    /// </summary>
    /// <param name="fadeIn"></param>
    /// <returns></returns>
    IEnumerator ControlFade(bool fadeIn)
    {
        Debug.Log("started");

        // determine shift direction
        float fadeDirection = fadeIn ? 1f : -1f;

        // while highlight hasn't reached target alpha
        do
        {
            // shift opacity towards target over time
            myCanvasGroup.alpha += Time.deltaTime * fadeDirection * fadeInRate;
            yield return new WaitForEndOfFrame();
        } while (myCanvasGroup.alpha > 0f && myCanvasGroup.alpha < 1f);
    }
}

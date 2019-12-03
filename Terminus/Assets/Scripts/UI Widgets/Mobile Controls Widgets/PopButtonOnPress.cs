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
    Vector3 popPeakScale = new Vector3();               // scale meter pop grows to
    IEnumerator popCoroutine;                           // coroutine controlling button's "pop" microinteraction

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if not set in editor, assume component controls image on same gameobject
        if (!popTransform)
            popTransform = GetComponent<RectTransform>();

        // retrieve relevant components/information
        popCanvasGroup = popTransform.GetComponent<CanvasGroup>();
        popPeakScale = popTransform.localScale;
    }

    /// <summary>
    /// Initiates 'pop' microinteraction when user presses button down
    /// </summary>
    public void HandlePress()
    {
        // initiate button pop
        popCoroutine = PopButton();
        StartCoroutine(popCoroutine);
    }

    /// <summary>
    /// Causes button to "pop" when user presses it
    /// </summary>
    /// <returns></returns>
    IEnumerator PopButton()
    {
        // initialize pop overlay
        popCanvasGroup.alpha = 1f;

        // expand pop overlay to its peak
        float popProgress = 0f;
        do
        {
            popProgress += Time.deltaTime * popExpandRate;
            popTransform.localScale = Vector3.Lerp(Vector3.zero, popPeakScale, popProgress);
            yield return new WaitForEndOfFrame();
        } while (popProgress < 1);

        //// shrink and fade away
        float diminishProgress = 0f;
        do
        {
            diminishProgress += Time.deltaTime * popDiminishRate;
            popTransform.localScale = Vector3.Lerp(popPeakScale, Vector3.one, diminishProgress);
            popCanvasGroup.alpha = Mathf.Lerp(1, 0, diminishProgress);
            yield return new WaitForEndOfFrame();
        } while (diminishProgress < 1);
    }
}

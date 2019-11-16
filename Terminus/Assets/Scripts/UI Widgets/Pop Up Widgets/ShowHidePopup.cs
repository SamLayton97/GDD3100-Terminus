using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays and hides UI popup in a visually interesting way.
/// Typically used by in-game menus.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class ShowHidePopup : MonoBehaviour
{
    // display support variables
    RectTransform myTransform;
    Vector2 showScale = new Vector2();
    IEnumerator showCoroutine;                              // coroutine controlling expansion of popup into full view
    IEnumerator hideCoroutine;                              // coroutine controlling flattening of popup

    // display configuration variables
    [SerializeField] Vector2 hiddenScale = new Vector2();   // dimension of pop-up when it is hidden -- in use, typically contains at least one 0
    [SerializeField] CanvasGroup contentVisibility;         // controls visibility of popup's content
    [Range(0f, 10f)]
    [SerializeField] float growRate = 3f;                   // rate at which popup expands horizontally

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant information
        myTransform = GetComponent<RectTransform>();
        showScale = myTransform.localScale;

        // if content visibility controller wasn't set before startup
        if (!contentVisibility)
            // assume it's part of immediate child of object
            contentVisibility = GetComponentInChildren<CanvasGroup>();

        // initialize popup to invisible and flattened
        contentVisibility.alpha = 0;
        contentVisibility.blocksRaycasts = false;
        contentVisibility.interactable = false;
        myTransform.localScale = hiddenScale;
    }

    /// <summary>
    /// Displays/hides pop-up using coroutines
    /// </summary>
    /// <param name="display">whether popup should now display</param>
    public void ToggleDisplay(bool display)
    {
        // if user requested to display popup
        if (display)
        {
            // start show coroutine
            showCoroutine = ShowPopUp();
            StartCoroutine(showCoroutine);
        }
    }

    /// <summary>
    /// Gradually expands pop-up into view before
    /// displaying its contents
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowPopUp()
    {
        // expand pop-up into full view
        float showProgress = 0f;
        do
        {
            showProgress += Time.deltaTime * growRate;
            myTransform.localScale = Vector2.Lerp(hiddenScale, showScale, showProgress);
            yield return new WaitForEndOfFrame();

        } while ((Vector2)myTransform.localScale != showScale);

        // once fully expanded, show pop-up's content
        contentVisibility.alpha = 1;
        contentVisibility.blocksRaycasts = true;
        contentVisibility.interactable = true;
    }

    /// <summary>
    /// Hides pop-up's content before gradually
    /// flattening it vertically.
    /// </summary>
    /// <returns></returns>
    IEnumerator HidePopUp()
    {
        // hide pop-up's content
        contentVisibility.alpha = 0;
        contentVisibility.blocksRaycasts = false;
        contentVisibility.interactable = false;

        // shrink popup into hidden scale
        float hideProgress = 0f;
        do
        {
            hideProgress += Time.deltaTime * growRate;
            myTransform.localScale = Vector2.Lerp(showScale, hiddenScale, hideProgress);
            yield return new WaitForEndOfFrame();

        } while ((Vector2)myTransform.localScale != hiddenScale);
    }
}

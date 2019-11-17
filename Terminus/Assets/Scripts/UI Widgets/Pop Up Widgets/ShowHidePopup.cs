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
    bool shown = false;
    RectTransform myTransform;
    Vector2 showScale = new Vector2();
    IEnumerator showCoroutine;                              // coroutine controlling expansion of popup into full view
    IEnumerator hideCoroutine;                              // coroutine controlling flattening of popup
    bool growing = false;
    bool shrinking = false;
    float iDeltaTime = 0f;                                  // timescale independent delta time -- necessary as pop-up often appear and disappear when game is paused

    // display configuration variables
    [SerializeField] Vector2 hiddenScale = new Vector2();   // dimension of pop-up when it is hidden -- in use, typically contains at least one 0
    [SerializeField] CanvasGroup contentVisibility;         // controls visibility of popup's content
    [Range(0f, 5f)]
    [SerializeField] float growRate = 3f;                   // rate at which popup shows itself
    [Range(0f, 5f)]
    [SerializeField] float shrinkRate = 3f;                 // rate at which popup hides

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve relevant information
        myTransform = GetComponent<RectTransform>();
        showScale = myTransform.localScale;
        iDeltaTime = 1f / Application.targetFrameRate;

        // if content visibility controller wasn't set before startup
        if (!contentVisibility)
            // assume it's part of immediate child of object
            contentVisibility = GetComponentInChildren<CanvasGroup>();

        // hide content
        contentVisibility.alpha = 0;
        contentVisibility.blocksRaycasts = false;
        contentVisibility.interactable = false;
        myTransform.localScale = hiddenScale;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Displays/hides pop-up using coroutines
    /// </summary>
    /// <param name="display">whether popup should now display</param>
    public void ToggleDisplay(bool display)
    {
        // if user requested to display popup
        if (display && !shown)
        {
            // interrupt hide coroutine if active
            if (shrinking) StopCoroutine(hideCoroutine);

            // start show coroutine
            showCoroutine = ShowPopUp();
            StartCoroutine(showCoroutine);
        }
        // otherwise (user requested to hide popup)
        else if (!display && shown)
        {
            // interrupt show coroutine if active
            if (growing) StopCoroutine(showCoroutine);

            // hide popup
            hideCoroutine = HidePopUp();
            StartCoroutine(hideCoroutine);
        }
    }

    #endregion

    #region Coroutines

    /// <summary>
    /// Gradually expands pop-up into view before
    /// displaying its contents
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowPopUp()
    {
        growing = true;
        shown = true;

        // expand pop-up into full view
        float showProgress = 0f;
        Vector2 startScale = myTransform.localScale;
        do
        {
            showProgress += iDeltaTime * growRate;
            myTransform.localScale = Vector2.Lerp(startScale, showScale, showProgress);
            yield return new WaitForSecondsRealtime(iDeltaTime);

        } while ((Vector2)myTransform.localScale != showScale);

        // once fully expanded, show pop-up's content
        contentVisibility.alpha = 1;
        contentVisibility.blocksRaycasts = true;
        contentVisibility.interactable = true;

        growing = false;
    }

    /// <summary>
    /// Hides pop-up's content before gradually
    /// flattening it vertically.
    /// </summary>
    /// <returns></returns>
    IEnumerator HidePopUp()
    {
        shrinking = true;
        shown = false;

        // hide pop-up's content
        contentVisibility.alpha = 0;
        contentVisibility.blocksRaycasts = false;
        contentVisibility.interactable = false;

        // shrink popup into hidden scale
        float hideProgress = 0f;
        Vector2 startScale = myTransform.localScale;
        do
        {
            hideProgress += iDeltaTime * shrinkRate;
            myTransform.localScale = Vector2.Lerp(startScale, hiddenScale, hideProgress);
            yield return new WaitForSecondsRealtime(iDeltaTime);

        } while ((Vector2)myTransform.localScale != hiddenScale);

        shrinking = false;
    }

    #endregion

}

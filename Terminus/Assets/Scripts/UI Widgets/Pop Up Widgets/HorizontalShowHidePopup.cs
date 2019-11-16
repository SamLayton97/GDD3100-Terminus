using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays and hides UI popup in a visually interesting way.
/// Typically used by in-game menus.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class HorizontalShowHidePopup : MonoBehaviour
{
    // display support variables
    RectTransform myTransform;
    Vector2 targetScale = new Vector2();
    Vector2 flattenedScale = new Vector2();

    // display configuration variables
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
        targetScale = myTransform.localScale;
        flattenedScale = new Vector2(0, targetScale.y);

        // if content visibility controller wasn't set before startup
        if (!contentVisibility)
            // assume it's part of immediate child of object
            contentVisibility = GetComponentInChildren<CanvasGroup>();

        // initialize popup to invisible and flattened
        contentVisibility.alpha = 0;
        contentVisibility.blocksRaycasts = false;
        contentVisibility.interactable = false;
        myTransform.localScale = flattenedScale;
    }

    /// <summary>
    /// Displays/hides pop-up using coroutines
    /// </summary>
    /// <param name="display">whether popup should now display</param>
    public void ToggleDisplay(bool display)
    {

    }

    /// <summary>
    /// Gradually expands pop-up into view before
    /// displaying its contents
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowPopUp()
    {
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Hides pop-up's content before gradually
    /// flattening it vertically.
    /// </summary>
    /// <returns></returns>
    IEnumerator HidePopUp()
    {
        yield return new WaitForEndOfFrame();
    }
}

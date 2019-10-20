using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls special vignetting types not supported
/// by post-processing.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class UIVignetting : MonoBehaviour
{
    // material collect vignetting
    [SerializeField] CanvasGroup materialCollectVignette;
    [Range(0.01f, 5)]
    [SerializeField] float matCollectFlashTime = 1f;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize canvas
        Canvas myCanvas = GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        myCanvas.worldCamera = Camera.main;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // add self as listener to add crafting material event
        EventManager.AddPickUpMaterialsListener(HandleCraftingMaterialCollect);
    }

    /// <summary>
    /// Flashes vignette on bottom screen when player collects a crafting material.
    /// Note: Method ignores parameters -- needed only to listen for Add Material event.
    /// </summary>
    /// <param name="material">material to add - ignored</param>
    /// <param name="amount">amount by which to add - ignored</param>
    void HandleCraftingMaterialCollect(CraftingMaterials material, int amount)
    {
        IEnumerator flashCollect = FlashMaterialVignette(matCollectFlashTime);
        StartCoroutine(flashCollect);
    }

    /// <summary>
    /// Flashes partial vignette at bottom screen,
    /// indicating player has collecting a crafting material.
    /// </summary>
    /// <param name="flashTime">time to complete flash</param>
    /// <returns></returns>
    IEnumerator FlashMaterialVignette(float flashTime)
    {
        materialCollectVignette.alpha = 0;
        bool increaseAlpha = true;
        do
        {
            // increase/decrease opacity of vignette, reversing direction at apex
            materialCollectVignette.alpha += Time.deltaTime * (2f / flashTime) * (increaseAlpha ? 1 : -1);
            if (materialCollectVignette.alpha >= 1)
                increaseAlpha = !increaseAlpha;

            yield return new WaitForEndOfFrame();
        } while (materialCollectVignette.alpha > 0);
    }

}

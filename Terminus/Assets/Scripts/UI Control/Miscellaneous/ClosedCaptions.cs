using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Singleton controlling display of closed-captions
/// throughout application.
/// </summary>
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
public class ClosedCaptions : MonoBehaviour
{
    // configuration variables
    [Range(0.5f, 5f)]
    [SerializeField] float displayTime = 2f;            // duration captions display on-screen

    // display support
    Canvas myCanvas;
    CanvasGroup myCanvasGroup;
    [SerializeField] Text ccText;
    IEnumerator coroutineCC;

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

        // retrieve necessary components
        myCanvas = GetComponent<Canvas>();
        myCanvasGroup = GetComponent<CanvasGroup>();

        // initialize canvas
        SceneManager.sceneLoaded += InitializeCamera;
        myCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        myCanvas.worldCamera = Camera.main;
        myCanvasGroup.alpha = 0;
    }

    /// <summary>
    /// Initializes CC's world camera to main camera of current scene
    /// </summary>
    void InitializeCamera(Scene newScene, LoadSceneMode loadMode)
    {
        myCanvas.worldCamera = Camera.main;
    }

    /// <summary>
    /// Starts coroutine to draw captions on-screen
    /// </summary>
    /// <param name="caption"></param>
    public void DisplayCaptions(string caption)
    {
        // if closed captions are enabled
        if (ccEnabled)
        {
            // stop and restart CC coroutine with new data
            if (coroutineCC != null)
                StopCoroutine(coroutineCC);
            coroutineCC = DrawCaption(caption, displayTime);
            StartCoroutine(coroutineCC);
        }
        // otherwise, log warning
        else
            Debug.LogWarning("Warning: Attempted to display closed captions when option was disabled.");
    }

    /// <summary>
    /// Coroutine which handles toggling captions box's visibility,
    /// setting its text, and making it disappear after completion.
    /// </summary>
    /// <param name="caption">text message to draw</param>
    /// <param name="displayTime">duration caption remains on-screen</param>
    /// <returns></returns>
    IEnumerator DrawCaption(string caption, float displayTime)
    {
        // display caption and wait duration (ignores timeScale)
        myCanvasGroup.alpha = 1;
        ccText.text = caption;
        yield return new WaitForSecondsRealtime(displayTime);

        // after duration, hide caption
        myCanvasGroup.alpha = 0;
    }
}

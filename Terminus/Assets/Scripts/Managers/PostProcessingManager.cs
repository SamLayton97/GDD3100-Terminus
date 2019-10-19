using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Singleton object controlling post-processing effects
/// throughout entire application.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public class PostProcessingManager : MonoBehaviour
{
    // singleton variables
    static PostProcessingManager instance;

    // post-processing support
    PostProcessVolume myVolume;

    /// <summary>
    /// Read-access property returning instance of 
    /// post-processing manager singleton
    /// </summary>
    public static PostProcessingManager Instance
    {
        get { return instance; }
    }

    #region Unity Methods

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

        // set-up volume
        myVolume = GetComponent<PostProcessVolume>();
        myVolume.isGlobal = true;

        IEnumerator testCoroutine = FlashOxygenVignette(1f);
        //StartCoroutine(testCoroutine);
    }

    #endregion

    /// <summary>
    /// Flashes bluish vignette when player's oxygen is restored
    /// </summary>
    void HandleOxygenRestored()
    {

    }

    /// <summary>
    /// Flashes blue vignette over screen, 
    /// indicating player's oxygen has been restored.
    /// </summary>
    /// <param name="flashTime">time to complete flash</param>
    /// <returns></returns>
    IEnumerator FlashOxygenVignette(float flashTime)
    {
        bool increaseWeight = true;
        do
        {
            // increment/decrement weight of volume, reversing direction at apex
            myVolume.weight += Time.deltaTime * (2f / flashTime) * (increaseWeight ? 1 : -1);
            if (myVolume.weight >= 1)
                increaseWeight = !increaseWeight;

            yield return new WaitForEndOfFrame();
        } while (myVolume.weight > 0);
    }

}

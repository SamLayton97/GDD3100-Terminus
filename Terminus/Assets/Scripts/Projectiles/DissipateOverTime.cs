using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes object to fade away and disappear over time.
/// Typically used by photon projectiles.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class DissipateOverTime : MonoBehaviour
{
    // public variables
    public float dissipationTime = 6f;          // time it takes (seconds) for object to fully disappear

    // private variables
    SpriteRenderer mySpriteRenderer;            // object's sprite renderer component (used to control opacity)
    Color originalColor;                        // starting color to transition object's sprite color from

    float counter = 0;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components/information
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = mySpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        // transition sprite's alpha to zero over time
        mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b,
            mySpriteRenderer.color.a - Time.deltaTime / dissipationTime);

        counter += Time.deltaTime;
        Debug.Log(counter);

        // if object has fully dissipated (fully transparent), destroy object
        if (mySpriteRenderer.color.a <= 0)
        {
            Debug.Log("gone");
            Destroy(gameObject);
        }
    }
}

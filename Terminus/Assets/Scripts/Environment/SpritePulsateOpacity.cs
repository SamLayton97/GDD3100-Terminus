using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes sprite color to pulsate in opacity over time
/// </summary>
public class SpritePulsateOpacity : MonoBehaviour
{
    // pulsation configuration variables
    [SerializeField] SpriteRenderer pulseRenderer;
    [Range(0f, 1f)]
    [SerializeField] float pulseValley = 0f;
    [Range(0f, 1f)]
    [SerializeField] float pulsePeak = 1f;
    [Range(0f, 5f)]
    [SerializeField] float minimumPulseRate = 1f;

    // pulse support variables
    Color startingColor;
    bool ascending = true;
    float pulseRate = 0f;

    /// <summary>
    /// Read/write-access property returning the
    /// rate at which the sprite pulsates.
    /// </summary>
    public float PulseRate
    {
        get { return pulseRate; }
        set { pulseRate = Mathf.Clamp01(value) * 5f + minimumPulseRate; }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if not set in editor, assume renderer is part of this object
        if (!pulseRenderer)
            pulseRenderer = GetComponent<SpriteRenderer>();

        // initialize opacity of sprite
        startingColor = pulseRenderer.color;
        pulseRenderer.color = new Color(startingColor.r, startingColor.g, startingColor.b, pulseValley);
    }

    // Update is called once per frame
    void Update()
    {
        // adjust alpha of sprite
        pulseRenderer.color = new Color(startingColor.r, startingColor.g, startingColor.b,
            Mathf.Clamp(pulseRenderer.color.a + Time.deltaTime * PulseRate * (ascending ? 1 : -1), pulseValley, pulsePeak));

        // reverse direction at peak/valley
        if (pulseRenderer.color.a >= pulsePeak || pulseRenderer.color.a <= pulseValley)
            ascending = !ascending;
    }
}

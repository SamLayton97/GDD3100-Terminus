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
    [SerializeField] float pulseRate = 1f;

    // pulse support variables
    Color startingColor;
    bool ascending = true;

    /// <summary>
    /// Read/write-access property returning the
    /// highest opacity sprite will pulsate to.
    /// Clamped between 0 and 1.
    /// </summary>
    public float PulsePeak
    {
        get { return pulsePeak; }
        set { pulsePeak = Mathf.Clamp01(value); }
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
            Mathf.Clamp(pulseRenderer.color.a + Time.deltaTime * pulseRate * (ascending ? 1 : -1), pulseValley, pulsePeak));

        // reverse direction at peak/valley
        if (pulseRenderer.color.a >= pulsePeak || pulseRenderer.color.a <= pulseValley)
            ascending = !ascending;
    }
}

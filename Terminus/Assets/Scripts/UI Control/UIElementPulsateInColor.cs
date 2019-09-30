using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shifts UI element's color between two predefined colors over time
/// </summary>
public class UIElementPulsateInColor : MonoBehaviour
{
    // public variables
    public Color toColor;               // color to pulsate to
    public Color fromColor;             // color to pulsate from
    public float pulsationRate = 1f;    // speed at which element pulsates between two colors

    // private variables
    Image myImage;                      // element's Image component (used to pulsate between colors)

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // initialize image color
        myImage = GetComponent<Image>();
        myImage.color = fromColor;
    }

    // Update is called once per frame
    void Update()
    {
        // lerp from one color to another, reversing direction as appropriate
        myImage.color = Color.Lerp(myImage.color, toColor, pulsationRate);
        if (myImage.color == toColor)
        {
            Color tempColor = fromColor;
            fromColor = toColor;
            toColor = tempColor;
        }
    }
}

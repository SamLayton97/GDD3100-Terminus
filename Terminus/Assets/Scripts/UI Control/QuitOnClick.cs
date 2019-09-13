using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Closes application when user clicks object.
/// </summary>
public class QuitOnClick : MonoBehaviour
{
    /// <summary>
    /// Closes application on click
    /// </summary>
    public void HandleClickEvent()
    {
        Debug.Log("Application closed by user after running for " + Time.time + " seconds.");
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls navigation between scenes from
/// the main menu/title screen
/// </summary>
public class MainMenuNavigation : SceneTransitioner
{
    /// <summary>
    /// Handles player pressing the "Play" button
    /// </summary>
    public void PlayButtonOnClick()
    {
        // transition to "premise" scene
        transitionSceneEvent.Invoke(transitionTo[0]);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Handles player pressing the "Help" button
    /// </summary>
    public void HelpButtonOnClick()
    {
        // transition to "instructions" scene
        transitionSceneEvent.Invoke(transitionTo[1]);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    public void OptionsButtonClick()
    {
        // transition to "options" scene
        transitionSceneEvent.Invoke(transitionTo[3]);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Handles player pressing the "Credits" button
    /// </summary>
    public void CreditsButtonOnClick()
    {
        // transition to "credits" scene
        transitionSceneEvent.Invoke(transitionTo[2]);
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
    }

    /// <summary>
    /// Handles player pressing the "Quit" button
    /// </summary>
    public void QuitButtonOnClick()
    {
        // close application
        Debug.Log("Application closed after " + Time.time + " seconds.");
        AudioManager.Play(AudioClipNames.UI_buttonPress, true);
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages transitions to scenes from current scene
/// </summary>
public class SceneTransitionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // add self as listener to Transition Scene event
        EventManager.AddTransitionSceneListener(TransitionToScene);
    }

    /// <summary>
    /// Loads scene by name (string), cleaning up old scene and
    /// saving any necessary data.
    /// </summary>
    /// <param name="sceneName"></param>
    void TransitionToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

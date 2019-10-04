using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to tell Scene Manager to load a particular scene.
/// Passes name (string) of scene invoker elects to load.
/// </summary>
public class TransitionToSceneEvent : UnityEvent<string>
{
}

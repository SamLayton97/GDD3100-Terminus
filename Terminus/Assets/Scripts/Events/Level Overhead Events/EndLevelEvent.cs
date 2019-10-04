using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event used to handle ending level on player success or failure.
/// Holds success flag (bool) and remaining sanity (float) on level end.
/// </summary>
public class EndLevelEvent : UnityEvent<bool, float>
{
}

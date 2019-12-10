using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to activate physical and audio-visual
/// feedback on player when they fire a weapon.
/// </summary>
public class ActivateFireFeedbackEvent : UnityEvent<Vector2, WeaponType>
{
}

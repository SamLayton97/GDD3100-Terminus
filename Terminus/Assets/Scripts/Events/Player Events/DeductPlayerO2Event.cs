using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoked to deduct float amount of O2 from player's tank,
/// further indicating whether to shake camera upon oxygen loss.
/// </summary>
public class DeductPlayerO2Event : UnityEvent<float, bool>
{ 
}

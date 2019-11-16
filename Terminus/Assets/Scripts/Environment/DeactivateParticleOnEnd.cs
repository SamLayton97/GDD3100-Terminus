using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deactivates one-shot particle effect after it has ended
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public class DeactivateParticleOnEnd : MonoBehaviour
{
    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // start deactivation coroutine
        StartCoroutine(LateDeactivate(GetComponent<ParticleSystem>().main.startLifetime.constant));
    }

    /// <summary>
    /// Deactivates particle effect after duration
    /// </summary>
    /// <param name="duration">duration of effect</param>
    /// <returns></returns>
    IEnumerator LateDeactivate(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}

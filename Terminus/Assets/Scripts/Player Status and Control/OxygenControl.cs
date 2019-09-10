using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages oxygen depletion and re-gain of agent
/// </summary>
public class OxygenControl : MonoBehaviour
{
    // public variables
    public float oxygenDepletionRate = 1f;  // percent of oxygen used per second

    // private variables
    int maxOxygen = 100;                    // total capacity of agent's oxygen tank
    float currOxygen = 0;                   // remaining percentage of agent's oxygen tank

    // Start is called before the first frame update
    void Start()
    {
        // "fill" agent's oxygen tank to capacity
        currOxygen = maxOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        // reduce remaining oxygen, clamping above 0
        currOxygen = Mathf.Max(0, currOxygen - (oxygenDepletionRate * Time.deltaTime));
        Debug.Log(currOxygen);
    }
}

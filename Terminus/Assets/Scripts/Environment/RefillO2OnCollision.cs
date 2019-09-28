using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Refill's player's oxygen tank by set amount on collision
/// </summary>
public class RefillO2OnCollision : MonoBehaviour
{
    // public variables
    public float refill = 25;                   // amount of agent's oxygen refilled on collision
    public AudioClipNames myCollisionSound =    // sound played when player collides with object
        AudioClipNames.env_collectOxygen;

    // event support
    RefillPlayerO2Event refillO2Event;  // event invoked to refill player's oxygen

    /// <summary>
    /// Called once before first Update()
    /// </summary>
    void Start()
    {
        // add self as invoker of refill player O2 event
        refillO2Event = new RefillPlayerO2Event();
        EventManager.AddRefillO2Invoker(this);
    }

    /// <summary>
    /// Called when incoming collider makes contact with object's collider
    /// </summary>
    /// <param name="collision">collision data</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // refill player's tank through event
        refillO2Event.Invoke(refill);

        // play oxygen collection sound effect
        AudioManager.Play(myCollisionSound, true);
    }

    /// <summary>
    /// Adds given listener to Refill Player O2 event
    /// </summary>
    /// <param name="newListener">new listener for event</param>
    public void AddRefillO2Listener(UnityAction<float> newListener)
    {
        refillO2Event.AddListener(newListener);
    }
}

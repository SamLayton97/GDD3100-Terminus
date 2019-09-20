using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enemy agent behavior of pursuing player once within
/// sight range and attacking within melee distance.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PursueAndAttack : O2Remover
{
    // state enumeration
    public enum ChaseStates
    {
        Idle,
        Pursue,
        Attack,
        Wait
    }

    // public variables
    public Transform targetTransform;                       // transform of target to pursue (typically player)
    public float attackDamage = 15f;                        // amount of O2 deducted from target's tank after initiating attack
    public float maxSpeed = 5f;                             // magnitude of agent's velocity
    public float sightRange = 30f;                          // max distance agent can see target without objects obstructing its view
    public float attackRange = 3f;                          // distance agent must be within to initiate attack on target
    public float attackCooldown = 3f;                       // time (in seconds) which agent waits after initiating an attack before pursuing
    public ChaseStates startingState = ChaseStates.Idle;    // state which pursuing agent starts in (typically Idle)
    public Color cooldownColor;                             // color agent transitions to when under cooldown

    // private variables
    ChaseStates currState;              // current state of agent
    Color standardColor;                // color of agent's sprite while idle
    Rigidbody2D myRigidbody;            // agent's rigidbody component
    SpriteRenderer mySpriteRenderer;    // agent's sprite renderer component
    Rigidbody2D targetRigidbody;        // target's rigidbody component
    Animator myAnimator;                // agent's animator component
    int ignoreLayerMask;                // physics layermask to ignore when performing raycasts
    float waitCounter = 0;              // counter used to facilitate attack cooldowns

    #region State Machine

    /// <summary>
    /// Updates agent as it waits idly
    /// </summary>
    void UpdateIdle()
    {
        // slow agent to halt and lerp color to normal
        myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, Vector2.zero, Time.deltaTime);
        mySpriteRenderer.color = Color.Lerp(mySpriteRenderer.color, standardColor, Time.deltaTime);

        // if agent can see target, move to pursue state
        if (CanSeeTarget())
        {
            myAnimator.SetTrigger("OnPursueTrigger");
            myAnimator.ResetTrigger("OnIdleTrigger");
            currState = ChaseStates.Pursue;
        }
    }

    /// <summary>
    /// Updates agent as it pursues its target
    /// </summary>
    void UpdatePursue()
    {
        // lerp agent's color to normal
        mySpriteRenderer.color = Color.Lerp(mySpriteRenderer.color, standardColor, Time.deltaTime);

        // find distance from agent to target
        float distToTarget = (transform.position - targetTransform.position).magnitude;

        // move agent to intercept target
        float targetDisplacementAtIntercept = (distToTarget / Mathf.Max(0.1f, myRigidbody.velocity.magnitude)) * targetRigidbody.velocity.magnitude;
        Vector3 interceptPoint = (Vector3)targetRigidbody.velocity.normalized * targetDisplacementAtIntercept + targetTransform.position;
        myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, (interceptPoint - transform.position).normalized * maxSpeed, Time.deltaTime);

        // if agent can no longer see target, move to idle state
        if (!CanSeeTarget())
        {
            myAnimator.SetTrigger("OnIdleTrigger");
            myAnimator.ResetTrigger("OnPursueTrigger");
            currState = ChaseStates.Idle;
        }
        // but if agent is within attack range, move to attack state
        else if (distToTarget <= attackRange)
        {
            currState = ChaseStates.Attack;
            EnterAttack();
        }
    }

    /// <summary>
    /// Initiates an attack before entering agent's wait state
    /// </summary>
    void EnterAttack()
    {
        // attack target
        deductO2Event.Invoke(attackDamage);
        mySpriteRenderer.color = cooldownColor;

        // initialize cooldown timer and transition to wait state
        waitCounter = attackCooldown;
        currState = ChaseStates.Wait;
    }

    /// <summary>
    /// Updates agent as it waits for cooldown before returning to idle state
    /// </summary>
    void UpdateWait()
    {
        // deactivate agent
        myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, Vector2.zero, Time.deltaTime);

        // decrease wait counter by time between frames
        waitCounter -= Time.deltaTime;

        // after waiting specific time, transition to idle
        if (waitCounter <= 0)
            currState = ChaseStates.Idle;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Determines whether agent's target is within
    /// sight range and has line of sight
    /// </summary>
    /// <returns>whether agent 'sees' target</returns>
    bool CanSeeTarget()
    {
        // Shoot raycast towards target, returning whether it hit
        return (Physics2D.Raycast(transform.position, targetTransform.position - transform.position, sightRange, ignoreLayerMask).transform == targetTransform);
    }

    #endregion

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve components from agent
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();

        // initialize agent and set internal variables
        currState = startingState;
        standardColor = mySpriteRenderer.color;
        ignoreLayerMask = ~(1 << 12);

        // if target wasn't set before launch
        if (targetTransform == null)
        {
            // attempt to find and set Player as target
            try
            {
                targetTransform = GameObject.Find("Player").transform;
            }
            // Log failed attempt to find Player
            catch
            {
                Debug.LogWarning("Error: " + name + " at position " + transform.position +
                    " could not find target");
            }
        }

        // retrieve target's rigidbody component
        targetRigidbody = targetTransform.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // call state-appropriate Update behavior
        switch (currState)
        {
            case ChaseStates.Idle:
                UpdateIdle();
                break;
            case ChaseStates.Pursue:
                UpdatePursue();
                break;
            case ChaseStates.Wait:
                UpdateWait();
                break;
            default:
                break;
        }
    }

    #endregion

}

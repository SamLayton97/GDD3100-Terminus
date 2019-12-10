using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enemy agent behavior of pursuing player once within
/// sight range and attacking within melee distance.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PursueAndAttack : O2Remover
{
    #region Fields

    // state enumeration
    public enum ChaseStates
    {
        Idle,
        Pursue,
        Attack,
        Wait
    }

    // configuration variables
    public Transform targetTransform;                       // transform of target to pursue (typically player)
    public float attackDamage = 15f;                        // amount of O2 deducted from target's tank after initiating attack
    [SerializeField] float maxSpeed = 5f;                   // magnitude of agent's velocity

    public float sightRange = 30f;                          // max distance agent can see target without objects obstructing its view
    public float attackRange = 3f;                          // distance agent must be within to initiate attack on target
    public float attackCooldown = 3f;                       // time (in seconds) which agent waits after initiating an attack before pursuing
    public ChaseStates startingState = ChaseStates.Idle;    // state which pursuing agent starts in (typically Idle)
    public Vector4 attackHSV;                               // HSV agent's shader adjusts to when attacking
    public Vector4 cooldownHSV;                             // HSV agent's shader adjusts to when under cooldown
    public GameObject alertIndicator;                       // visual indicator shown when agent detects its target
    public Vector2 alertOffset;                             // relative offset alert display above agent

    // sound effect support
    AudioClipNames myAttackSound = AudioClipNames.agent_chaserAttack;       // sound played upon entering attack state
    AudioClipNames myAlertSound = AudioClipNames.agent_chaserAlert;         // sound played upon entering pursue state

    // support variables
    ChaseStates currState;              // current state of agent
    CircleCollider2D sightTrigger;      // trigger collider defining sight range of agent
    bool withinSightRange = false;      // whether target (i.e., player) is within sight range -- determines whether to raycast to target
    Vector4 standardHSV;                // HSV of agent's shader while idle
    Rigidbody2D myRigidbody;            // agent's rigidbody component
    SpriteRenderer mySpriteRenderer;    // agent's sprite renderer component
    Rigidbody2D targetRigidbody;        // target's rigidbody component
    Animator myAnimator;                // agent's animator component
    int ignoreLayerMask;                // physics layermask to ignore when performing raycasts
    float waitCounter = 0;              // counter used to facilitate attack cooldowns

    #endregion

    #region Properties

    /// <summary>
    /// Property with read/write access exposing maximum speed agent
    /// will move at.
    /// </summary>
    public float AgentSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }


    #endregion

    #region State Machine

    /// <summary>
    /// Updates agent as it waits idly
    /// </summary>
    void UpdateIdle()
    {
        // slow agent to halt and lerp color to normal
        myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, Vector2.zero, Time.deltaTime);
        mySpriteRenderer.material.SetVector("_HSVAAdjust",
            Vector4.Lerp(mySpriteRenderer.material.GetVector("_HSVAAdjust"), standardHSV, Time.deltaTime));

        // if agent can see target
        if (CanSeeTarget())
        {
            // set animation triggers
            myAnimator.SetTrigger("OnPursueTrigger");
            myAnimator.ResetTrigger("OnIdleTrigger");

            // move to pursue state
            currState = ChaseStates.Pursue;
            EnterPursue();
        }
    }

    /// <summary>
    /// Initiates agent's pursue state
    /// </summary>
    void EnterPursue()
    {
        // play agent's alert sound
        AudioManager.Play(AudioClipNames.agent_chaserAlert, true);

        // if agent isn't showing attack indicator, display one at agent's position
        if (transform.childCount < 1)
            Instantiate(alertIndicator, transform).transform.position += (Vector3)alertOffset;
    }

    /// <summary>
    /// Updates agent as it pursues its target
    /// </summary>
    void UpdatePursue()
    {
        // lerp agent's color to normal
        mySpriteRenderer.material.SetVector("_HSVAAdjust",
            Vector4.Lerp(mySpriteRenderer.material.GetVector("_HSVAAdjust"), standardHSV, Time.deltaTime));

        // find distance from agent to target
        float distToTarget = (transform.position - targetTransform.position).magnitude;

        // move agent to intercept target
        float targetDisplacementAtIntercept = (distToTarget / Mathf.Max(0.1f, myRigidbody.velocity.magnitude)) * targetRigidbody.velocity.magnitude;
        Vector3 interceptPoint = (Vector3)targetRigidbody.velocity.normalized * targetDisplacementAtIntercept + targetTransform.position;
        myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, (interceptPoint - transform.position).normalized * AgentSpeed, Time.deltaTime);

        // if agent can no longer see target
        if (!CanSeeTarget())
        {
            // set animation triggers
            myAnimator.SetTrigger("OnIdleTrigger");
            myAnimator.ResetTrigger("OnPursueTrigger");

            // move to idle animation
            currState = ChaseStates.Idle;
        }
        // but if agent is within attack range
        else if (distToTarget <= attackRange)
        {
            // set animation triggers
            myAnimator.SetTrigger("OnAttackTrigger");
            myAnimator.ResetTrigger("OnPursueTrigger");

            // move to attack state
            currState = ChaseStates.Attack;
            EnterAttack();
        }
    }

    /// <summary>
    /// Initiates an attack before entering agent's wait state
    /// </summary>
    void EnterAttack()
    {
        // set attack color and attack target, shaking camera
        mySpriteRenderer.material.SetVector("_HSVAAdjust", attackHSV);
        deductO2Event.Invoke(attackDamage, true);

        // play attack sound effect
        AudioManager.Play(myAttackSound, true);
    }

    /// <summary>
    /// Uninitializes attack state after completing animation
    /// </summary>
    void ExitAttack()
    {
        // darken agent and initializes cooldown timer
        mySpriteRenderer.material.SetVector("_HSVAAdjust", cooldownHSV);
        waitCounter = attackCooldown;

        // set animation triggers and transition to wait state
        myAnimator.SetTrigger("OnWaitTrigger");
        myAnimator.ResetTrigger("OnAttackTrigger");
        currState = ChaseStates.Wait;
    }

    /// <summary>
    /// Updates agent as it waits for cooldown before returning to idle state
    /// </summary>
    void UpdateWait()
    {
        // gradually slow agent to a halt
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
        return ((targetTransform.position - transform.position).magnitude <= sightRange) && 
            (Physics2D.Raycast(transform.position, targetTransform.position - transform.position, Mathf.Infinity, ignoreLayerMask).transform == targetTransform);
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
        sightTrigger = GetComponent<CircleCollider2D>();

        // initialize agent and set internal variables
        currState = startingState;
        sightTrigger.isTrigger = true;
        standardHSV = mySpriteRenderer.material.GetVector("_HSVAAdjust");
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

    /// <summary>
    /// Called once per frame
    /// </summary>
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

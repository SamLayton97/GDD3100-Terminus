using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls lifetime of alert status icon spawned when agent
/// detects its target.
/// </summary>
public class AgentAlert : MonoBehaviour
{
    // display variables
    [SerializeField]
    List<Sprite> alertSprites =    // list of sprites alert indicator will cycle through
        new List<Sprite>();

    // configuration variables
    [SerializeField] float flashTime;       // time (seconds) at which indicator cycles to next sprite
    [SerializeField] float lifetime;        // duration which indicator is visible

    // display support
    SpriteRenderer myRenderer;
    float flashCounter = 0;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // retrieve necessary components
        myRenderer = GetComponent<SpriteRenderer>();

        // if indicator is offscreen
        if (!myRenderer.isVisible)
        {
            // move indicator to within camera's periphery
            Debug.Log(Camera.main.transform.position);
            Vector2 fromCamera = Camera.main.transform.position - transform.position;


            Debug.LogError("throw");
        }

        // set self to detroy after lifetime
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // cycle through sprites over time
        flashCounter += Time.deltaTime;
        if (flashCounter >= flashTime)
        {
            // reset flash counter
            flashCounter = 0;

            // cycle to next frame, wrapping across list if necessary
            int spriteIndex = alertSprites.IndexOf(myRenderer.sprite) + 1;
            if (spriteIndex >= alertSprites.Count) spriteIndex = 0;
            myRenderer.sprite = alertSprites[spriteIndex];
        }
    }
}

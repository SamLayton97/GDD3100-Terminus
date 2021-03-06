﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Enumeration of mouse cursors and reticles
/// </summary>
public enum Cursors
{
    Standard,
    PistolReticle,
    ShotgunReticle,
    PhotonReticle,
    BioRifleReticle
}

/// <summary>
/// Enumerations of cursor variation states
/// </summary>
public enum CursorStates
{
    Standard,       // used when mouse button is unclicked
    Depressed,      // used when mouse button is held down
    Hostile         // used when user mouses over enemies
}

/// <summary>
/// Texture2D list wrapper class used to modify
/// a list of lists in the inspector
/// </summary>
[System.Serializable]
public class Texture2DListWrapper
{
    // list wrapped by class
    public List<Texture2D> myList;

    /// <summary>
    /// Read-write property accessing texture at a given index of list.
    /// Implements brackets accessor to behave similar to normal list.
    /// </summary>
    /// <param name="key">index of texture</param>
    /// <returns>texture2d at given index</returns>
    public Texture2D this[int key]
    {
        get { return myList[key]; }
        set { myList[key] = value; }
    }
}

/// <summary>
/// Singleton managing design and size of user's mouse cursor.
/// </summary>
public class CursorManager : MonoBehaviour
{
    // singleton variables
    static CursorManager instance;

    // cursor resources
    [SerializeField]
    List<Texture2DListWrapper> cursorTextures =     // 2D list of textures user's mouse cursor can change to throughout game
        new List<Texture2DListWrapper>();           // NOTE: rows must be entered as they appear in the Cursors enum
                                                    // columns must be entered in order of CursorStates enum

    // cursor type support variables
    List<List<Vector2>> hotspots =                          // 2D list of hotspots for each cursor type and state
        new List<List<Vector2>>(); 
    Cursors currCursor = Cursors.PistolReticle;             // current cursor used by player
    Cursors cursorOnResume = Cursors.PistolReticle;         // holds cursor to display when user resumes game
    CursorStates currState = CursorStates.Standard;         // current state of cursor used by player
    bool canSwitch = true;                                  // flag determining whether reticle can switch -- used to lock cursor type while game is paused

    // cursor depression support variables
    IEnumerable depressedCoroutine;                         // coroutine controlling depression of cursor
    [Range(0f, 0.3f)]
    [SerializeField] float depressionTime = 0.1f;           // realtime seconds cursor is locked in depressed state
    bool depressed = false;

    // hostile detection support variables
    RaycastHit2D hostileHit;

    #region Properties

    /// <summary>
    /// Static read-access property returning
    /// instance of cursor manager.
    /// </summary>
    public static CursorManager Instance
    {
        get { return instance; }
    }

    #endregion

    #region Unity Methods

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // if singleton has already initialized as another instance
        if (instance != null && Instance != this)
        {
            // destroy this instance and break
            Destroy(gameObject);
            return;
        }

        // set this object as instance of singleton
        instance = this;
        DontDestroyOnLoad(gameObject);

        // initialize hotspots for each cursor type
        foreach (Texture2DListWrapper wrapper in cursorTextures)
        {
            // set default hotspot for first cursor type (standard mouse cursor)
            if (cursorTextures.IndexOf(wrapper) == 0)
            {
                hotspots.Add(Enumerable.Repeat(Vector2.zero, wrapper.myList.Count()).ToList());
                continue;
            }

            // set hotspot as dead center for all reticle cursors (and their variations)
            List<Vector2> stateHotspots = new List<Vector2>();
            foreach (Texture2D cursor in wrapper.myList)
                stateHotspots.Add(new Vector2(cursor.width / 2f, cursor.height / 2f));

            hotspots.Add(stateHotspots);
        }

        // set starting cursor
        SetCursorType(Cursors.Standard);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // depress cursor on mouse button down
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            StartCoroutine(DepressCursor());

        // detect if user is mousing over hostile entity
        hostileHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, 1 << LayerMask.NameToLayer("Alien"));

        // change cursor state to reflect hit's hostility
        if (hostileHit.transform != null)
            SetCursorState(CursorStates.Hostile);
        else
            SetCursorState(CursorStates.Standard);

    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets user's mouse cursor to one of
    /// a given enumeration of cursors types
    /// </summary>
    /// <param name="newCursor">new cursor type to display</param>
    public void SetCursorType(Cursors newCursor)
    {
        // switch if allowed
        if (canSwitch)
        {
            currCursor = newCursor;
            Cursor.SetCursor(cursorTextures[(int)currCursor][(int)currState], hotspots[(int)currCursor][(int)currState], CursorMode.ForceSoftware);
        }
        // otherwise (weapon selected while game was paused)
        else
            // store new cursor type as one to restore on resume
            cursorOnResume = newCursor;
    }

    /// <summary>
    /// Sets user's mouse cursor to reflect whether player
    /// is interacting with weapons or in-game menus, restoring
    /// cursor to cursor before pause in former case.
    /// </summary>
    /// <param name="gamePaused">whether game is now paused</param>
    public void HandlePause(bool gamePaused)
    {
        // if game is paused
        if (gamePaused)
        {
            // save cursor before pause and lock cursor to standard mouse
            cursorOnResume = currCursor;
            SetCursorType(Cursors.Standard);
            canSwitch = false;
        }
        // otherwise, unlock and set cursor to type before pause
        else
        {
            canSwitch = true;
            SetCursorType(cursorOnResume);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets cursor's state to one of a given
    /// enumeration of states
    /// </summary>
    /// <param name="newState">new state of cursor</param>
    void SetCursorState(CursorStates newState)
    {
        // change state if button isn't currently depressed
        if (!depressed)
        {
            // play sound effect on initial mouse over of enemy
            if (newState == CursorStates.Hostile && currState != CursorStates.Hostile)
                AudioManager.Play(AudioClipNames.player_mouseOverHostile, true);

            currState = newState;
            Cursor.SetCursor(cursorTextures[(int)currCursor][(int)currState], hotspots[(int)currCursor][(int)currState], CursorMode.ForceSoftware);
        }
    }

    /// <summary>
    /// Locks cursor in depressed state for unscaled time
    /// </summary>
    /// <returns></returns>
    IEnumerator DepressCursor()
    {
        // set cursor's state to depressed
        SetCursorState(CursorStates.Depressed);
        depressed = true;
        yield return new WaitForSecondsRealtime(depressionTime);

        // return cursor to default state after waiting
        depressed = false;
        SetCursorState(CursorStates.Standard);
    }

    #endregion

}

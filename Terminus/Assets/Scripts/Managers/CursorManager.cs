using System.Collections;
using System.Collections.Generic;
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
    Standard,
    Depressed,
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
    List<Vector2> hotspots = new List<Vector2>();           // list of hotspots for each cursor type
    Cursors currCursor = Cursors.PistolReticle;             // current cursor used by player
    Cursors cursorBeforePause = Cursors.PistolReticle;      // holds cursor displayed before player paused game
    CursorStates currState = CursorStates.Standard;         // current state of cursor used by player

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

        // initialize hotspots of each cursor
        foreach (Texture2DListWrapper wrapper in cursorTextures)
        {
            // set default hotspot for first cursor type (standard mouse cursor)
            if (cursorTextures.IndexOf(wrapper) == 0)
            {
                hotspots.Add(Vector2.zero);
                continue;
            }

            // set hotspot as dead center for all reticle cursors
            hotspots.Add(new Vector2(wrapper[0].width / 2f, wrapper[0].height / 2f));
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
        currCursor = newCursor;
        Cursor.SetCursor(cursorTextures[(int)currCursor][(int)currState], hotspots[(int)currCursor], CursorMode.ForceSoftware);
    }
    
    /// <summary>
    /// Sets cursor's state to one of a given
    /// enumeration of states
    /// </summary>
    /// <param name="newState">new state of cursor</param>
    public void SetCursorState(CursorStates newState)
    {
        // change state if button isn't currently depressed
        if (!depressed)
        {
            currState = newState;
            Cursor.SetCursor(cursorTextures[(int)currCursor][(int)currState], hotspots[(int)currCursor], CursorMode.ForceSoftware);
        }
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
            // save cursor before pause and set cursor to standard mouse
            cursorBeforePause = currCursor;
            SetCursorType(Cursors.Standard);
        }
        // otherwise, restore cursor before pause
        else
            SetCursorType(cursorBeforePause);
    }

    #endregion

    #region Private Methods

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

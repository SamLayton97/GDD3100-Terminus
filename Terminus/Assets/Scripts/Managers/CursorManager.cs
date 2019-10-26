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
/// Singleton managing design and size of user's mouse cursor.
/// </summary>
public class CursorManager : MonoBehaviour
{
    // singleton variables
    static CursorManager instance;

    // cursor resources
    [SerializeField]
    List<Texture2D> cursorTextures = new List<Texture2D>();             // list of textures user's mouse cursor can change to throughout game
                                                                        // NOTE: items must be entered as they appear in the Cursors enum

    // support variables
    List<Vector2> hotspots = new List<Vector2>();                       // list of hotspots for each cursor type

    /// <summary>
    /// Static read-access property returning
    /// instance of cursor manager.
    /// </summary>
    public static CursorManager Instance
    {
        get { return instance; }
    }

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
        foreach (Texture2D cursor in cursorTextures)
        {
            // set default hotspot for first cursor (standard mouse cursor)
            if (cursorTextures.IndexOf(cursor) == 0)
            {
                hotspots.Add(Vector2.zero);
                continue;
            }

            // set hotspot as dead center for all reticle cursors
            hotspots.Add(new Vector2(cursor.width / 2f, cursor.height / 2f));
        }

        // set starting cursor
        SetCursor(Cursors.Standard);
    }

    /// <summary>
    /// Sets user's mouse cursor to one of
    /// a given enumeration of cursors
    /// </summary>
    /// <param name="newCursor">new cursor to display</param>
    public void SetCursor(Cursors newCursor)
    {
        Cursor.SetCursor(cursorTextures[(int)newCursor], hotspots[(int)newCursor], CursorMode.ForceSoftware);
    }

}

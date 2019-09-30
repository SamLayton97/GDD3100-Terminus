using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages playing and stopping of music throughout game
/// </summary>
public class MusicManager : MonoBehaviour
{
    // private variables
    static bool initialized = false;                        // flag determining whether object has been initialized
    static AudioSource myAudioSource;                       // reference to audio source to play sounds from
    static Dictionary<SongNames, AudioClip> tracks =        // dictionary pairing track names with audio clips
        new Dictionary<SongNames, AudioClip>();
    static Dictionary<string, SongNames> scenesToTracks =   // dictionary pairing track names with scenes
        new Dictionary<string, SongNames>();

    /// <summary>
    /// Read-access property returning whether manager has
    /// set its audio source
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes manager by setting an audio source
    /// and pairing music with scene names
    /// </summary>
    public static void Initialize(AudioSource audioSource)
    {
        // set audio source
        initialized = true;
        myAudioSource = audioSource;

        // load in music files form Resources/Music
        tracks.Add(SongNames.mus_menu, Resources.Load<AudioClip>("Music/mus_menu"));
        tracks.Add(SongNames.mus_gameplay, Resources.Load<AudioClip>("Music/mus_gameplay"));

        // pair scenes with songs
        scenesToTracks.Add("GameplayLevel1", SongNames.mus_gameplay);
    }

    /// <summary>
    /// Swaps background music to different track
    /// </summary>
    /// <param name="newTrack">name of new track to play</param>
    public static void SwitchTrack(SongNames newTrack)
    {
        // stop source, switch track, and restart source
        myAudioSource.Stop();
        myAudioSource.clip = tracks[newTrack];
        myAudioSource.Play();
    }

    /// <summary>
    /// Returns song associated with given scene, returning
    /// menu music if scene-song pair wasn't loaded into
    /// dictionary.
    /// </summary>
    /// <param name="sceneName">name of given scene</param>
    /// <returns>song to play</returns>
    public static SongNames GetSongFromScene(string sceneName)
    {
        // attempt to retrieve song by scene name
        try
        {
            return scenesToTracks[sceneName];
        }
        // if scene-song pair doesn't exist, return menu music
        catch
        {
            return SongNames.mus_menu;
        }
    }
}

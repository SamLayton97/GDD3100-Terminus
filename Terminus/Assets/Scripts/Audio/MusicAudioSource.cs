using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Audio source to play all music
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicAudioSource : MonoBehaviour
{
    // private variables
    AudioSource myAudioSource;

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // keep one game audio source object for entire game
        if (!MusicManager.Initialized)
        {
            // initialize audio source and make it persist across scenes
            myAudioSource = GetComponent<AudioSource>();
            MusicManager.Initialize(myAudioSource);
            DontDestroyOnLoad(gameObject);

            // add self as delegate of OnSceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            // destroy self if duplicate
            Destroy(gameObject);
    }

    /// <summary>
    /// Called whenever a new scene is loaded
    /// </summary>
    /// <param name="scene">scene loaded</param>
    /// <param name="mode">mode that scene was loaded under</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if music playing doesn't match track for scene, switch tracks
        SongNames trackForScene = MusicManager.GetSongFromScene(scene.name);
        if (myAudioSource.clip == null || myAudioSource.clip.name != trackForScene.ToString())
            MusicManager.SwitchTrack(trackForScene);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages playing and stopping of all audio clips excluding music
/// </summary>
public static class AudioManager
{
    // private variables
    static bool initialized = false;                            // flag determining whether object has been initialized
    static AudioSource myAudioSource;                           // reference to audio source to play sounds from
    static Dictionary<AudioClipNames, AudioClip> audioClips =   // dictionary pairing sound effects with names in enumeration
        new Dictionary<AudioClipNames, AudioClip>();

    /// <summary>
    /// Read-access property returning whether manager has
    /// read in audio clips and set its manager
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes manager by reading in audio clips from Resources\Sounds
    /// and setting the audio source to play clips from
    /// </summary>
    /// <param name="audioSource">source to play audio clips from</param>
    public static void Initialize(AudioSource audioSource)
    {
        // set source
        initialized = true;
        myAudioSource = audioSource;

        // load in sounds from Resources\Sounds

        #region Player Sounds

        audioClips.Add(AudioClipNames.player_hurt, Resources.Load<AudioClip>("Sounds/sfx_player_hurt"));
        audioClips.Add(AudioClipNames.player_hurt1, Resources.Load<AudioClip>("Sounds/sfx_player_hurt1"));
        audioClips.Add(AudioClipNames.player_hurt2, Resources.Load<AudioClip>("Sounds/sfx_player_hurt2"));

        #endregion

        #region Weapon Sounds

        audioClips.Add(AudioClipNames.weapon_shootPistol, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPistol"));
        audioClips.Add(AudioClipNames.weapon_shootPistol1, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPistol1"));
        audioClips.Add(AudioClipNames.weapon_shootPistol2, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPistol2"));

        #endregion

    }

    /// <summary>
    /// Plays audio clip by given name, looping if appropriate
    /// </summary>
    /// <param name="soundName">name of sound effect</param>
    /// <param name="dontLoop">whether to loop sound</param>
    public static void Play(AudioClipNames soundName, bool dontLoop)
    {
        // if sound doesn't loop, simply play once
        if (dontLoop)
            myAudioSource.PlayOneShot(audioClips[soundName]);
        // TODO: otherwise, create separate, controllable audio source and play from there
        else
            Debug.LogWarning("Warning: Loopable sounds not yet implemented.");
    }

}

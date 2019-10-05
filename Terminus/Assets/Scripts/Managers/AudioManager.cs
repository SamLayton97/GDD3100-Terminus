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
    /// read in audio clips and set its source
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

        // load in sounds from Resources/Sounds

        #region Enemy Agent Sounds

        audioClips.Add(AudioClipNames.agent_chaserAttack, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserAttack"));
        audioClips.Add(AudioClipNames.agent_chaserAlert, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserAlert"));
        audioClips.Add(AudioClipNames.agent_chaserDeath, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserDeath"));
        audioClips.Add(AudioClipNames.agent_chaserHurt, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserHurt"));
        audioClips.Add(AudioClipNames.agent_chaserHurt1, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserHurt1"));
        audioClips.Add(AudioClipNames.agent_chaserHurt2, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserHurt2"));

        #endregion

        #region Environmental Sounds

        audioClips.Add(AudioClipNames.env_playerWallCollide, Resources.Load<AudioClip>("Sounds/sfx_env_playerWallCollide"));
        audioClips.Add(AudioClipNames.env_airlockReached, Resources.Load<AudioClip>("Sounds/sfx_env_airlockReached"));
        audioClips.Add(AudioClipNames.env_collectOxygen, Resources.Load<AudioClip>("Sounds/sfx_env_collectOxygen"));
        audioClips.Add(AudioClipNames.env_pickUpWeapon, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpWeapon"));
        audioClips.Add(AudioClipNames.env_bioExplosion, Resources.Load<AudioClip>("Sounds/sfx_env_bioExplosion"));
        audioClips.Add(AudioClipNames.env_pickUpMaterial, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpMaterial"));
        audioClips.Add(AudioClipNames.env_pickUpBiomass, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpBiomass"));

        #endregion

        #region Player Sounds

        audioClips.Add(AudioClipNames.player_hurt, Resources.Load<AudioClip>("Sounds/sfx_player_hurt"));
        audioClips.Add(AudioClipNames.player_hurt1, Resources.Load<AudioClip>("Sounds/sfx_player_hurt1"));
        audioClips.Add(AudioClipNames.player_hurt2, Resources.Load<AudioClip>("Sounds/sfx_player_hurt2"));
        audioClips.Add(AudioClipNames.player_death, Resources.Load<AudioClip>("Sounds/sfx_player_death"));
        audioClips.Add(AudioClipNames.player_swapWeapon, Resources.Load<AudioClip>("Sounds/sfx_player_swapWeapon"));

        #endregion

        #region UI Sounds

        audioClips.Add(AudioClipNames.UI_buttonPress, Resources.Load<AudioClip>("Sounds/sfx_UI_buttonPress"));
        audioClips.Add(AudioClipNames.UI_buttonHighlight, Resources.Load<AudioClip>("Sounds/sfx_UI_buttonHighlight"));
        audioClips.Add(AudioClipNames.UI_gamePause, Resources.Load<AudioClip>("Sounds/sfx_UI_gamePause"));
        audioClips.Add(AudioClipNames.UI_gameUnpause, Resources.Load<AudioClip>("Sounds/sfx_UI_gameUnpause"));

        #endregion

        #region Weapon Sounds

        audioClips.Add(AudioClipNames.weapon_shootPistol, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPistol"));
        audioClips.Add(AudioClipNames.weapon_shootPistol1, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPistol1"));
        audioClips.Add(AudioClipNames.weapon_shootPistol2, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPistol2"));
        audioClips.Add(AudioClipNames.weapon_shootShotgun, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootShotgun"));
        audioClips.Add(AudioClipNames.weapon_shootShotgun1, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootShotgun1"));
        audioClips.Add(AudioClipNames.weapon_shootShotgun2, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootShotgun2"));
        audioClips.Add(AudioClipNames.weapon_shootPhoton, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootPhoton"));
        audioClips.Add(AudioClipNames.weapon_shootBioshot, Resources.Load<AudioClip>("Sounds/sfx_weapon_shootBioshot"));

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

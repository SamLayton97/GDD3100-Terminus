﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages playing and stopping of all audio clips excluding music
/// </summary>
public static class AudioManager
{
    // private variables
    static bool initialized = false;                                // flag determining whether object has been initialized
    static AudioSource myAudioSource;                               // reference to audio source to play sounds from
    static Dictionary<AudioClipNames, AudioClip> audioClips =       // dictionary pairing sound effects with names in enumeration
        new Dictionary<AudioClipNames, AudioClip>();
    static Dictionary<AudioClipNames, string> soundsToCaptions =    // dictionary pairing sound effects with captions
        new Dictionary<AudioClipNames, string>();                   // NOTE: not all sounds may have captions -- CC controller accounts for this

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

        // load in sounds from Resources/Sounds and pair sound effects with captions

        #region Atmospheric Sounds

        audioClips.Add(AudioClipNames.atm_kayagum, Resources.Load<AudioClip>("Sounds/sfx_atm_kayagum"));
        audioClips.Add(AudioClipNames.atm_piano, Resources.Load<AudioClip>("Sounds/sfx_atm_piano"));

        soundsToCaptions.Add(AudioClipNames.atm_kayagum, "[kayagum stings]");
        soundsToCaptions.Add(AudioClipNames.atm_piano, "[piano stings]");

        #endregion

        #region Enemy Agent Sounds

        audioClips.Add(AudioClipNames.agent_chaserAttack, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserAttack"));
        audioClips.Add(AudioClipNames.agent_chaserAlert, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserAlert"));
        audioClips.Add(AudioClipNames.agent_chaserDeath, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserDeath"));
        audioClips.Add(AudioClipNames.agent_chaserHurt, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserHurt"));
        audioClips.Add(AudioClipNames.agent_chaserHurt1, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserHurt1"));
        audioClips.Add(AudioClipNames.agent_chaserHurt2, Resources.Load<AudioClip>("Sounds/sfx_agent_chaserHurt2"));

        soundsToCaptions.Add(AudioClipNames.agent_chaserAttack, "[alien bites]");
        soundsToCaptions.Add(AudioClipNames.agent_chaserAlert, "[alien hisses]");
        soundsToCaptions.Add(AudioClipNames.agent_chaserDeath, "[alien dying]");
        soundsToCaptions.Add(AudioClipNames.agent_chaserHurt, "[alien gasps]");
        soundsToCaptions.Add(AudioClipNames.agent_chaserHurt1, "[alien gasps]");
        soundsToCaptions.Add(AudioClipNames.agent_chaserHurt2, "[alien gasps]");

        #endregion

        #region Environmental Sounds

        audioClips.Add(AudioClipNames.env_playerWallCollide, Resources.Load<AudioClip>("Sounds/sfx_env_playerWallCollide"));
        audioClips.Add(AudioClipNames.env_airlockReached, Resources.Load<AudioClip>("Sounds/sfx_env_airlockReached"));
        audioClips.Add(AudioClipNames.env_collectOxygen, Resources.Load<AudioClip>("Sounds/sfx_env_collectOxygen"));
        audioClips.Add(AudioClipNames.env_pickUpWeapon, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpWeapon"));
        audioClips.Add(AudioClipNames.env_bioExplosion, Resources.Load<AudioClip>("Sounds/sfx_env_bioExplosion"));
        audioClips.Add(AudioClipNames.env_pickUpMaterial, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpMaterial"));
        audioClips.Add(AudioClipNames.env_pickUpBiomass, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpBiomass"));
        audioClips.Add(AudioClipNames.env_pickUpEnergy, Resources.Load<AudioClip>("Sounds/sfx_env_pickUpEnergy"));

        soundsToCaptions.Add(AudioClipNames.env_airlockReached, "[airlock closes]");
        soundsToCaptions.Add(AudioClipNames.env_collectOxygen, "[cannister opens]");
        soundsToCaptions.Add(AudioClipNames.env_pickUpWeapon, "[weapon loads]");
        soundsToCaptions.Add(AudioClipNames.env_bioExplosion, "[bioshot bursts]");
        soundsToCaptions.Add(AudioClipNames.env_pickUpMaterial, "[scrap dings]");
        soundsToCaptions.Add(AudioClipNames.env_pickUpBiomass, "[biomass spatters]");
        soundsToCaptions.Add(AudioClipNames.env_pickUpEnergy, "[energy core buzzes]");

        #endregion
        
        #region Player Sounds

        audioClips.Add(AudioClipNames.player_hurt, Resources.Load<AudioClip>("Sounds/sfx_player_hurt"));
        audioClips.Add(AudioClipNames.player_hurt1, Resources.Load<AudioClip>("Sounds/sfx_player_hurt1"));
        audioClips.Add(AudioClipNames.player_hurt2, Resources.Load<AudioClip>("Sounds/sfx_player_hurt2"));
        audioClips.Add(AudioClipNames.player_death, Resources.Load<AudioClip>("Sounds/sfx_player_death"));
        audioClips.Add(AudioClipNames.player_swapWeapon, Resources.Load<AudioClip>("Sounds/sfx_player_swapWeapon"));
        audioClips.Add(AudioClipNames.player_oxygenWarning, Resources.Load<AudioClip>("Sounds/sfx_player_oxygenWarning"));
        audioClips.Add(AudioClipNames.player_distortionWarning, Resources.Load<AudioClip>("Sounds/sfx_player_distortionWarning"));
        audioClips.Add(AudioClipNames.player_mouseOverHostile, Resources.Load<AudioClip>("Sounds/sfx_player_mouseOverHostile"));
        audioClips.Add(AudioClipNames.player_generalAlert, Resources.Load<AudioClip>("Sounds/sfx_player_generalAlert"));
        
        soundsToCaptions.Add(AudioClipNames.player_hurt, "[astronaut gasps]");
        soundsToCaptions.Add(AudioClipNames.player_hurt1, "[astronaut wheezes]");
        soundsToCaptions.Add(AudioClipNames.player_hurt2, "[astronaut groans]");
        soundsToCaptions.Add(AudioClipNames.player_death, "[astronaut chokes]");
        soundsToCaptions.Add(AudioClipNames.player_swapWeapon, "[weapon cocks]");
        soundsToCaptions.Add(AudioClipNames.player_oxygenWarning, "LOW OXYGEN");
        soundsToCaptions.Add(AudioClipNames.player_distortionWarning, "DISTORTION DETECTED");
        soundsToCaptions.Add(AudioClipNames.player_generalAlert, "[alert pings]");

        #endregion

        #region UI Sounds

        audioClips.Add(AudioClipNames.UI_buttonPress, Resources.Load<AudioClip>("Sounds/sfx_UI_buttonPress"));
        audioClips.Add(AudioClipNames.UI_buttonHighlight, Resources.Load<AudioClip>("Sounds/sfx_UI_buttonHighlight"));
        audioClips.Add(AudioClipNames.UI_gamePause, Resources.Load<AudioClip>("Sounds/sfx_UI_gamePause"));
        audioClips.Add(AudioClipNames.UI_gameUnpause, Resources.Load<AudioClip>("Sounds/sfx_UI_gameUnpause"));
        audioClips.Add(AudioClipNames.UI_popMaterial, Resources.Load<AudioClip>("Sounds/sfx_UI_popMaterial"));
        audioClips.Add(AudioClipNames.UI_pushMaterial, Resources.Load<AudioClip>("Sounds/sfx_UI_pushMaterial"));
        audioClips.Add(AudioClipNames.UI_pushLastMaterial, Resources.Load<AudioClip>("Sounds/sfx_UI_pushLastMaterial"));
        audioClips.Add(AudioClipNames.UI_denied, Resources.Load<AudioClip>("Sounds/sfx_UI_denied"));

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

        soundsToCaptions.Add(AudioClipNames.weapon_shootPistol, "[rifle fires]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootPistol1, "[rifle fires]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootPistol2, "[rifle fires]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootShotgun, "[shotgun bursts]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootShotgun1, "[shotgun bursts]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootShotgun2, "[shotgun bursts]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootPhoton, "[photon whirs]");
        soundsToCaptions.Add(AudioClipNames.weapon_shootBioshot, "[geiger counter crackles]");

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

        // write closed captioning if enabled and sound has captions
        if (ClosedCaptions.Instance.ccEnabled && soundsToCaptions.ContainsKey(soundName))
            ClosedCaptions.Instance.DisplayCaptions(soundsToCaptions[soundName]);
    }

}

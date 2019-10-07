using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic input manager with customizable inputs.
/// Code based on input manager from:
/// https://forum.unity.com/threads/can-i-modify-the-input-manager-via-script.458800/
/// </summary>
public static class CustomInputManager
{
    // keyboard input storage
    static Dictionary<string, KeyCode> keyMappings = new Dictionary<string, KeyCode>
    {
        { "ShowHideCraftingMenu", KeyCode.Space },
        { "SelectWeapon1", KeyCode.A },
        { "SelectWeapon2", KeyCode.S },
        { "SelectWeapon3", KeyCode.D },
        { "SelectWeapon4", KeyCode.F },
    };

    // mouse button input storage
    static Dictionary<string, int> mouseButtonMappings = new Dictionary<string, int>
    {
        { "Fire", 0 },
        { "ShowHideCraftingMenu", 1 }
    };

    /// <summary>
    /// Sets name-to-keyboard input mapping to new key
    /// </summary>
    /// <param name="keyMapName">name of key mapping</param>
    /// <param name="newKey">key to map to</param>
    public static void SetKeyMap(string keyMapName, KeyCode newKey)
    {
        // throw exception if mapping doesn't exit
        if (!keyMappings.ContainsKey(keyMapName))
            Debug.LogError("ERROR: Invalid key mapping in SetKeyMap " + keyMapName);
        keyMappings[keyMapName] = newKey;
    }
}

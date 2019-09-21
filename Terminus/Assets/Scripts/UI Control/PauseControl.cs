using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pauses/unpauses game on user input
/// </summary>
public class PauseControl : MonoBehaviour
{
    // public variables
    public GameObject darkenGameOnPause;                // semi-transparent panel covering game when paused
    public GameObject pauseMenu;                        // pop-up pause menu

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if user attempts to pause game and game is not already paused
        if (Input.GetAxis("Pause") != 0 && Time.timeScale != 0)
        {
            // freeze game
            Time.timeScale = 0;

            // enable pause menu components
            darkenGameOnPause.SetActive(true);
            pauseMenu.SetActive(true);
        }
    }
}

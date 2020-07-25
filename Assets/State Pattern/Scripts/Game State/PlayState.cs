/*
* Jake Buri
* PlayState.cs
* Assignment 9
* GameState when the game is running
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : GameState
{
    //Variables
    GameManager gameManager;
    GameStateController gameStateController;

    void Start()
    {
        //Get the GameStateController and GameManager
        gameStateController = gameObject.GetComponent<GameStateController>();
        gameManager = GameManager.instance;
    }

    public override void Menu()
    {
        Debug.LogWarning("The game must be paused to reach the main menu");
    }

    public override void Pausing()
    {
        //Pause the game and enable the pause menu
        gameManager.pauseMenu.SetActive(true);
        gameManager.song.Pause();
        Time.timeScale = 0;

        //Change State to playing
        gameStateController.currentState = gameStateController.pauseState;
    }

    public override void Playing()
    {
        Debug.LogWarning("The game is already playing");
    }


    //Used to reset the song after it finishes
    public override void Restarting()
    {
        //Change the state to restarting
        gameStateController.currentState = gameStateController.restartState;
    }
}

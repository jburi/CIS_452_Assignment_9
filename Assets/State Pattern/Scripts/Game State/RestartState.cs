/*
* Jake Buri
* RestartState.cs
* Assignment 9
* Used to start and restart the game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartState : GameState
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

    //Used after the song finishes
    public override void Menu()
    {
        //Disable the pause menu and display the main menu
        gameManager.pauseMenu.SetActive(false);
        gameManager.mainMenu.SetActive(true);

        //Change state to menu
        gameStateController.currentState = gameStateController.menuState;
    }

    public override void Pausing()
    {
        Debug.Log("Wait for the game to start before pausing");
    }

    public override void Playing()
    {
        //if Pause -> Restart
        if (gameManager.pauseMenu.activeInHierarchy)
        {
            //Remove the pause menu and resume time
            gameManager.pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        //Playing the new chart
        StartCoroutine(Countdown());
    }

    public override void Restarting()
    {
        Debug.LogWarning("The game can only be restarted from the main and pause menus");
    }

    //Countdown before the game starts
    IEnumerator Countdown()
    {
        //Countdown starting number
        float timeRemaining = 3f;

        while (timeRemaining > 0)
        {
            //Update the text boxes
            gameManager.countdownText.text = timeRemaining.ToString();
            gameManager.songTitleText.text = "Epic Song - BoxCat Games";

            //Count one second
            yield return new WaitForSeconds(1);
            timeRemaining --;
        }

        //Start the game after the countdown
        StartGame();
    }

    public void StartGame()
    {
        //Used to start the game
        gameManager.StartSong();

        //Change state to playing
        gameStateController.currentState = gameStateController.playState;
    }
}

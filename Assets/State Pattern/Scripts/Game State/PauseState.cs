/*
* Jake Buri
* PauseState.cs
* Assignment 9
* GameState where the pause menu is displayed and the game is paused
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseState : GameState
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
        //Resume time
        Time.timeScale = 1;

        //Reset the chart and variabels
        ResetChart();
        gameManager.ResetVariables();

        //Disable the pause menu
        gameManager.pauseMenu.SetActive(false);

        //Display the main menu
        gameManager.mainMenu.SetActive(true);

        //Change state to menu
        gameStateController.currentState = gameStateController.menuState;
    }

    public override void Pausing()
    {
        //If the 'p' key is pressed again, the game will play
        Playing();
    }

    public override void Playing()
    {
        //Disable the pause menu
        gameManager.pauseMenu.SetActive(false);

        //Resume  the game
        gameManager.song.Play();
        Time.timeScale = 1;

        //Change State to playing
        gameStateController.currentState = gameStateController.playState;
    }

    public override void Restarting()
    {
        //Reset the chart and variables
        ResetChart();
        gameManager.ResetVariables();

        //Change state to Restarting
        gameStateController.currentState = gameStateController.restartState;
    }

    public void ResetChart()
    {
        //Destroy the old chart
        GameObject oldChart = GameObject.FindGameObjectWithTag("Chart");
        Destroy(oldChart);
        gameManager.song.Stop();

        //Create a new chart using the prefab and replace the chart in the game manager
        Instantiate(gameManager.reset);
    }
}

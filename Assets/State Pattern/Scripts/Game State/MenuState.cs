/*
* Jake Buri
* MenuState.cs
* Assignment 9
* Game state when the menu is displayed and the game is not started
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : GameState
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
        Debug.LogWarning("The menu is already displayed");
    }

    public override void Pausing()
    {
        Debug.LogWarning("The game has not started and can not be pause");
    }

    public override void Playing()
    {
        Debug.LogWarning("The game has not started so there is nothing to resume");
        
    }

    public override void Restarting()
    {
        //Disable the main menu
        gameManager.mainMenu.SetActive(false);

        //Change the state to start the chart
        gameStateController.currentState = gameStateController.restartState;
    }
}

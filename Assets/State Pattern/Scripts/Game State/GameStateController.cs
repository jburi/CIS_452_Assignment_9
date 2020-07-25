/*
* Jake Buri
* GameStateController.cs
* Assignment 9
* Holds all of the game states including the current one
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    //Getters and Setters for each state
    public GameState menuState { get; set; }
    public GameState playState { get; set; }
    public GameState pauseState { get; set; }
    public GameState restartState { get; set; }
    public GameState currentState { get; set; }


    // Initialize the states
    void Awake()
    {
        pauseState = gameObject.AddComponent<PauseState>();
        playState = gameObject.AddComponent<PlayState>();
        menuState = gameObject.AddComponent<MenuState>();
        restartState = gameObject.AddComponent<RestartState>();
    }

    //Used to call the methods of the current state
    public void Menu() { currentState.Menu(); }
    public void Playing() { currentState.Playing(); }
    public void Pausing() { currentState.Pausing(); }
    public void Restarting() { currentState.Restarting(); }
}

/*
* Jake Buri
* GameState.cs
* Assignment 9
* Abstract class for each GameState to use
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    //Functions that each of the GameStates will use
    public abstract void Menu();
    public abstract void Playing();
    public abstract void Pausing();
    public abstract void Restarting();
}

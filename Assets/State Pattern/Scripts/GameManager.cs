/*
* Jake Buri
* GameManager.cs
* Assignment 9
* Controls the GameStateController & holds all of the game variables
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Public Variables and Prefabs
    public AudioSource song;
    public Chart theChart;
    public GameObject reset;
    public GameObject pauseMenu;
    public GameObject mainMenu;

    //Debug State
    //public string theState;

    //Text Boxes
    public Text scoreText;
    public Text failText;
    public Text multiplierText;
    public Text multiplierCounterText;
    public Text countdownText;
    public Text highScoreText;
    public Text songTitleText;

    //State Machine
    public GameStateController gameStateController;

    //Button bools for state change
    private bool startPlaying;
    private bool restartGame;
    private bool pauseToMainMenu;
    private bool resumeGame;

    //Private Variables
    private int scorePerNote = 100;
    private int canMiss = 5;
    private int currentHighScore;
    private int currentScore;
    private int currentMultiplier;
    private int multiplierCounter;
    private int[] multiplierThresholds;
    private int hitCounter;

    //Singleton for easier reference in each GameState
    public static GameManager instance;

    void Start()
    {
        //Initialize varaibles
        instance = this;
        currentHighScore = 0;
        currentMultiplier = 1;
        multiplierCounter = 0;
        hitCounter = 0;
        multiplierThresholds = new int[] { 4, 8, 12 };

        //Initialize text boxes
        countdownText.text = "";
        scoreText.text = "Score: 0";
        highScoreText.text = "High Score: " + currentHighScore + "    " + 0 + "%";
        failText.text = "Can Miss: " + canMiss + " more notes";
        multiplierText.text = currentMultiplier + "x";
        multiplierCounterText.text = multiplierCounter + "/" + multiplierThresholds[currentMultiplier - 1];

        //Initialize booleans
        startPlaying = false;
        restartGame = false;
        pauseToMainMenu = false;
        resumeGame = false;
        

        //Ensure the main menu and starting state is set
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);

        //Fix WebGL Build Null Reference Error
        gameStateController = gameObject.AddComponent<GameStateController>();
        gameStateController.currentState = gameStateController.menuState;
    }

    void Update()
    {
        //Debug State
        //theState = gameStateController.currentState.ToString();

        //Check if the song finishes (Play -> Menu)
        if(song.isPlaying == false && gameStateController.currentState == gameStateController.playState)
        {
            //Update the high score
            UpdateHighScore();

            //Play -> Pause -> Restart
            gameStateController.pauseState.Restarting();

            //Restart -> Menu
            gameStateController.currentState.Menu();

        }

        //If you fail the game, return to the main menu
        if(canMiss == 0)
        {
            //Play -> Pause -> Restart
            gameStateController.pauseState.Restarting();

            //Restart -> Menu
            gameStateController.currentState.Menu();
        }

        //Toggles pausing useing the 'p' key
        if (Input.GetKeyDown("p"))
        {
            //Play -> Pause || Pause -> Play
            gameStateController.currentState.Pausing();
        }

        //UI button was clicked to resume the game from the pause menu
        if (resumeGame == true)
        {
            //Pause -> Play
            gameStateController.currentState.Playing();
            resumeGame = false;
        }

        //UI Button was clicked to start playing from the main menu
        if (startPlaying == true)
        {
            //Menu -> Restart
            gameStateController.currentState.Restarting();

            //Restart -> Play
            gameStateController.currentState.Playing();
            startPlaying = false;
        }

        //UI Button was clicked to restart the game from the pause menu 
        if (restartGame == true)
        {
            //Pause -> Restart
            gameStateController.currentState.Restarting();

            //Restart -> Playing
            gameStateController.currentState.Playing();
            restartGame = false;
        }

        //UI Button was clicked to return to the main menu from the pause menu
        if (pauseToMainMenu == true)
        {
            //Pause -> Menu
            gameStateController.currentState.Menu();
            pauseToMainMenu = false;
        }
    }

    //Update the score & multiplier is a note is hit
    public void NoteHit()
    {
        //Debug.Log("Note Hit");

        //Update the counter (Used for score percentage)
        hitCounter++;

        //Detect if you reached the max multiplier
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            //Update the counter
            multiplierCounter++;

            //Detect if you hit enough notes to multiply
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierCounter)
            {
                //Reset the counter & update the multiplier
                multiplierCounter = 0;
                currentMultiplier++;
            }
        }

        //Give back one missed note if less than 10 remaining
        if (canMiss < 5)
        {
            canMiss++;
        }

        //Update the multiplier and multiplier counter text
        multiplierText.text = currentMultiplier + "x";
        multiplierCounterText.text = multiplierCounter + "/" + multiplierThresholds[currentMultiplier - 1];

        //Update the score using the multiplier
        currentScore += scorePerNote * currentMultiplier;

        //Update the score and fail text
        scoreText.text = "Score: " + currentScore;
        failText.text = "Can Miss: " + canMiss + " more notes";
    }

    //Resets the multiplier and text if you miss a note
    public void NoteMissed()
    {
        //Debug.Log("Note Missed");

        //Reset multiplier and counter
        currentMultiplier = 1;
        multiplierCounter = 0;

        //Update miss notes counter
        canMiss--;

        //Reset text
        failText.text = "Can Miss: " + canMiss + " more notes";
        multiplierText.text = currentMultiplier + "x";
        multiplierCounterText.text = multiplierCounter + "/" + multiplierThresholds[currentMultiplier - 1];
    }

    public void StartSong()
    {
        //If there is a new chart, find it here because timeScale = 0 in pause state
        if(theChart == null)
        {
            theChart = GameObject.FindGameObjectWithTag("Chart").GetComponent<Chart>();
        }

        //Remove the countdown and song title
        countdownText.text = "";
        songTitleText.text = "";

        //Start the chart and song
        theChart.start = true;
        song.Play();
    }

    //Updates the high score
    public void UpdateHighScore()
    {
        //Check if you beat the previous high score
        if(currentScore >= currentHighScore)
        {
            //Update the high score
            currentHighScore = currentScore;

            //Calculate the percentage of notes hit
            float percentage = ((float)hitCounter / 43) * 100;

            //Update the high score text
            highScoreText.text = "High Score: " + currentHighScore + "    " + (int)percentage + "%";
        }
    }

    //Resets the score and multiplier
    public void ResetVariables()
    {
        //Note Missed resets the multiplier and text box
        NoteMissed();

        //Reset counters
        currentScore = 0;
        hitCounter = 0;
        canMiss = 5;

        //Update the text boxes
        scoreText.text = "Score: " + currentScore;
        failText.text = "Can Miss: " + canMiss + " more notes";
    }


    //Attach to UI Button (Menu -> Restart)
    public void MenuToPlay()
    {
        startPlaying = true;
    }

    //Attach to UI Button (Pause -> Restart)
    public void RestartGame()
    {
        restartGame = true;
    }

    //Attach to UI Button (Pause -> Play)
    public void ResumeGame()
    {
        resumeGame = true;
    }

    //Attach to UI Button (Pause -> Menu)
    public void PauseToMenu()
    {
        pauseToMainMenu = true;
    }
}

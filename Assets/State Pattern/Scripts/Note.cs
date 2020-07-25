/*
* Jake Buri
* Note.cs
* Assignment 9
* Used to detect if the note can be hit, if it is missed, and what key it is bound to.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    //Variables
    public KeyCode key;
    private bool canHitNote;

    //Checks if the player hit the note or not
    void Update()
    {
        //Check if the key is pressed
        if (Input.GetKeyDown(key))
        {
            //Check if the note can be hit
            if (canHitNote)
            {
                //Remove the note and call the fucntion in the GameManager
                gameObject.SetActive(false);

                GameManager.instance.NoteHit();
            }
        }
    }

    //Checks if the note is over the fret
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Allows the note to be hit
        if(collision.tag == "Fret")
        {
            canHitNote = true;
        }
    }

    //Checks if the note is no longer over the fret
    private void OnTriggerExit2D(Collider2D collision)
    {
        //The note can no longer be hit and returns a miss
        if (collision.tag == "Fret")
        {
            canHitNote = false;

            if (gameObject.activeInHierarchy)
            {
                GameManager.instance.NoteMissed();
            }
        }
    }
}

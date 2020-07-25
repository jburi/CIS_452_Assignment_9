/*
* Jake Buri
* ButtonControls.cs
* Assignment 9
* Lets the player know the fret is pressed by changing the color
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControls : MonoBehaviour
{
    //Public Variables
    public Color idle;
    public Color highlighted;
    public KeyCode key;

    //Private Variable
    private SpriteRenderer sr;

    private void Start()
    {
        //Get the SpriteRenderer
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Update the Color if the key is pressed or not
        if (Input.GetKeyDown(key))
        {
            sr.color = highlighted;
        }
        if (Input.GetKeyUp(key))
        {
            sr.color = idle;
        
        }
    }
}

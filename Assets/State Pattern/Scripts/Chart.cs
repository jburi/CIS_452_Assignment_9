/*
* Jake Buri
* Chart.cs
* Assignment 9
* Holds the notes and transforms them down
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chart : MonoBehaviour
{
    //Variables
    public float tempo;
    public bool start;

    void Start()
    {
        //Initialize the variables
        tempo /= 60f;
        start = false;
    }

    void Update()
    {
        //Transform the notes down when the game starts
        if (start)
        {
            transform.position -= new Vector3(0f, tempo * Time.deltaTime, 0f);
        }
        
    }
}

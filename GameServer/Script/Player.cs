using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public bool isGame;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username; 
        isGame = false;

    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {



    }




}

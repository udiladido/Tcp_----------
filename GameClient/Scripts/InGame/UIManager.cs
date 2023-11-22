using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public GameObject LobbyMenu;
    public GameObject InGame;
    public GameObject EndMenu;
    public InputField usernameField;


    public bool isGaming;
    public bool Gameready = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        LobbyMenu.SetActive(true);
        usernameField.interactable = false;
        Client.instance.ConnectToServer();
    }


    public void readyCheck()
    {

        Gameready = !Gameready;

        int ready;

        if (Gameready)
            ready = 1;
        else
            ready = -1;

        GameManager.instance.readyCountnum(ready);
        ClientSend.ReadyCheck(ready);

    }

   


    public void EndGame()
    {

            InGame.SetActive(false);
            EndMenu.SetActive(true);

        
    }



 


}

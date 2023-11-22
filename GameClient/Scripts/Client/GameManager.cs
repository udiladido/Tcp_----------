using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public int oppId;


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

    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialized(_id, _username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }




  


    public void readyCountnum(int _ready)
    {

        InGameManager a;
        a = GetComponent<InGameManager>();
        a.checkReady(_ready);

        if ((a.maxCount == a.readyCount) && (a.maxCount != 0 && a.readyCount != 0))
        {
            StartCoroutine(Delay(1f));
            
        }


    }

    public void oppChar(int index)
    {

        InGameManager a;
        a = GetComponent<InGameManager>();
        a.oppindex = index;
    
    
    }


    IEnumerator Delay( float waitTiem)
    {

        while(true)
        { 
         yield return new WaitForSeconds(waitTiem);
            GameStart();

        }
        

    }


    public void GameStart()
    {


        UIManager.instance.LobbyMenu.SetActive(false);
        UIManager.instance.InGame.SetActive(true);
        UIManager.instance.isGaming = true;
        InGameManager.inst.GameStart();

    }


    public void LinkCountnum(int _maxPlayer)
    {
        InGameManager a;
        a = GetComponent<InGameManager>();
        a.LinkCheck(_maxPlayer);



    }


  



}

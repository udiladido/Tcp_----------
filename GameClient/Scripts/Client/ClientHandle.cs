using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;




public class ClientHandle : MonoBehaviour
{
    public SerializeCharacterData serializeCharacterData { get; private set; }

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();


        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        //Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation);

    
    }



    public static void spawnChar(Packet _packet)
    {
       
        int charData = _packet.ReadInt();
        GameManager.instance.oppChar(charData);


    }


    public static void charCode(Packet _packet)
    { 
      int charCode = _packet.ReadInt();

        InGameManager.inst.SpawnOpp(charCode);
    
    }

    public static void playReady(Packet _packet)
    {

        int _isreadyCount = _packet.ReadInt();
        GameManager.instance.readyCountnum(_isreadyCount);


    }

    public static void maxPlayer(Packet _packet)
    {

        int _maxPlayer = _packet.ReadInt();
        GameManager.instance.LinkCountnum(_maxPlayer);
        
    
    }


    public static void PlayerHealth(Packet _packet)
    {

        int _health = _packet.ReadInt();
        InGameManager.inst.DecreaseHeart_opp();


    }



    public static void TargetMerge(Packet _packet)
    { 
    
        int _target = _packet.ReadInt();
        int _index  =  _packet.ReadInt();



        GameObject[] EnemyAll = GameObject.FindGameObjectsWithTag("CharacterOpp");
        GameObject targetObj = Array.Find(EnemyAll, x => x.GetComponent<CharacterOpp>().serializeCharacterData.index == _index);
        
        
       var targetCharacter = targetObj.GetComponent<CharacterOpp>();
       targetCharacter.oppmerge(_target);

    }



    public static void TargetOpp(Packet _packet)
    {
     
        int _target = _packet.ReadInt(); // 적
        int _index = _packet.ReadInt(); // 누가 쏘는지
        int _star = _packet.ReadInt(); // 별 위치 

  

        GameObject[] EnemyAll = GameObject.FindGameObjectsWithTag("CharacterOpp");
        GameObject targetObj = Array.Find(EnemyAll, x => x.GetComponent<CharacterOpp>().serializeCharacterData.index == _index);


        var targetCharacter = targetObj.GetComponent<CharacterOpp>();
        targetCharacter.AttackCo(_target, _star);

    }



    public static void playerDisconneted(Packet _packet)
    {
        int _id = _packet.ReadInt();
        int maxplayer = _packet.ReadInt();
        GameManager.instance.LinkCountnum(maxplayer);

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
       

    }



}

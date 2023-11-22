using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class ServerHandle
{



    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].SendIntoGame(_username);

    }




    public static void ReadyCheck(int _fromClient, Packet _packet)
    {

        int _isready = _packet.ReadInt();


        if (_fromClient == 1)
            _fromClient = 2;
        else
            _fromClient = 1;

        ServerSend.playReady(_fromClient, _isready);


    }


    public static void playerHealth(int _fromClient, Packet _packet)
    {


        int health = _packet.ReadInt();

        if (_fromClient == 1)
            _fromClient = 2;
        else
            _fromClient = 1;

        ServerSend.playerHealth(_fromClient, health);
    }



    public static void spawnChar(int _fromClient, Packet _packet)
    {


        int charData = _packet.ReadInt();
       
        if (_fromClient == 1)
            _fromClient = 2;
        else
            _fromClient = 1;

        ServerSend.spawnChar(_fromClient, charData);



    }

    public static void charCode(int _fromClient, Packet _packet)
    { 
     
        int oppCode = _packet.ReadInt();

        if (_fromClient == 1)
            _fromClient = 2;
        else
            _fromClient = 1;

        ServerSend.charCode(_fromClient, oppCode);


    }

    public static void TargetOpp(int _fromClient, Packet _packet)
    {

        if (_fromClient == 1)
            _fromClient = 2;
        else
            _fromClient = 1;

        int target = _packet.ReadInt();
        int index = _packet.ReadInt();
        int star = _packet.ReadInt();


        ServerSend.TargetOpp(_fromClient, target, index, star);
    }

    public static void TargetMerge(int _fromClient, Packet _packet)
    {

        if (_fromClient == 1)
            _fromClient = 2;
        else
            _fromClient = 1;

        int target = _packet.ReadInt();
        int index = _packet.ReadInt();

        ServerSend.TargetMerge(_fromClient, target, index);
    }


}

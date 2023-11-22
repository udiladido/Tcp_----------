﻿

using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ServerSend
{


    /// <summary>Sends a packet to a client via TCP.</summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to a client via UDP.</summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    /// <summary>Sends a packet to all clients via TCP.</summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    /// <summary>Sends a packet to all clients except one via TCP.</summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }

    /// <summary>Sends a packet to all clients via UDP.</summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    /// <summary>Sends a packet to all clients except one via UDP.</summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }

    #region Packets
    /// <summary>Sends a welcome message to the given client.</summary>
    /// <param name="_toClient">The client to send the packet to.</param>
    /// <param name="_msg">The message to send.</param>
    public static void Welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    /// <summary>Tells a client to spawn a player.</summary>
    /// <param name="_toClient">The client that should spawn the player.</param>
    /// <param name="_player">The player to spawn.</param>
    public static void SpawnPlayer(int _toClient, Player _player)
    {

        using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
        {
            _packet.Write(_player.id);
            _packet.Write(_player.username);

            SendTCPData(_toClient, _packet);
        }

    }


    public static void playReady(int _toClient, int _readyCount)
    {

        using (Packet _packet = new Packet((int)ServerPackets.playReady))
        {

            _packet.Write(_readyCount);
            SendTCPData(_toClient, _packet);
        }

    }


    public static void playerHealth(int _toClient, int health)
    {

        using (Packet _packet = new Packet((int)ServerPackets.playerHealth))
        {

            _packet.Write(health);

            SendTCPData(_toClient, _packet);

        }


    }

    public static void maxPlayer(int playernum)
    {

        using (Packet _packet = new Packet((int)ServerPackets.maxPlayer))
        {
            _packet.Write(playernum);

            SendTCPDataToAll(_packet);

        }

    }


    public static void spawnChar(int _toClient, int charData)
    {

        using (Packet _packet = new Packet((int)ServerPackets.spawnChar))
        {

            _packet.Write(charData);

            SendTCPData(_toClient, _packet);
        }


    }



    public static void charCode(int _toClient, int _code)
    {

        using (Packet _packet = new Packet((int)ServerPackets.charCode))
        {
            _packet.Write(_code);

            SendTCPData(_toClient, _packet);

        }



    }

    public static void TargetOpp(int _toClient, int _target, int _index, int _star)
    {

        using (Packet _packet = new Packet((int)ServerPackets.TargetOpp))
        {
            _packet.Write(_target);
            _packet.Write(_index);
            _packet.Write(_star);

            SendTCPData(_toClient, _packet);

        }


    }


    public static void TargetMerge(int _toClient, int _target, int _index)
    {

        using (Packet _packet = new Packet((int)ServerPackets.TargetMerge))
        {
            _packet.Write(_target);

            _packet.Write(_index);
            SendTCPData(_toClient, _packet);

        }


    }


    public static void playerDisconnected(int _playerId)
    {

        using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
        {

            _packet.Write(_playerId);
            SendTCPDataToAll(_packet);

        }
    
    
    }



    #endregion
}

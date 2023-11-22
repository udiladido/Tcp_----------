using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }



    public static void spawnChar(int charData)
    {

         using (Packet _packet = new Packet((int)ClientPackets.spawnChar))
        {

           _packet.Write(charData);
     
        
            SendTCPData(_packet);

        }



    }

    public static void charCode(int _Code)
    {

        using (Packet _packet = new Packet((int)ClientPackets.charCode))
        {
            _packet.Write(_Code);
        
            SendTCPData(_packet);
        }
    


    }


    public static void TargetOpp(int target, int index, int star)
    {
        using (Packet _packet = new Packet((int)ClientPackets.TargetOpp))
        {


            _packet.Write(target);
            _packet.Write(index);
            _packet.Write(star);

            SendTCPData(_packet);
        }
    
    
    
    }

    public static void TargetMerge(int _merge, int _self)
    {
        using (Packet _packet = new Packet((int)ClientPackets.TargetMerge))
        {
            _packet.Write(_merge);
            _packet.Write(_self);


            SendTCPData(_packet);

        }
    }




    public static void playerHealth(int heart)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerHealth))
        {
            _packet.Write(heart);

            SendTCPData(_packet);
       
        }
   
    }

  


    public static void ReadyCheck(int ready)
    {
        using (Packet _packet = new Packet((int)ClientPackets.ReadyCheck))
        {
     
           _packet.Write(ready);
 
            SendTCPData(_packet);

        }


    }

   

    #endregion
}

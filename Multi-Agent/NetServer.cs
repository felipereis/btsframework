using System.Collections.Generic;
using UnityEngine;
using Ruffles.Channeling;
using Ruffles.Connections;
using Ruffles.Core;
using System.Text;
using System;

// API that communicates with the agent
[System.Serializable]
public class NetServer : MonoBehaviour
{
    RuffleSocket socket;
    Connection clientConnection = null;
    int messagesSent = 0;
    public List<RotationDTO> rotations;
    NetworkEvent @event;

    readonly string uuid = "b6af0cc1-4e8d-43cd-b771-321125895433";

    public bool IsConnected { get; private set; }

    private void Start()
    {
        StartServer();
    }

    private void StartServer()
    {
        IsConnected = false;
        socket = new RuffleSocket(new Ruffles.Configuration.SocketConfig()
        {
            ChallengeDifficulty = 20, // Difficulty 20 is fairly hard
            ChannelTypes = new ChannelType[]
            {
                ChannelType.Reliable,
                ChannelType.ReliableSequenced,
                ChannelType.ReliableSequencedFragmented,
                ChannelType.Unreliable,
                ChannelType.UnreliableOrdered
            },
            AllowBroadcasts = true,
            AllowUnconnectedMessages = true,
            DualListenPort = 5557
        });
        socket.Start();
    }

    void Update()
    {
        @event = socket.Poll();
        if (@event.Type == NetworkEventType.Nothing) //nothing received
        {
            @event.Recycle();
            return;
        }
        else if
            (@event.Type == NetworkEventType.BroadcastData) //received broadcast, if the uuid matches, respond to client
        {
            string message = Encoding.UTF8.GetString(@event.Data.Array, @event.Data.Offset, @event.Data.Count);
            if (message.Equals(uuid))
            {
                socket.SendUnconnected(@event.Data, @event.EndPoint);
            }

            Debug.Log("Broadcast: " + @event.EndPoint.AddressFamily.ToString() + " send " +
                      System.Text.Encoding.UTF8.GetString(@event.Data.Array));
            // We got a broadcast. Reply to them with the same token they used
        }

        else if (@event.Type == NetworkEventType.Connect) //received connection request
        {
            clientConnection = @event.Connection;
            Debug.Log(@event.Connection.State + "to " + @event.EndPoint.AddressFamily.ToString());
            IsConnected = true;
        }
        else if (@event.Type == NetworkEventType.Data) //the client's commands go here
        {
            string message = Encoding.UTF8.GetString(@event.Data.Array, @event.Data.Offset, @event.Data.Count);
            OnMessageReceived(message);
        }

        // Recycle the event
        @event.Recycle();
    }

    // Message received from agent
    void OnMessageReceived(string message)
    {
        QuaternionListFromAgent(message);
    }

    // Send a message to the agent
    public void SendMessageToAgent(string message, byte channel = 1)
    {
        ArraySegment<byte> data =
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetBytes(message).Length);

        clientConnection?.Send(data, channel, false, (ulong)messagesSent);
        messagesSent++;
    }

    // Quaternion list sent by agent
    public void QuaternionListFromAgent(string message)
    {
        if (message.StartsWith("Quaternions/"))
        {
            string json = message.Substring(12);
            JsonUtility.FromJsonOverwrite(json, this);

            foreach (RotationDTO rdto in rotations)
            {
                Quaternion q = new Quaternion(rdto.rotation.x, rdto.rotation.y, rdto.rotation.z, rdto.rotation.w);
                GameObject.Find(rdto.name).transform.rotation = q;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ruffles.Core;
using Ruffles.Channeling;
using Ruffles.Connections;
using System;
using System.Linq;
using System.Net;
using System.Text;

// API that communicates with the multi-agent
[Serializable]
public class NetClient : MonoBehaviour
{
    private RuffleSocket socket;

    // UUID of the application, must be the same on the client and on the server, it serves to identify the network
    readonly string uuid = "b6af0cc1-4e8d-43cd-b771-321125895433";

    Connection serverConnection;

    bool isConnected;

    bool isDiscovering = true;

    // True if the program is in the server discovery process
    public bool IsDiscovering
    {
        get { return isDiscovering; }
    }

    NetworkEvent @event;
    int messagesSent;

    // Start is called before the first frame update
    void Start()
    {
        // Initializes the client
        socket = new RuffleSocket(new Ruffles.Configuration.SocketConfig()
        {
            ChallengeDifficulty = 20, // Difficulty 20 is fairly hard
            ChannelTypes = new[]
            {
                ChannelType.Reliable,
                ChannelType.ReliableSequenced,
                ChannelType.Unreliable,
                ChannelType.UnreliableOrdered,
            },
            AllowBroadcasts = true,
            AllowUnconnectedMessages = true,
        });
        socket.Start();

        StartCoroutine(Discover());
        StartCoroutine(SendBroadcast());
    }

    // Send network broadcast on port 5556 with UUID
    IEnumerator SendBroadcast()
    {
        while (IsDiscovering)
        {
            ArraySegment<byte> data =
                new ArraySegment<byte>(Encoding.UTF8.GetBytes(uuid), 0, Encoding.UTF8.GetBytes(uuid).Length);
            socket.SendBroadcast(data, 5557);
            Debug.Log("Sending " + data + " to " + 5557);
            yield return new WaitForSeconds(2);
        }
    }

    // Wait for a reply to the broadcast, the connection is successful only if the same UUID sent is received
    IEnumerator Discover()
    {
        while (IsDiscovering)
        {
            yield return new WaitForSeconds(1);
            @event = socket.Poll();
            if (@event.Type == NetworkEventType.Nothing)
            {
                @event.Recycle();
                continue;
            }

            if (@event.Type == NetworkEventType.UnconnectedData)
            {
                string message = Encoding.UTF8.GetString(@event.Data.Array, @event.Data.Offset, @event.Data.Count);
                if (message.Equals(uuid))
                {
                    isDiscovering = false;
                    serverConnection = socket.Connect(@event.EndPoint);
                    isConnected = true;
                    StartCoroutine(SendData());

                    yield return new WaitForSeconds(5);
                    //Connection ok
                    break;
                }
            }
        }
    }

    // Message received from multi-agent core
    public void OnMessageReceived(string messageServer)
    {
        // do something
    }


    // Send a message to the multi-agent core
    // Channel 1 - ChannelType.ReliableSequenced
    public void SendMessageToCore(string message, byte channel = 1)
    {
        ArraySegment<byte> data =
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(message), 0, Encoding.UTF8.GetBytes(message).Length);
        serverConnection?.Send(data, channel, false, (ulong)messagesSent);
        messagesSent++;
    }

    // Coroutine that sends data when it is connected. Sending at 30 frames per second.
    IEnumerator SendData()
    {
        while (isConnected)
        {
            SendQuaternionList();
            yield return new WaitForSeconds(1 / 30f);
        }
    }

    void Update()
    {
        if (isConnected)
        {
            @event = socket.Poll();
            if (@event.Type == NetworkEventType.Nothing) //nothing received
            {
                @event.Recycle();
                return;
            }

            else if (@event.Type == NetworkEventType.Data) //the client's commands go here
            {
                string message = Encoding.UTF8.GetString(@event.Data.Array, @event.Data.Offset, @event.Data.Count);
                OnMessageReceived(message);
            }

            // Recycle the event
            @event.Recycle();
        }
    }

    // Send quaternion data to other platforms
    public void SendQuaternionList()
    {
        string a = "Quaternions/{\"rotations\":[";
        List<RotationDTO> rt = new List<RotationDTO>();
        var rotatableObjects = FindObjectsOfType<RotatableObject>().Where(ro => ro.bsnDevice != null);

        foreach (RotatableObject ro in rotatableObjects)
        {
            var transform1 = ro.transform;
            var rotation = transform1.rotation;
            var quaternion = new QuaternionDTO(rotation.x, rotation.y,
                rotation.z, rotation.w);
            rt.Add(new RotationDTO(ro.SimplifiedName, quaternion));
        }

        rt.ForEach(rotation => { a += JsonUtility.ToJson(rotation) + ","; });

        a = a.TrimEnd(',') + "]}";
        SendMessageToCore(a, 1);
    }
}

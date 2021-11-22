/*
Based on: 
Two-way communication between Python 3 and Unity (C#) - Y. T. Elashry (2020) - Apache License 2.0
Unity3D to MATLAB UDP communication - Sandra Fang (2016)
*/

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpSocket : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; //localhost
    [SerializeField] int receivePort = 8000; //receive from python 
    [SerializeField] int sendPort = 8001; //send to python

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread

    Game game;

    public void SendData(string text)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    void Awake()
    {
        // Create remote endpoint (to Matlab) 
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), sendPort);

        // Create local client
        client = new UdpClient(receivePort);

        // local endpoint define (where messages are received)
        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialize (seen in comments window)
        Debug.Log("UDP Socket started");

    }

    private void Start()
    {
        game = FindObjectOfType<Game>();
    }

    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                string text = Encoding.UTF8.GetString(data);
                ProcessData(text);
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ProcessData(string text)
    {
        // PROCESS INPUT RECEIVED STRING HERE
        game.ReceiveInput(text); // Update text by string received from python

        if (!isTxStarted) // First data arrived so tx started
        {
            isTxStarted = true;
        }
    }

    //Prevent crashes - close clients and threads properly!
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }

}
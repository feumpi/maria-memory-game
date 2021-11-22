using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    string receivedText;
    UdpSocket udpSocket;
 
    // Start is called before the first frame update
    void Start()
    {
        udpSocket = FindObjectOfType<UdpSocket>();
        udpSocket.SendData("Start(): Sent from game");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveInput(string text)
    {
        Debug.Log(text);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    string receivedText;
    UdpSocket udpSocket;

    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject calibrationScreen;

    SelectableArea area1, area2, area3;

    // Start is called before the first frame update
    void Start()
    {
        udpSocket = FindObjectOfType<UdpSocket>();
        udpSocket.SendData("Start(): Sent from game");

        area1 = gameScreen.transform.Find("SelectableArea1").gameObject.GetComponent<SelectableArea>();
        area2 = gameScreen.transform.Find("SelectableArea2").gameObject.GetComponent<SelectableArea>();
        area3 = gameScreen.transform.Find("SelectableArea3").gameObject.GetComponent<SelectableArea>();

        //StartCoroutine(Calibrate());

        //StartCoroutine(PlayVideo());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveInput(string text)
    {
        Debug.Log(text);

        bool isNumeric = int.TryParse(text, out int index);

        if (isNumeric)
        {

            switch (index)
            {

                case 0:
                    area1.selected = false;
                    area2.selected = false;
                    area3.selected = false;
                    break;
                case 1:
                    area1.selected = false;
                    area2.selected = false;
                    area3.selected = true;
                    break;
                case 2:
                    area1.selected = false;
                    area2.selected = true;
                    area3.selected = false;
                    break;
                case 3:
                    area1.selected = true;
                    area2.selected = false;
                    area3.selected = false;
                    break;
            }
        }
    }

    IEnumerator Calibrate()
    {

        gameScreen.SetActive(false);
        calibrationScreen.SetActive(true);

        yield return new WaitForSeconds(2);

        calibrationScreen.SetActive(false);
        gameScreen.SetActive(true);

        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {

        SceneManager.LoadScene("VideoPlayer", LoadSceneMode.Additive);

        yield return new WaitForSeconds(5);

        SceneManager.UnloadSceneAsync("VideoPlayer");

    }
}



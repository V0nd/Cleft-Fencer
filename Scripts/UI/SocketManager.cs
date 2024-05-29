using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SocketManager : MonoBehaviour
{
    public IntValue playerSockets;
    public Image socketBar;
    private float maxFillAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageSockets();
    }

    void ManageSockets()
    {
        socketBar.fillAmount = maxFillAmount - (float)playerSockets.currentValue/10;
    }
}

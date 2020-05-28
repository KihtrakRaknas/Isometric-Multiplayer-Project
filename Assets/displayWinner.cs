using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using static control;


public class displayWinner : MonoBehaviourPun

{
    // Start is called before the first frame update
    public Text winText;
    public bool isMain;
    public static string winner = "";
    int counter = 0;
    void Start()
    {
        isMain = GetComponent<control>().isMain;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (winner != "")
        {
            winText.text = winner + " won!";
            counter++;
            if (counter > 100)
                winner = "";
        }
    }
}

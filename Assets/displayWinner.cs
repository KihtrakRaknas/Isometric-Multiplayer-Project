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
    void Start()
    {
        isMain = GetComponent<control>().isMain;
    }

    // Update is called once per frame
    void Update()
    {
        if (winner != "")
            winText.text = winner + " won!";
    }
}

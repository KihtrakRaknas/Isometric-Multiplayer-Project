using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
            NetworkManager.instance.instantiatePlayer(this.playerPrefab.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

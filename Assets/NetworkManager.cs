using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            this.gameObject.SetActive(false);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.JoinLobby();
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangedScene (string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }


    void OnJoinRoomFailed()
    {
        print("room join failed");
    }

    void OnJoinedRoom()
    {
        print("room joined");
    }
}

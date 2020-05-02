﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Button JoinBtn;
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
    void OnClick_CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("basic", options, TypedLobby.Default);
    }

    void Start()
    {
        Button btn = JoinBtn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick_CreateRoom);
        print("Connecting to server");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player" + Random.Range(0, 999).ToString();
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangedScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }


    public void OnJoinRoomFailed()
    {
        print("room join failed");
    }

    public override void OnJoinedRoom()
    {
        print("room joined");
    }

}

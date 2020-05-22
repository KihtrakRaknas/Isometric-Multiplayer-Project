using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    public Button JoinBtn;
    public static NetworkManager instance;
    public Text Username_field;
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

        string roomID = Username_field.text.ToString() != "" ? Username_field.text.ToString() : "forgotToTypeARoomCode";
        makeRoomWithID(roomID);
    }

    public void makeRoomWithID(string id)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        print(id);
        PhotonNetwork.JoinOrCreateRoom(id, options, TypedLobby.Default);
    }

    public void instantiatePlayer(string name)
    {
        PhotonNetwork.Instantiate(name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    public void spawnBullet(Vector3 pos, Quaternion rotaton)
    {
        PhotonNetwork.Instantiate("lazers", pos, rotaton, 0);
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
        if (PhotonNetwork.IsMasterClient)
            ChangedScene("Game");
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);
    }

}
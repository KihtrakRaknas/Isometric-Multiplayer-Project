
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoomListObj : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text text;
    public RoomInfo RoomInfo;
    // Start is called before the first frame update
    public void setRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        if (roomInfo.PlayerCount == 0)
            Destroy(this.gameObject);
        text.text = roomInfo.Name + " (" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + ")";
    }
    void OnMouseDown()
    {
        NetworkManager.instance.makeRoomWithID(RoomInfo.Name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("clicked");
        NetworkManager.instance.makeRoomWithID(RoomInfo.Name);
    }
}
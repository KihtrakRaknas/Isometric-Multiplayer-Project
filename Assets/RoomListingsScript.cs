using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsScript : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private RoomListObj roomListObj;

    private List<RoomListObj> listingz = new List<RoomListObj>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo roomInfo in roomList)
        {

            int index = listingz.FindIndex(x => x.RoomInfo.Name == roomInfo.Name); //ARROW FUNCTIONS!
            if (index != -1)
            {
                if (roomInfo.RemovedFromList)
                {
                    Destroy(listingz[index].gameObject);
                    listingz.RemoveAt(index);
                }
                else
                {
                    listingz[index].setRoomInfo(roomInfo);
                }
            }else { 
                RoomListObj listing = (RoomListObj)Instantiate(roomListObj, content);
                if (listing != null) {
                    listing.setRoomInfo(roomInfo);
                    listingz.Add(listing);
                }
            }
        }
    }
}

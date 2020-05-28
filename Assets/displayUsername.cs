using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;


public class displayUsername : Photon.MonoBehaviour
{
    private GameObject textGo;
    private TextMesh tm;
    public bool DisableOnOwnObjects;

    void Start()
    {
        if (tm == null) 
        {
            textGo = gameObject.GetComponentInChildren().gameObject;
            tm = textGo.GetComponent();
        }
    }

    void Update()
    {
        bool showInfo = !this.DisableOnOwnObjects || this.photonView.isMine;
        if (textGo != null)
        {
            textGo.SetActive(showInfo);
        }
        if (!showInfo)
        {
            return;
        }
        PhotonPlayer owner = this.photonView.owner;
        if (owner != null)
        {
            tm.text = (string.IsNullOrEmpty(owner.NickName)) ? "player" + owner.ID : owner.NickName;
        }
        else if (this.photonView.isSceneView)
        {
            tm.text = "scn";
        }
        else
        {
            tm.text = "n/a";
        }
    }
}

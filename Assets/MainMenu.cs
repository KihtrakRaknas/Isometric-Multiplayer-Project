using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Button JoinBtn;
    public Text Username_field;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = JoinBtn.GetComponent<Button>();
        btn.onClick.AddListener(JoinRoom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void JoinRoom()
    {
        string roomID = Username_field.text.ToString();
        print(roomID);
        NetworkManager.instance.JoinRoom(roomID);
    }
}

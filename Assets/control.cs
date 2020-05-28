using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviourPun
{
    public GameObject text;
    float xscale = 0;
    float zscale = 0;
    bool canShoot = true;
    public bool isMain = false;
    bool multiplayerHasStarted = false;
    public string username = "Player";
    string winner = "";
    // Start is called before the first frame update
    void Start()
    {
        username = GetComponent<PhotonView>().Owner.NickName;
        displayWinner.winner = "";
    }

    void resetShootBool()
    {
        canShoot = true;
    }
        // Update is called once per frame
        private void FixedUpdate()
    {
        
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;
        text.transform.LookAt(Camera.main.transform.position - v);
        text.transform.Rotate(0, 180, 0);
        text.GetComponent<TextMesh>().text = username;
        if (transform.position.y < -100)
            kill();
        if (isMain)
        {
            float movementSpeed = 5;
            float speedDecrease = 1.05f;
            if (canShoot && Input.GetKey("space"))
            {
                canShoot = false;
                Invoke("resetShootBool", .2f);
                NetworkManager.instance.spawnBullet(this.transform.position, transform.rotation);
                //clone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 10);
            }
            if (Input.GetKey("down"))
            {
                xscale += -movementSpeed;
            }
            else if (Input.GetKey("up"))
            {
                xscale += movementSpeed;
            }
            else
            {
                xscale /= speedDecrease;
            }
            if (Input.GetKey("left"))
            {
                zscale += movementSpeed;
            }
            else if (Input.GetKey("right"))
            {
                zscale += -movementSpeed;
            }
            else
            {
                zscale /= speedDecrease;
            }
            if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
            {
                if (Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.001)
                    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
            }
        }
    }
    void Update()
    {
        if(multiplayerHasStarted && GameObject.FindGameObjectsWithTag("playerObj").Length == 1)
        {
            NetworkManager.instance.changeRoom();
            winner = GameObject.FindGameObjectsWithTag("playerObj")[0].GetComponent<PhotonView>().Owner.NickName;
            if (isMain)
                winner = "you";
            print(winner);
            displayWinner.winner = winner;
            multiplayerHasStarted = false;
        }
        else
        {
            if(GameObject.FindGameObjectsWithTag("playerObj").Length != 1)
                multiplayerHasStarted = true;
        }

        if (isMain)
        {
            var camera = Camera.main;

            var forward = camera.transform.forward;
            var right = -1 * camera.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            Vector3 vex = (forward * xscale + right * zscale) * Time.deltaTime / 10;
            transform.position+=vex;
            Vector3 movement = new Vector3(vex.x, 0.0f, vex.z);
            if (movement != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(movement);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            isMain = false;
            Invoke("kill", 3);
            transform.Rotate(new Vector3(90, 0, 0));
        }
    }
    void kill()
    {
        Destroy(this.gameObject);
    }
}
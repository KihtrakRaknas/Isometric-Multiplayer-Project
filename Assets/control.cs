using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject projectile;
    float xscale = 0;
    float zscale = 0;
    bool canShoot = true;
    public bool isMain = false;
    bool multiplayerHasStarted = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void resetShootBool()
    {
        canShoot = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float movementSpeed = 5;
        float speedDecrease = 1.05f;
        if (canShoot && Input.GetKey("space"))
        {
            canShoot = false;
            Invoke("resetShootBool", .2f);
            print("space");
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
            print("left");
            zscale += movementSpeed;
        }
        else if (Input.GetKey("right"))
        {
            print("right");
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
    void Update()
    {
        if(multiplayerHasStarted && GameObject.FindGameObjectsWithTag("playerObj").Length == 1)
        {
            NetworkManager.instance.changeRoom();
        }
        else
        {
            multiplayerHasStarted = true;
        }

        if (isMain)
        {
            var x = this.transform.position.x;
            var y = this.transform.position.y;
            var z = this.transform.position.z;
            x += xscale * Time.deltaTime / 10;
            z += zscale * Time.deltaTime / 10;
            this.transform.position = new Vector3(x, y, z);
            Vector3 movement = new Vector3(xscale, 0.0f, zscale);
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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public GameObject projectile;
    float xscale = 0;
    float zscale = 0;
    bool canShoot = true;
    public bool isMain = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void resetShootBool()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMain)
        {
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
                xscale += -1;
            }
            else if (Input.GetKey("up"))
            {
                xscale += 1;
            }
            else
            {
                xscale /= 1.02f;
            }
            if (Input.GetKey("left"))
            {
                print("left");
                zscale += 1;
            }
            else if (Input.GetKey("right"))
            {
                print("right");
                zscale += -1;
            }
            else
            {
                zscale /= 1.02f;
            }

            if (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))
            {
                if (Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.001)
                    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
            }
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
            Destroy(this.gameObject);
        }
    }
}

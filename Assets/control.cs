using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    public Rigidbody projectile;
    float xscale = 0;
    float zscale = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")) {
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
        else if (Input.GetKey("down")) {
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
            if(Mathf.Abs(this.GetComponent<Rigidbody>().velocity.y) < 0.001)
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
        }
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var z = this.transform.position.z;
        x += xscale * Time.deltaTime/10;
        z += zscale * Time.deltaTime / 10;
        this.transform.position = new Vector3(x, y, z);
    }
}

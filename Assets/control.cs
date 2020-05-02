using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("down")){

        }
        else if (Input.GetKey("up"))
        {

        }
        else if (Input.GetKey("left"))
        {
            print("left");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z-1*Time.deltaTime);
        }
        else if (Input.GetKey("right"))
        {
            print("right");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1 * Time.deltaTime);
        }
    }
}

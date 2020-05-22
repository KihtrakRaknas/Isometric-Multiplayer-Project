using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("kill",10);
    }
    void kill()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 20;
    }
}

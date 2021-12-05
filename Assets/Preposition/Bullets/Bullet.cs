using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 forward;
    // Start is called before the first frame update
    void Start()
    {
        forward = this.transform.forward.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position+=forward*10*Time.deltaTime;
    }
}

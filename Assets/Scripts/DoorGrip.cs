using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrip : OVRGrabbable{
    void Update(){

    }

    public void OnCollisionEnter(Collision other) {
        Debug.Log("touch DoorGrip!");
    }
}
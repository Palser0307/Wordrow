using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnable : MonoBehaviour{
    void Start(){
        GetComponent<Camera>().enabled = false;
        GetComponent<Camera>().enabled = true;
    }
}

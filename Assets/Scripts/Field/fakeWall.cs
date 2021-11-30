using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeWall : MonoBehaviour {
    protected Story_Controller sc = null;

    private void Start() {
        if(GameObject.Find("System_Scripts").TryGetComponent(out sc)){
            Debug.Log("fakeWall.cs : start() finished.");
        }
    }

    private void Update(){
        if((bool)sc.fm.getFlag("useFly")){
            this.gameObject.SetActive(false);
        }
    }
}
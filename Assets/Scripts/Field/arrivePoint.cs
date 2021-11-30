using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 目的地に到着を感知するためのスクリプト

public class arrivePoint : MonoBehaviour {
    // 位置の名前をインスペクタからロード
    [SerializeField]
    protected string pointName = null;

    protected Story_Controller sc = null;

    private void Start() {
        Invoke(nameof(sc_setup), 1.0f);
    }

    void sc_setup(){
        sc = GameObject.Find("System_Scripts").GetComponent<Story_Controller>();
        if(sc==null){
            outputError("Story_Controller is not FOUND!!");
        }
    }

    // 接触判定
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            sc.fm.setFlag("arrivePoint"+pointName, true);
        }
    }
    void outputLog(string str){
        Debug.Log("arrivePoint"+pointName+" : "+str);
    }
    void outputError(string str){
        Debug.LogError("arrivePoint"+pointName+" : "+str);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 目的地に到着を感知するためのスクリプト
// ColliderのOnTriggerはTrueにしておくこと

public class arrivePoint : MonoBehaviour {
    // 位置の名前をインスペクタからロード
    [SerializeField]
    protected string pointNum = null;

    protected Story_Controller sc = null;

    private void Start() {
        // scの割り当て
        if(GameObject.Find("System_Scripts").TryGetComponent(out sc)){
            outputLog("OK");
        }else{
            outputLog("false");
        }
    }

    // 接触判定
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            sc.fm.setFlag("arrivePoint"+pointNum, true);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            sc.fm.setFlag("arrivePoint"+pointNum, true);
            outputLog("true");
        }
    }
    void outputLog(string str){
        Debug.Log("arrivePoint"+pointNum+" : "+str);
    }
    void outputError(string str){
        Debug.LogError("arrivePoint"+pointNum+" : "+str);
    }
}
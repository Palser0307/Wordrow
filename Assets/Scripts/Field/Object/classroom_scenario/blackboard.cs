using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 出席簿にアタッチする
public class blackboard : MonoBehaviour {
    protected Story_Controller sc = null;
    public void Start() {
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            Debug.LogError("blackboard: cant found sc");
        }
        return;
    }
    private void Update(){
        if(this.sc == null){
            GameObject ss = GameObject.Find("System_Scripts");
            if(ss == null){
                return;
            }
            if(!ss.TryGetComponent(out this.sc)){
                //Debug.LogError("blackboard: cant found sc");
            }else{
                Debug.Log("blackboard: can found sc");
            }
        }
        return;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Card"){
            if(other.gameObject.TryGetComponent(out InstantCard ic)){
                if(ic.getCardName() == "Clean"){
                    this.sc.fm.setFlag("clearBoard", 1);
                }else{
                    this.sc.fm.setFlag("clearBoard", 2);
                }
            }
        }
    }
}
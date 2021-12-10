using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 出席簿にアタッチする
public class blackboard : MonoBehaviour {
    protected Story_Controller sc = null;
    private void Start() {
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            Debug.LogError("blackboard: cant found sc");
        }
        return;
    }
    private void Update(){
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 出席簿にアタッチする
public class attendanceBook : MonoBehaviour {
    protected Story_Controller sc = null;
    private void Start() {
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            Debug.LogError("attendanceBook: cant found sc");
        }
        return;
    }
    private void Update(){
        return;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Card"){
            if(other.gameObject.TryGetComponent(out InstantCard ic)){
                if(ic.getCardName() == "Read"){
                    this.sc.fm.setFlag("readBook", 1);
                }else{
                    this.sc.fm.setFlag("readBook", 2);
                }
            }
        }
    }
}
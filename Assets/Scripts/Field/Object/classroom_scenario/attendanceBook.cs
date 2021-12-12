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
        Debug.Log("attendanceBook: OnCollisionEnter() start");
        if(other.gameObject.tag == "Card"){
            Debug.Log("attendanceBook: other have Card tag.");
            if(other.gameObject.TryGetComponent(out InstantCard ic)){
                if(ic.getCardName() == "Read"){
                    Debug.Log("attendanceBook: Card is \"Read\"");
                    this.sc.fm.setFlag("readBook", 1);// 正解フラグ
                    playAudio();
                }else{
                    Debug.Log("attendanceBook: Card is not \"Read\"");
                    this.sc.fm.setFlag("readBook", 2);// 不正解フラグ
                }
            }
        }
    }
    protected void playAudio(){
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
    }

}
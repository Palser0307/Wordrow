using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabChoices : MonoBehaviour{
    [SerializeField]
    protected string choiceTime = "";
    public int choiceNumber = 0;

    protected Story_Controller sc = null;

    private void Start(){
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            Debug.LogError("attendanceBook: cant found sc");
        }
        return;
    }

    protected void OnCollisionEnter(Collision other){
        this.sc.fm.setFlag(choiceTime, choiceNumber);
        Debug.Log(" on collison enter to "+other.gameObject.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_class_mod : MonoBehaviour{
    private string cardName;

    // target tag list
    private string[] targetList;


    private string triggerType;

    void Start(){
        this.cardName = "";
        this.targetList = new string[];
    }


}

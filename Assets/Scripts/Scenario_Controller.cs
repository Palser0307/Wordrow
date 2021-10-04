using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_Controller : MonoBehaviour{
    // シナリオ status
    // alpha : あーちゃんセリフ待ち
    //
    public static string status;
    // Start is called before the first frame update
    void Start(){
        status = "alpha";
    }

    // Update is called once per frame
    void Update(){
        // this.statusを各Scriptに反映
        switch (status){
            case "alpha" :
                Alpha_Controller.setStatus("active");
                break;
            default:
                break;
        }
    }
}

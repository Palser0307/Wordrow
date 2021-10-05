using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_Controller : MonoBehaviour{
    // シナリオ status
    // alpha : あーちゃんセリフ待ち
    // wait : プレイヤー動作待ち
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
            case "wait":

                break;
            default:
                break;
        }
    }

    // 指定プレハブの出現
    void appearPrefab(){
        // 中身未定
    }
}

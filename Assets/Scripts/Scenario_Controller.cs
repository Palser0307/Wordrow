using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シナリオ管理スクリプト
// Object:System_Scripts にadd Componentされる

public class Scenario_Controller : MonoBehaviour{
    // シナリオ status
    // alpha : あーちゃんセリフ待ち
    // wait : プレイヤー動作待ち
    public static string status;

    // フラグ管理スクリプト
    // 中のフラグは全てstaticの予定
    // 各シナリオごとに参照先が異なる
    // tutorial_flags t_flags = GetComponent<tutorial_flags>();

    void Start(){
        status = "alpha";
    }

    // Update is called once per frame
    void Update(){
        // this.statusを各Scriptに反映
        switch (status){
            case "alpha" :
                //Alpha_Controller.Status = "active";
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

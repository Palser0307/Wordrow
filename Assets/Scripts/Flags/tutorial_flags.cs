using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// チュートリアル(縮小版)のフラグ管理スクリプト
// もうデモ版と呼ぶべきでは？

public class tutorial_flags : MonoBehaviour{
    // フラグがここに書かれる
    // 必ずstatic
    // publicかどうかは要相談？

    // 焚火の火が大きくなったフラグ
    public static bool Bonfire_is_big = false;

    // 焚火が消えたフラグ
    public static bool Bonfire_is_out = false;

    // ドアが置かれたフラグ
    public static bool Door_appear = false;

    void Start(){
    }

    void Update(){
    }

    public bool getDoorAppear(){
        return Door_appear;
    }
    public void setDoorAppear(bool temp){
        Door_appear = temp;
    }
}

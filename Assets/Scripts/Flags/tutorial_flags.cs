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
    // 未使用
    public static bool Bonfire_is_big = false;

    // 焚火が消えたフラグ
    // 未使用
    public static bool Bonfire_is_out = false;

    // ドアが置かれたフラグ
    // 未使用
    public static bool Door_appear = false;

    // あーちゃん
    protected Alpha_Controller alpha_Controller;

    void Start(){
        alpha_Controller = this.gameObject.GetComponent<Alpha_Controller>();
    }

    void Update(){
        if(Metamorphose == true){
            //Debug.Log("蝶☆合体！パピヨン！");
        }
    }

    public bool getDoorAppear(){
        return Door_appear;
    }
    public void setDoorAppear(bool temp){
        Door_appear = temp;
    }

    // 握っているか？
    private bool canGrab=false;
    public bool CanGrab{
        set{
            this.canGrab = value;
        }
        get{
            return this.canGrab;
        }
    }

    // 蝶☆合体
    private bool metamorphose = false;
    public bool Metamorphose{get; set;} // これで簡易実装

    // 木が燃える
    private bool burningTree = false;
    public bool BurningTree{get; set;}
}

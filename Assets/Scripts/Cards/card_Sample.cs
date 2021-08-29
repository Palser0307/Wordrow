using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントが足りない場合，自動追加
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(BoxCollider))]

// カードサンプル
// "Fly" : 上空に射出
// tag:"ball" と接触時，鉛直方向に力学的エネルギーを付加する
// 鉛直投げ上げ運動 みたいな？
public class card_Sample : card_class{
    [SerializeField]
    GameObject cardObject;

    // 親コンストラクタと違う処理書いてるから
    // 頭にnewをつけておく
    new protected void Start(){
        // 親のコンストラクタ呼出し
        base.Start();
        Debug.Log("card_Sample.Start");
        setCardName("Fly");
        addTargetList("Ball");
        setTriggerType("DC");
    }

    new protected void Update(){
        return;
    }

    // 接触反応式
    // Trigger condition = DC
    /*
    public override void OnCollisionEnter(Collision other){
        if(triggerType == "DC"){
            this.use(other);
        }
    }
    */


    // 実行関数
    public override void use(Collision other){
        string tag = other.gameObject.tag;
        // target tag : Ball
        if(!haveTargetTag(tag)){
            Debug.Log("NOT Ball...");
            return;
        }
        Debug.Log("IT IS BALL!!!! YEAAAAAAHHHHHHH!!!!!");
        // 接触時，上方向にボール射出
        // 具体的にはLocal上方向に運動量付加

        // 上方向指定
        Vector3 direction = Vector3.up;

        other.gameObject.GetComponent<Rigidbody>().AddForce(direction*10f, ForceMode.Impulse);
    }
}

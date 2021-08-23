using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カードサンプル
// "Shoot" : 射出
// tag:"ball" と接触時，鉛直方向に力学的エネルギーを付加する
// 鉛直投げ上げ運動 みたいな？
public class card_Sample : card_class{
    void Start(){
        setName("Shoot");
        setType("Hit");
    }

    void Update(){

    }

    public override void use(Collision other){
        Debug.Log("use Shoot Card!");
        if(other.gameObject.tag != "Ball"){
            Debug.Log("NOT Ball...");
            return;
        }
        // 接触時，上方向にボール射出
        // 具体的にはLocal上方向に運動量付加

        // 上方向指定
        Vector3 direction = Vector3.up;

        other.gameObject.GetComponent<Rigidbody>().AddForce(direction*1f, ForceMode.Impulse);
    }
}

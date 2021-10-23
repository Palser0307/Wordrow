using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 人差し指トリガー発動型魔法カード用継承元クラス
// 持った状態でトリガーを引くことで発動
// TO = Triggered Operation

// 必須コンポーネントを補填
// 重力判定
[RequireComponent(typeof(Rigidbody))]
// 掴めるように
[RequireComponent(typeof(OVRGrabbable))]
// 箱型当たり判定
[RequireComponent(typeof(BoxCollider))]
public class TO_card_class : MonoBehaviour{
    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){

    }
}

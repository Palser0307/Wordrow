using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 第二世代用アップグレード
// 複数形のs
// 

// 必須コンポーネントを補填
// 物理判定
[RequireComponent(typeof(Rigidbody))]
// 掴めるように
[RequireComponent(typeof(OVRGrabbable))]
// 箱型当たり判定
[RequireComponent(typeof(BoxCollider))]

public class multiple : MonoBehaviour{
    // +--------+
    // | values |
    // +--------+

    // くっついたカードのオブジェクト
    protected GameObject Card;

    // くっついたカードのスクリプト
    // 少なくとも変数としては宣言できないわ

    // +-----------+
    // | functions |
    // +-----------+

    // 初期設定
    protected void Start(){
        setCardName("Multiple");
    }

    // run per frame
    protected void Update(){
        updateIsHold();

        callUse();
    }

    // カードだったらひっつく処理
    protected void OnCollisionEnter(Collision other) {
        GameObject obj = other.gameObject;
        // タグを見て，カードだったらひっつく
        if(obj.tag == "Card"){
            obj.transform.parent = this.transform;
            Card = obj;
            
            // TODO: 具体的にくっつく処理を作る

        }
    }

    public void use(Collision other){
        // カードのuse()を呼び出す
    }

    protected void callUse(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            this.use();
        }
    }

    // TODO: GETTER / SETTER の宣言，定義
}
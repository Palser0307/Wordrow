using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントが足りない場合，自動追加
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(BoxCollider))]

// カード第二弾
// "Rain" : 辺り一帯に雨を降らせる
// 右人差し指トリガーで発動
public class card_Rain : card_class{
    [SerializeField]
    GameObject cardObject;
    [SerializeField]
    GameObject Rain_Prefab;

    // world内Rainオブジェクトの格納先
    protected GameObject Rain_Object;

    // 親コンストラクタと違う処理書いてるから
    // 頭にnewをつけとく
    new protected void Start(){
        // 親コンストラクタ呼出し
        base.Start();
        Debug.Log("card_Rain.Start");
        setCardName("Rain");
        setTriggerType("TO");
        Rain_Object = null;
    }

    new protected void Update(){
        if(this.transform.parent != null && !this.getIsHold()){
            setIsHold(true);
        }
        if(this.transform.parent == null && this.getIsHold()){
            setIsHold(false);
        }

        // 手に持ってなかったら以降省略
        if(!this.getIsHold()){
            return;
        }
        // トリガー発動型
        if(this.getTriggerType() == "TO"){
            // 右人差し指トリガーで発動
            if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                this.use();
            }
        }
    }

    public override void use(Collision other = null){
        if(Rain_Object == null){
            Debug.Log("Rain appear");
            Rain_Object = Instantiate(Rain_Prefab);
            Rain_Object.transform.position = new Vector3(0.0f, 10.0f, 0.0f);
            Invoke(nameof(DelayMethod), 10.0f);
        }else{
            Destroy(Rain_Object);
            Rain_Object = null;
            CancelInvoke();
        }
    }

    protected void DelayMethod(){
        Debug.Log("a few seconds later : Destroy");
        Destroy(Rain_Object);
    }
}

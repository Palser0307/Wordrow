using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントが足りない場合，自動追加
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(BoxCollider))]

// ドア出現カード
// "Door" : 前方にドア設置
// 視線方向1mちょい先にドア出現
//
public class card_Door : card_class{
    [SerializeField]
    GameObject cardObject;

    [SerializeField]
    GameObject Door_Prefab;

    // world内Doorオブジェクトの格納先
    protected GameObject Door_Object = null;

    // 親コンストラクタと違う処理書いてるから
    // 頭にnewをつけておく
    new protected void Start(){
        // 親のコンストラクタ呼出し
        base.Start();
        Debug.Log("card_Door.Start");
        setCardName("Door");
        addTargetList("None");
        setTriggerType("TO");
    }

    new protected void Update(){
        if(this.transform.parent != null && !this.getIsHold()){
            setIsHold(true);
        }
        if(this.transform.parent == null && this.getIsHold()){
            setIsHold(false);
        }

        // 手に持ってなかったら，以降の処理省略
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

    // 実行関数
    public override void use(Collision other = null){
        if(Door_Object == null){
            Debug.Log("Door appear");
            Door_Object = Instantiate(Door_Prefab);


            //
            // とりあえず固定位置に出現
            //
            Door_Object.transform.position = new Vector3(1.0f, 1.0f, 1.0f);


            // 10sec後にDelayMethodを実行
            Invoke(nameof(DelayMethod), 10.0f);
        }else{
            Destroy(Door_Object);
            Door_Object = null;
            CancelInvoke();
        }
    }

    // 一定時間後発動するDestroy関数
    protected void DelayMethod(){
        Debug.Log("Door_Object : Destroy");
        Destroy(Door_Object);
        Door_Object = null;
    }
}

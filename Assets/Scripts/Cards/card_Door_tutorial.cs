using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントが足りない場合，自動追加
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(BoxCollider))]

// チュートリアル用
// ドア出現カード
// "Door" : 前方にドア設置
// 実際はドア前に設置したフェイクのドアをInactiveにするだけ
//
public class card_Door_tutorial : card_class{
    [SerializeField]
    GameObject cardObject;

    [SerializeField]
    GameObject Door_Prefab;

    // world内Doorオブジェクトの格納先
    protected GameObject Door_Object = null;

    // flag管理クラスへの接続?変数
    tutorial_flags tf;

    GameObject Fake_Wall;

    // 親コンストラクタと違う処理書いてるから
    // 頭にnewをつけておく
    new protected void Start(){
        // 親のコンストラクタ呼出し
        base.Start();
        Debug.Log("card_Door.Start");
        setCardName("Door");
        addTargetList("None");
        setTriggerType("TO");

        tf = GameObject.Find("System_Scripts").GetComponent<tutorial_flags>();
        Fake_Wall = GameObject.Find("Fake_Wall");
    }

    new protected void Update(){
        // GameObjectの更新
        if(Fake_Wall == null){
            Fake_Wall = GameObject.Find("Fake_Wall");
        }

        // 把持フラグの更新
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
        if(Fake_Wall.activeSelf){
            Debug.Log("Door appear");
            // Door_Object = Instantiate(Door_Prefab);

            //
            // とりあえず固定位置に出現
            //
            // Door_Object.transform.position = new Vector3(1.0f, 1.0f, 1.0f);


            // ドア立てたってフラグを立てる
            tf.setDoorAppear(true);

            // 10sec後にDelayMethodを実行
            // Invoke(nameof(DelayMethod), 10.0f);

            // Fake_Wall -> inactive
            Fake_Wall.SetActive(false);
        }else{
            Debug.Log("Door disappear");
            // Destroy(Door_Object);
            // Door_Object = null;
            tf.setDoorAppear(false);
            // CancelInvoke();

            // Fake_Wall -> active
            Fake_Wall.SetActive(true);
        }
    }

    // 一定時間後発動するDestroy関数
    protected void DelayMethod(){
        Debug.Log("Door_Object : Destroy");
        Destroy(Door_Object);
        Door_Object = null;
    }

    // Door_Objectを設置できるかフラグ管理にお伺いを立てる関数
    protected bool CanPutDoor(){
        bool temp = tf.getDoorAppear();
        Debug.Log("titan fall Stand by... : " + temp + " -> " + !temp);
        return !temp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントが足りない場合，自動追加
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(BoxCollider))]

// カードサンプル
// "Smoke" : 投げて着発 スモーク
// 把持トリガーで発動待ち，投擲で発動 再度トリガーで発動待機解除
// tagなし以外と接触時，スモークprefabを作る
// スモークは5秒持続，スモークカードは消失
public class card_Smoke : card_class{
    [SerializeField]
    GameObject cardObject;

    [SerializeField][Header ("Smoke_Prefab")]
    GameObject Smoke_Prefab;

    [SerializeField][Header ("Ready texture")]
    Material ReadyMat;
    [SerializeField][Header ("Not Ready texture")]
    Material NotReadyMat;

    protected GameObject Smoke_Object = null;

    protected bool isUsed = false;

    protected bool isReady = false;

    // 親コンストラクタと違う処理書いてるから
    // 頭にnewをつけておく
    new protected void Start(){
        // 親のコンストラクタ呼出し
        base.Start();
        Debug.Log("card_Smoke.Start");
        setCardName("Smoke");
        setTriggerType("TC");
        this.GetComponent<Renderer>().material = NotReadyMat;
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
        // トリガーで発動待機，待機解除
        if(this.getTriggerType() == "TC"){
            if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                this.toggleIsReady();
            }
        }
    }

    // 実行関数
    public override void use(Collision other){
        string tag = other.gameObject.tag;
        OVRGrabbable ovrGrab = this.GetComponent<OVRGrabbable>();
        if(!this.isReady){
            return;
        }
        //既に発動してるなら，何もしない
        /*if(Smoke_Object == null){
            return;
        }*/
        // target tag : !Untagged
        /*if(tag == "Untagged"){
            return;
        }*/
        if(isUsed){
            return;
        }
        isUsed = true;
        Debug.Log("Smoke discharge!");
        Vector3 startPos = this.transform.position;
        Smoke_Object = Instantiate(Smoke_Prefab);
        Smoke_Object.transform.position = startPos;
        Invoke(nameof(DelayMethod), 5.0f);
        //Destroy(this.GetComponent<OVRGrabbable>);
        ovrGrab.enabled = false;
    }

    new public void OnCollisionEnter(Collision other){
        if(this.getTriggerType() == "TC" && this.isReady){
            this.use(other);
        }
    }

    public void toggleIsReady(){
        if(this.isReady){
            this.isReady = false;
            this.GetComponent<Renderer>().material = NotReadyMat;
        }else{
            this.isReady = true;
            this.GetComponent<Renderer>().material = ReadyMat;
        }
    }
    protected void DelayMethod(){
        Destroy(Smoke_Object);
        Destroy(this.gameObject);
    }
}

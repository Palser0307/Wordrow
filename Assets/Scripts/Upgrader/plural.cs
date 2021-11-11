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
[RequireComponent(typeof(CapsuleCollider))]
// 接続用
// [RequireComponent(typeof(FixedJoint))]


public class plural : MonoBehaviour{
    // +--------+
    // | values |
    // +--------+
    private string cardName = "";

    // くっついたカードのオブジェクト
    protected GameObject Card = null;

    // 把持されているか
    protected bool isHold = false;

    // jointへのアクセサ
    protected FixedJoint joint;

    // くっついたカードのスクリプト
    // 少なくとも変数としては宣言できないわ

    // +-----------+
    // | functions |
    // +-----------+

    // 初期設定
    protected void Start(){
        setCardName("plural");
        joint = this.GetComponent<FixedJoint>();
    }

    // run per frame
    protected void Update(){
        updateIsHold();

        callUse();
    }

    // カードだったらひっつく処理
    protected void OnCollisionEnter(Collision other) {
        GameObject obj = other.gameObject;
        outputLog("くっつく？");
        // タグを見て，カードだったらひっつく
        if(obj.tag == "Card" && Card == null){
            obj.transform.parent = this.transform;
            Card = obj;
            outputLog("くっついた！");

            // TODO: 具体的にくっつく処理を作る
            // FixedJointでくっつけてみる
            // …ための下準備 CardにFixedJointのComponentをつける
            addJoint(Card);
            // 位置調整
            // FixedJointの接続先設定の前に位置を変えないと動けなくなる
            Card.transform.position = this.transform.position;
            Card.transform.rotation = this.transform.rotation;

            joint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
            // Card.GetComponent<Rigidbody>().mass = 0.1f;
        }
    }

    public void use(Collision other = null){
        // カードのuse()を呼び出す
    }

    protected void callUse(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            this.use();
        }
    }

    // Debug.Log() for CardClass
    public void outputLog(string str){
        Debug.Log(getObjectName() + " : " + str);
    }

    protected void addJoint(GameObject target){
        joint = target.AddComponent<FixedJoint>();
        target.GetComponent<BoxCollider>().enabled = false;
    }

    protected void removeJoint(GameObject target){
        Destroy(target.GetComponent<FixedJoint>());
        joint = null;
        target.GetComponent<BoxCollider>().enabled = true;
    }

    // TODO: GETTER / SETTER の宣言，定義

    // +--------------+
    // |    GETTER    |
    // +--------------+

    public string getCardName(){
        return this.cardName;
    }

    // get isHold
    public bool getIsHold(){
        return this.isHold;
    }

    // get Object Name
    public string getObjectName(){
        return this.name;
    }


    // +--------------+
    // |    SETTER    |
    // +--------------+

    public void setCardName(string newCardName){
        this.cardName = newCardName;
        this.name = "Upgrader_" + newCardName;
    }

    // set isHold
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
    }

    // update isHold
    public void updateIsHold(){
        // 最上位の親オブジェクトのGameObject
        GameObject rootParent = this.transform.root.gameObject;
        //outputLog("root tag -> "+rootParent.tag);

        // 最上位の親オブジェクトがPlayerタグを持ってるか
        // 持ってる->把持されてる
        if(rootParent.tag == "Player"){
            setIsHold(true);
        }else{
            setIsHold(false);
        }
    }

}

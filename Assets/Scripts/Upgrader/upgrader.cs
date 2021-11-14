using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 第二世代用upgrade
// 全統合版？

// 必須コンポーネントを補填
// 物理判定
[RequireComponent(typeof(Rigidbody))]
// 掴めるように
[RequireComponent(typeof(OVRGrabbable))]
// カプセル型当たり判定
[RequireComponent(typeof(CapsuleCollider))]

public class upgrader : MonoBehaviour{
    // タイプ
    public string Upgrade_Type = "";

    private bool isHold = false;

    protected FixedJoint joint;

    protected List<string> UpgraderType = new List<string>();

    protected GameObject Card;

    protected GameObject DownCard;

    void Start(){
        setupTypeList();

        if(haveUpgraderType(Upgrade_Type)){
            setCardName(Upgrade_Type);
        }
    }

    // Update is called once per frame
    void Update(){
        updateIsHold();

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

            // FixedJointでくっつけてみる
            // …ための下準備 CardにFixedJointのComponentをつける
            addJoint(Card);
            // 位置調整
            // FixedJointの接続先設定の前に位置を変えないと動けなくなる
            Card.transform.position = this.transform.position;
            Card.transform.rotation = this.transform.rotation;

            joint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
            // Card.GetComponent<Rigidbody>().mass = 0.1f;

            // スクリプト呼び出し準備
            // cardScript = Card.GetComponent<card_Tree_gen2>();
            // TriggerType = cardScript.getTriggerType();

            // pluralカード召喚

            DownCard = Instantiate(Card.GetComponent<Test_Tree>().plural_card, this.transform.position + Vector3.up*0.1f, this.transform.rotation);
        }
    }

    public void outputLog(string str){
        Debug.Log(getObjectName() + " : " + str);
    }

    protected void addJoint(GameObject target){
        joint = target.AddComponent<FixedJoint>();
        target.GetComponent<BoxCollider>().enabled = false;
    }

    // get isHold
    public bool getIsHold(){
        return this.isHold;
    }

    // get Object Name
    public string getObjectName(){
        return this.name;
    }

    public void setCardName(string newCardName){
        // this.cardName = newCardName;
        this.name = "Upgrader_" + newCardName;
    }

    // set isHold
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
    }

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

public bool haveUpgraderType(string type){
    return this.UpgraderType.Contains(type);
}
public void addUpgraderType(string type){
    if(!haveUpgraderType(type)){
        this.UpgraderType.Add(type);
    }
}
public void setupTypeList(){
    addUpgraderType("plural");
}

}

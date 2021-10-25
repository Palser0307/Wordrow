using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 直接接触発動型魔法カード用継承元クラス
// 持った状態で対象にカードが触れることで発動
// TD = Touch Directly (直接接触)

// 必須コンポーネントを補填
// 重力判定
[RequireComponent(typeof(Rigidbody))]
// 掴める
[RequireComponent(typeof(OVRGrabbable))]
// 箱型当たり判定
[RequireComponent(typeof(BoxCollider))]

public class TD_card_class : MonoBehaviour{
    // カード名
    // オブジェクト名は"CardGen2_"+cardName
    // Card Name
    // ObjectName: "CardGen2_" + cardName
    private string cardName = "";

    // 発動対象タグリスト
    // 例: "Ball"
    // Target Tag List
    // example: "Ball"
    private List<string> targetList = new List<string>();

    // 発動条件
    // 一応クラス内でも宣言しておく
    // Trigger Type
    private const string TRIGGERTYPE = "TD";

    // 把持されているかどうか
    // 初期状態: false
    // Is this card being held?
    // default: false
    private bool isHold = false;


    // 初期設定
    // 呼び出させるつもりは無い
    protected void Start(){
        setCardName("TD_Class");
    }

    // Update is called once per frame
    protected void Update(){
        // 把持情報の更新
        updateIsHold();
    }

    // 接触時
    // ここはvirtualがないと，継承先のuseを呼んでくれない
    public virtual void OnCollisionEnter(Collision other){
        Debug.Log("Touch Directly: OnCollisionEnter() start");
        if(haveTargetTag(other.gameObject.tag)){
            use(other);
        }
    }

    // 効果発動
    /* 継承先で
    protected override void use(Collision other){}
    でオーバーライドしておく */
    protected virtual void use(Collision other){
        Debug.Log("Touch Directly: virtual use() start.");
        return;
    }


    // +--------------+
    // |    GETTER    |
    // +--------------+

    // get cardName
    public string getCardName(){
        return this.cardName;
    }

    // get TargetList
    public List<string> getTargetList(){
        return this.targetList;
    }

    // have TargetTag?
    public bool haveTargetTag(string tag){
        return this.targetList.Contains(tag);
    }

    // get TriggerType
    public string getTriggerType(){
        return TRIGGERTYPE;
    }

    // get isHold
    public bool getIsHold(){
        return this.isHold;
    }


    // +--------------+
    // |    SETTER    |
    // +--------------+

    // update cardName
    public void setCardName(string newCardName){
        this.cardName = newCardName;
        this.name = "CardGen2_" + newCardName;
    }

    // add newTag to TargetList
    public void addTargetList(string newTag){
        if(!haveTargetTag(newTag)){
            this.targetList.Add(newTag);
        }
    }

    // set isHold
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
    }

    // update isHold
    public void updateIsHold(){
        if(this.transform.parent != null){
            setIsHold(true);
        }else{
            setIsHold(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 投擲着発型魔法カード用継承元クラス
// 持った状態でトリガーで発動待機，その後手を離れてどこかに接触したことで発動
// ThO = Throwing Operation (投擲着発)

// 必須コンポーネントを補填
// 重力判定
[RequireComponent(typeof(Rigidbody))]
// 掴める
[RequireComponent(typeof(OVRGrabbable))]
// 箱型当たり判定
[RequireComponent(typeof(BoxCollider))]

public class ThO_card_class : MonoBehaviour{
    // エフェクトのPrefab
    [SerializeField]
    protected GameObject Effect_Prefab;

    // カード名
    // オブジェクト名は"CardGen2_"+cardName
    // CardName
    // ObjectName: "CardGen2_"+cardName
    private string cardName = "";

    // 発動対象タグリスト 必要あるかな？
    // 例: "Ball"
    // Target Tag List
    // example: "Ball"
    private List<string> targetList = new List<string>();

    // 発動条件
    // 一応クラス内でも宣言しておく
    // Trigger Type
    private const string TRIGGERTYPE = "ThO";

    // 把持されているかどうか
    // 初期状態: false
    // Is this card being held?
    // default: false
    private bool isHold = false;

    // 発動待機状態のフラグ
    // 初期状態: false
    // Is this card ready?
    // default: false
    private bool isReady = false;

    // 初期設定
    // 呼び出させるつもりは無い
    protected void Start(){
        setCardName("ThO_Class");

        // Effect_Prefabにちゃんと指定してあるかチェック
        checkPrefab();
    }

    // Update is called once per frame
    protected void Update(){
        // 把持情報の更新
        updateIsHold();
    }

    // 接触時
    // ここはvirtualがないと，継承先のuse()を呼んでくれない
    public virtual void OnCollisionEnter(Collision other) {
        if(haveTargetTag(other.gameObject.tag) && getIsReady()){
            use(other);
        }
    }

    // 効果発動
    /* 継承先で
    protected override void use(Collision other){}
    でオーバーライドしておく */
    protected virtual void use(Collision other){
        Debug.Log("Throwing Operation: virtual use() start.");
        return;
    }

    // Effect_Prefabが指定されているか
    protected bool checkPrefab(){
        if(Effect_Prefab != null){
            return true;
        }else{
            Debug.Log(cardName+"(Gen2): Warning!!! >Effect_Prefab is NULL!!");
            return false;
        }
    }

    // update isReady
    protected void updateIsReady(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            setIsReady(!getIsReady());
        }
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

    // get isReady
    public bool getIsReady(){
        return this.isReady;
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

    // set isReady
    public void setIsReady(bool newStatus){
        this.isReady = newStatus;
    }
}

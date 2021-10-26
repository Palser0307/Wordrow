using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 人差し指トリガー発動型魔法カード用継承元クラス
// 持った状態でトリガーを引くことで発動
// TrO = Triggered Operation

// 必須コンポーネントを補填
// 重力判定
[RequireComponent(typeof(Rigidbody))]
// 掴めるように
[RequireComponent(typeof(OVRGrabbable))]
// 箱型当たり判定
[RequireComponent(typeof(BoxCollider))]

public class TrO_card_class : MonoBehaviour{
    // 効果のPrefab
    [SerializeField]
    protected GameObject Effect_Prefab;

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
    private const string TRIGGERTYPE = "TrO";

    // 把持されているかどうか
    // 初期状態: false
    // Is this card being held?
    // default: false
    private bool isHold = false;


    // 初期設定
    // 呼び出させるつもりは無い
    protected void Start(){
        // 名前設定
        setCardName("TrO_Class");

        // Effect_Prefabにちゃんと指定してあるかチェック
        checkPrefab();
    }

    // Update is called once per frame
    protected void Update(){
        // 把持情報の更新
        updateIsHold();

        // use()を呼び出す関数を叩く
        callUse();
    }

    // 効果発動
    /* 継承先で
    protected override void use(Collision other){}
    でオーバーライドしておく */
    protected virtual void use(){
        Debug.Log("Triggered Operation: virtual use() start.");
        return;
    }

    // トリガー発動呼出し
    protected void callUse(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            this.use();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 第二世代魔法カード用継承元最上位クラス
// 各発動条件クラスの共通項目をここから継承する

// 必須コンポーネントを補填
// 物理判定
[RequireComponent(typeof(Rigidbody))]
// 掴めるように
[RequireComponent(typeof(OVRGrabbable))]
// 箱型当たり判定
[RequireComponent(typeof(BoxCollider))]

public class card_class_gen2 : MonoBehaviour{
    // +--------+
    // | values |
    // +--------+

    // エフェクトのプレハブ
    [SerializeField]
    protected GameObject Effect_Prefab;

    // カード名
    // オブジェクト名は"Card_"+cardName+"(gen2)"
    private string cardName = "";

    // 発動対象タグリスト
    // 例: "Ball"
    private List<string> targetList = new List<string>();

    // 発動条件
    // 一応クラス内でも宣言しておく
    // Trigger Type
    private string TRIGGERTYPE = "";

    // 把持されているかどうか
    // default: false
    private bool isHold = false;

    // +-----------+
    // | functions |
    // +-----------+

    // 初期設定
    // 呼び出し予定はない
    protected void Start(){
        setCardName("CardClass");
        checkPrefab();
        return;
    }

    // Update() is called once per frame
    /* 継承先で
    protected override void Update(){}
    でオーバーライドしておく */
    protected virtual void Update(){
        updateIsHold();
        return;
    }

    // 効果発動
    /* 継承先で
    protected override void use(Collision other){}
    でオーバーライドしておく */
    public virtual void use(Collision other){
        outputLog("virtual use() start.");
    }

    // Effect_Prefabが指定されているかのチェック
    protected bool checkPrefab(){
        if(Effect_Prefab != null){
            return true;
        }else{
            outputLog("Warning!! >Effect_Prefab is NULL!!");
            return false;
        }
    }

    // Debug.Log() for CardClass
    public void outputLog(string str){
        Debug.Log(getObjectName() + " : " + str);
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

    // get Object Name
    public string getObjectName(){
        return this.name;
    }

    // +--------------+
    // |    SETTER    |
    // +--------------+

    // update cardName
    public void setCardName(string newCardName){
        this.cardName = newCardName;
        this.name = "Card_" + newCardName + "(gen2)";
    }

    // add newTag to TargetList
    public void addTargetList(string newTag){
        if(!haveTargetTag(newTag)){
            this.targetList.Add(newTag);
        }
    }

    // set Trigger Type
    protected void setTriggerType(string newTT){
        this.TRIGGERTYPE = newTT;
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

    // set Object Name
    public void setObjectName(string newName){
        this.cardName = "Card_" + newName + "(gen2)";
    }
}
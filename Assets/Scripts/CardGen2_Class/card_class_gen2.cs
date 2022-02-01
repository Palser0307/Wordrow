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
// 音声
[RequireComponent(typeof(AudioSource))]

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
    private string TRIGGERTYPE;

    // 把持されているかどうか
    // default: false
    private bool isHold = false;

    // 使えるUpgrader
    private List<string> upgraderList = new List<string>();

    // 今くっついてるUpgrader
    //private string upgraderName = "";

    protected Rigidbody rigid;

    protected OVRGrabbable grab;

    // フラグ関連 てかストーリー管理スクリプトへのアクセサ
    protected Story_Controller sc = null;

    // +-----------+
    // | functions |
    // +-----------+

    // 初期設定
    // 呼び出し予定はない
    protected void Start(){
        setCardName("CardClass");
        checkPrefab();

        // Rigidbodyアクセサを取得
        rigid = this.GetComponent<Rigidbody>();
        // 力学無視
        //rigid.isKinematic = true;
        // 重力無視
        rigid.useGravity = false;

        grab = this.GetComponent<OVRGrabbable>();
        GameObject ssObj = GameObject.Find("System_Scripts");
        if(ssObj != null){
            if(!ssObj.TryGetComponent(out this.sc)){
                outputError("can't found sc");
            }
        }

        return;
    }

    // Update() is called once per frame
    /* 継承先で
    protected override void Update(){}
    でオーバーライドしておく */
    protected virtual void Update(){
        updateIsHold();

        vectorZero();

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
    // Debug.LogError() for CardClass
    protected void outputError(string str){
        Debug.LogError(getObjectName() + " : " + str);
    }

    // 移動速度をゼロに
    protected void vectorZero(){
        rigid.velocity = Vector3.zero;
    }

    protected void playAudio(){
        AudioSource audio = this.GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
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

    // get UpgraderList
    public List<string> getUpgraderList(){
        return this.upgraderList;
    }

    // have upgrader?
    public bool haveUpgrader(string type){
        return this.upgraderList.Contains(type);
    }

    // +--------------+
    // |    SETTER    |
    // +--------------+

    // update cardName
    public void setCardName(string newCardName){
        this.cardName = newCardName;
        this.name = "card_" + newCardName + "_gen2";
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
        // flag
        if(this.sc != null){
            this.sc.fm.setFlag("grab"+getCardName(), newStatus);
        }
    }

    // update isHold
    public void updateIsHold(){
        if(getIsHold() != grab.isGrabbed){
            setIsHold(grab.isGrabbed);
        }
    }

    // add newType to UpgraderList
    public void addUpgraderList(string newType){
        if(!haveUpgrader(newType)){
            this.upgraderList.Add(newType);
        }
    }
}
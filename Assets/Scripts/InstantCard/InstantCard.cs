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

public class InstantCard : MonoBehaviour{
    // +--------+
    // | values |
    // +--------+
    public string Name = "";

    // カード名
    // オブジェクト名は"Card_"+cardName+"(Instant)"
    private string cardName = "";

    // 把持されているかどうか
    // default: false
    private bool isHold = false;

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
        setCardName(this.Name);

        // Rigidbodyアクセサを取得
        this.gameObject.TryGetComponent(out this.rigid);
        // 重力無視
        rigid.useGravity = false;

        this.gameObject.TryGetComponent(out this.grab);
        GameObject ssObj = GameObject.Find("System_Scripts");
        if(!ssObj.TryGetComponent(out this.sc)){
            outputError("can't found sc");
        }

        return;
    }

    // Update() is called once per frame
    protected void Update(){
        updateIsHold();

        vectorZero();

        return;
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

    // +--------------+
    // |    GETTER    |
    // +--------------+

    // get cardName
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

    // update cardName
    public void setCardName(string newCardName){
        this.cardName = newCardName;
        this.name = "card_" + newCardName + "_(Instant)";
    }

    // set isHold
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
        // flag
        this.sc.fm.setFlag("grab"+getCardName(), newStatus);
    }

    // update isHold
    public void updateIsHold(){
        if(getIsHold() != grab.isGrabbed){
            setIsHold(grab.isGrabbed);
        }
    }

}
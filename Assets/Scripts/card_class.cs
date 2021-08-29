using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_class : MonoBehaviour{
    // Fly，Rainなど，card_をつけなくてヨシ
    // オブジェクト名にはつくようにしている
    private string cardName;

    // Target Tag List
    // 必要無かったらとりあえず何もaddしなくてOK
    // :Ball
    private List<string> targetList;

    // Trigger condition
    // 発動条件
    // :DC = Direct contact 直接接触
    // :TO = Triggered Operation 把持時トリガー発動
    private string triggerType;

    // 把持されているかどうか
    // default: false
    private bool isHold = false;

    protected void Start(){
        // 以下のコードを実行してから他のコードを実行すること
        Debug.Log("card_class.Start");
        this.cardName = "";
        this.targetList = new List<string>{};
        this.triggerType = "";
    }

    protected void Update(){
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
        // トリガー発動型用
        if(this.triggerType == "TO"){
            // 右人差し指トリガーで発動
            if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                this.use();
            }
        }
    }

    // 接触反応型用
    // Trigger condition = DC
    public virtual void OnCollisionEnter(Collision other){
        if(this.triggerType == "DC"){
            this.use(other);
        }
    }

    // 実行する関数
    // 引数無し対応のdefault value付きuse()
    public virtual void use(Collision other = null){
        Debug.Log("use " + this.cardName + "!");
        /*
            継承元のuse()
            継承元関数にはvirtualを
            継承先関数にはoverrideをつける
        */
    }

    // GETTER
    public string getCardName(){
        return this.cardName;
    }
    public List<string> getTargetList(){
        return this.targetList;
    }
    public bool haveTargetTag(string tag){
        return this.targetList.Contains(tag);
    }
    public string getTriggerType(){
        return this.triggerType;
    }
    public bool getIsHold(){
        return this.isHold;
    }

    // SETTER
    public void setCardName(string newCardName){
        this.cardName = newCardName;
        this.name = "Card_" + newCardName;
    }
    public void addTargetList(string newTag){
        this.targetList.Add(newTag);
    }
    public void setTriggerType(string newTriggerType){
        this.triggerType = newTriggerType;
    }
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
    }
}

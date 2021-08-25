using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_class : MonoBehaviour{
    private string cardName;

    // Target Tag List
    // :Ball
    private List<string> targetList;

    // Trigger condition
    // :DC = Direct contact
    private string triggerType;

    protected void Start(){
        // 以下のコードを実行してから他のコードを実行すること
        Debug.Log("card_class.Start");
        this.cardName = "";
        this.targetList = new List<string>{};
        this.triggerType = "";
    }

    // 接触反応式用
    // Trigger condition = DC
    public virtual void OnCollisionEnter(Collision other){
        if(this.triggerType == "DC"){
            this.use(other);
        }
    }

    // 実行する関数
    public virtual void use(Collision other){
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

    // SETTER
    public void setCardName(string newCardName){
        this.cardName = newCardName;
    }
    public void addTargetList(string newTag){
        this.targetList.Add(newTag);
    }
    public void setTriggerType(string newTriggerType){
        this.triggerType = newTriggerType;
    }
}

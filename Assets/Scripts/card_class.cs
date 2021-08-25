using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class "カード" の継承元
// 名前，タイプ(?)，実行関数use()を持つ
// 名前，タイプのR/WにはGetter/setterを用いること
public class card_class : MonoBehaviour{
    private string cardName;
    private string cardType;

    void Start(){
        this.cardName = "";
        this.cardType = "";
    }

    void Update(){

    }

    // 接触使用
    // 衝突時自動呼出し関数(unity固有)
    public void OnCollisionEnter(Collision other){
        Debug.Log(this.cardName + ": Hit to" + other.gameObject.name);
        this.use(other);
    }

    // 実行する関数
    public virtual void use(Collision other){
        // 継承元のuse()
        // 継承元関数にはvirtualを
        // 継承先関数にはoverrideをつける
    }

    // GETTER
    public string getName(){
        return this.cardName;
    }
    public string getType(){
        return this.cardType;
    }

    // SETTER
    // 使う予定無いが…
    public void setName(string newName){
        this.cardName = newName;
    }
    public void setType(string newType){
        this.cardType = newType;
    }
}

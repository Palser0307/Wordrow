using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 投擲着発型魔法カード用継承元クラス
// 持った状態でトリガーで発動待機，その後手を離れてどこかに接触したことで発動
// ThO = Throwing Operation (投擲着発)

public class ThO_card_class : card_class_gen2{
/*
    継承元で宣言された変数群
    [SerializeField]
    protected GameObject Effect_Prefab;

    // カード名
    private string cardName = "";

    // 発動対象タグリスト 必要あるかな？
    private List<string> targetList = new List<string>();

    // 発動条件
    private string TRIGGERTYPE = "";

    // 把持されているかどうか
    // 初期状態: false
    private bool isHold = false;
*/

    // 発動待機状態のフラグ
    // 初期状態: false
    private bool isReady = false;

    // 初期設定
    // 呼び出させるつもりは無い
    protected new void Start(){
        base.Start();
        setCardName("ThO_Class");

        // Effect_Prefabにちゃんと指定してあるかチェック
        checkPrefab();

        setTriggerType("ThO");
    }

    // Update is called once per frame
    protected new void Update(){
        base.Update();
        // 把持情報の更新
        updateIsHold();

        // 発動待機状態の更新
        updateIsReady();
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
    public new virtual void use(Collision other){
        Debug.Log("Throwing Operation: virtual use() start.");
        return;
    }

    // update isReady
    protected void updateIsReady(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            setIsReady(!getIsReady());
            outputLog("updateIsReady() > change isReady to "+ getIsReady());
        }
    }


    // +--------+
    // | GETTER |
    // +--------+

    protected bool getIsReady(){
        return this.isReady;
    }


    // +--------+
    // | SETTER |
    // +--------+

    protected void setIsReady(bool newStatus){
        this.isReady = newStatus;
    }
}

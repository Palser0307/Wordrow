using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 直接接触発動型魔法カード用継承元クラス
// 持った状態で対象にカードが触れることで発動
// TD = Touch Directly (直接接触)

public class TD_card_class : card_class_gen2{
/*
    継承元で宣言された変数群
    // エフェクトのPrefab
    [SerializeField]
    GameObject Effect_Prefab;

    // カード名
    private string cardName = "";

    // 発動対象タグリスト
    private List<string> targetList = new List<string>();

    // 発動条件
    private const string TRIGGERTYPE = "TD";

    // 把持されているかどうか
    // 初期状態: false
    private bool isHold = false;
*/

    // 初期設定
    // 呼び出させるつもりは無い
    protected new void Start(){
        setCardName("TD_Class");

        // Effect_Prefabにちゃんと指定してあるかチェック
        checkPrefab();

        setTriggerType("TD");
    }

    // Update is called once per frame
    protected new void Update(){
        // 把持情報の更新
        updateIsHold();
    }

    // 接触時
    // ここはvirtualがないと，継承先のuseを呼んでくれない
    public virtual void OnCollisionEnter(Collision other){
        outputLog("OnCollisionEnter() start");
        if(haveTargetTag(other.gameObject.tag)){
            use(other);
        }
    }

    // 効果発動
    /* 継承先で
    protected override void use(Collision other){}
    でオーバーライドしておく */
    public new virtual void use(Collision other){
        outputLog("virtual use() start.");
        return;
    }
}
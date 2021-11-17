using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 人差し指トリガー発動型魔法カード用継承元クラス
// 持った状態でトリガーを引くことで発動
// TrO = Triggered Operation

public class TrO_card_class : card_class_gen2{
/*
    継承元で宣言された変数群
    // 効果のPrefab
    [SerializeField]
    protected GameObject Effect_Prefab;

    // カード名
    private string cardName = "";

    // 発動対象タグリスト
    private List<string> targetList = new List<string>();

    // 発動条件
    private const string TRIGGERTYPE = "";

    // 把持されているかどうか
    // 初期状態: false
    private bool isHold = false;
*/

    // 初期設定
    // 呼び出させるつもりは無い
    protected new void Start(){
        base.Start();
        // 名前設定
        setCardName("TrO_Class");

        // Effect_Prefabにちゃんと指定してあるかチェック
        checkPrefab();

        setTriggerType("TrO");
    }

    // Update is called once per frame
    protected new void Update(){
        base.Update();
        // 把持情報の更新
        this.updateIsHold();

        // use()を呼び出す関数を叩く
        this.callUse();
    }

    // 効果発動
    /* 継承先で
    protected override void use(Collision other){}
    でオーバーライドしておく */
    public new virtual void use(Collision other = null){
        Debug.Log("Triggered Operation: virtual use() start.");
        return;
    }

    // トリガー発動呼出し
    protected void callUse(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            this.use();
        }
    }
}

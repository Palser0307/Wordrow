using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Rain_gen2 : TrO_card_class{
    // 雨のエフェクトPrefabは継承元クラスで宣言してる
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    // 発生させたRainオブジェクトの格納先
    protected GameObject Rain_Object = null;

    // 雨の起点の高さ
    private float height = 10f;

    // 雨の持続時間 [sec]
    private float lifeTime = 10.0f;

    // 初期設定
    // 初ロード時に叩かれる
    new void Start(){
        // カード名設定
        setCardName("Rain");

        checkPrefab();
        Debug.Log("Rain(Gen2): Setup finish.");
    }

    // フレームごとに叩かれる
        new void Update(){
        // 継承元クラスのUpdate()を呼び出す
        base.Update();
    }

    // 効果発動
    // 発動時のカード位置からheight分上空にEffect_Prefabの配置
    protected override void use(){
        Debug.Log("Rain(Gen2): use() start.");

        // Rain_Objectが空→まだ使ってない
        if(Rain_Object == null){
            Debug.Log("Rain(Gen2): Rain_Object appear.");

            // Effect_PrefabのインスタンスをRain_Objectに格納
            Rain_Object = Instantiate(Effect_Prefab);
            // 高さ設定
            Vector3 distance = Vector3.up * height;
            // 中心位置の代入
            Rain_Object.transform.position = this.transform.position + distance;

            // lifeTime 秒後にRain_Objectを削除する関数を呼び出すようにセット
            Invoke(nameof(DelayMethod), lifeTime);
        }else{
            Debug.Log("Rain(Gen2): Rain_Object disappear by use().");

            // Rain_Objectを破壊
            Destroy(Rain_Object);
            // 一応nullで代入
            Rain_Object = null;
            // セットした時限関数呼び出しをキャンセル
            CancelInvoke();
        }
    }

    // Invokeで呼び出される関数
    // Rain_Objectを破壊し，nullを代入する
    protected void DelayMethod(){
        Debug.Log("Rain(Gen2): Rain_Object disappear by InvokedMethod");
        Destroy(Rain_Object);
        Rain_Object = null;
    }
}
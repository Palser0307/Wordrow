using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Smoke_gen2 : ThO_card_class{
    // 煙のエフェクトPrefabは継承元クラスで宣言してる
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    // 発生させたSmokeオブジェクトの格納先
    protected GameObject Smoke_Object = null;

    // Smokeの持続時間
    private float lifeTime = 10.0f;

    // 初期設定
    // 初ロード時に叩かれる
    new void Start(){
        // カード名設定
        setCardName("Smoke");

        // Prefabがちゃんとあるかどうか
        checkPrefab();

        // 発動対象タグを追加(壁とかで発動しないように)
        addTargetList("Untagged");
        addTargetList("Ground");

        Debug.Log("Smoke(Gen2): Setup finish.");
    }

    // フレームごとに叩かれる
    new void Update(){
        // 継承元クラスのUpdate()を呼び出す
        base.Update();
    }

    // 効果発動
    // 発動時のカード位置->着弾地点にEffect_Prefabの配置
    protected override void use(Collision other){
        Debug.Log("Smoke(Gen2): use() start.");

        // Smoke_Objectが空→まだ使ってない
        if(Smoke_Object == null){
            Debug.Log("Smoke(Gen2): Smoke_Object appear.");

            // Effect_PrefabのインスタンスをSmoke_Objectに格納
            Smoke_Object = Instantiate(Effect_Prefab);
            // 中心位置の代入
            Smoke_Object.transform.position = this.transform.position;

            // lifeTime 秒後にSmoke_Objectを削除する関数を呼び出すようにセット
            Invoke(nameof(DelayMethod), lifeTime);
        }
    }

    // Invokeで呼び出される関数
    // Smoke_Objectを破壊し，nullを代入する
    protected void DelayMethod(){
        Debug.Log("Smoke(Gen2): Smoke_Object disappear by InvokedMethod");
        Destroy(Smoke_Object);
        Smoke_Object = null;
    }
}
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

    // isReady T/F でテクスチャが変わるように！
    [SerializeField]
    protected Material ReadyMat = null, NotReadyMat = null;

    // 初期設定
    // 初ロード時に叩かれる
    new void Start(){
        base.Start();
        // カード名設定
        setCardName("Smoke");

        // Prefabがちゃんとあるかどうか
        checkPrefab();

        // 発動対象タグを追加(壁とかで発動しないように)
        addTargetList("Untagged");
        addTargetList("Ground");

        outputLog("Setup finish.");
    }

    // フレームごとに叩かれる
    new void Update(){
        // 継承元クラスのUpdate()を呼び出す
        base.Update();
    }

    // 効果発動
    // 発動時のカード位置->着弾地点にEffect_Prefabの配置
    public override void use(Collision other){
        outputLog("use() start.");

        // Smoke_Objectが空でない→使用済み
        if(Smoke_Object != null){
            return;
        }

        // flag
        sc.fm.setFlag("useSmoke", true);

        playAudio();

        outputLog("Smoke_Object appear.");

        Vector3 pos = this.transform.position;
        Quaternion rot = Quaternion.Euler(-90, 0, 0);

        // Effect_PrefabのインスタンスをSmoke_Objectに格納
        Smoke_Object = Instantiate(Effect_Prefab, pos, rot);
        // 中心位置の代入
        //Smoke_Object.transform.position = this.transform.position;

        // lifeTime 秒後にSmoke_Objectを削除する関数を呼び出すようにセット
        Invoke(nameof(DelayMethod), lifeTime);

        // 発動待機状態を解除
        setIsReady(false);
    }

    // Invokeで呼び出される関数
    // Smoke_Objectを破壊し，nullを代入する
    protected void DelayMethod(){
        outputLog("Smoke_Object disappear by InvokedMethod");
        Destroy(Smoke_Object);
        Smoke_Object = null;

        Destroy(this.gameObject);
    }

    // update isReady
    // 色変わり用に再定義
    protected override void updateIsReady(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            setIsReady(!getIsReady());
            outputLog("new updateIsReady() > change isReady to "+ getIsReady());
            if(getIsReady() == true){
                this.GetComponent<Renderer>().material = ReadyMat;
            }else{
                this.GetComponent<Renderer>().material = NotReadyMat;
            }
        }
    }
}
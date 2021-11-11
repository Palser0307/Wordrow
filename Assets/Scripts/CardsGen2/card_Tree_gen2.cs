using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Tree_gen2 : TrO_card_class{
    // 丸太のPrefabは継承元クラスで宣言してる
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    // 発生させた丸太オブジェクトの格納先
    protected GameObject Tree_Object = null;

    // 丸太出現の起点の高さ
    private float height = 0.5f;

    // 木の生存時間 [sec]
    private float lifeTime = 10.0f;

    // 初期設定
    // 初ロード時に叩かれる
    public new void Start(){
        base.Start();
        // カード名設定
        setCardName("Tree");

        checkPrefab();
        outputLog("Setup finish.");
    }

    // フレームごとに叩かれる
    public new void Update(){
        // 継承元クラスのUpdate()を呼び出す
        base.Update();
    }

    // 効果発動
    // 発動時のカード位置からheight分上空にEffect_Prefabの配置
    public override void use(Collision other = null){
        outputLog("use() start.");

        // Rain_Objectが空→まだ使ってない
        if(Tree_Object == null){
            outputLog("Tree_Object appear.");
            Vector3 pos = this.transform.position + Vector3.up * height;
            Quaternion rot = Quaternion.Euler(90,0,0);

            // 出現位置ランダム化
            float randX = 1 - Random.Range(0.0f, 2.0f);
            float randZ = 1 - Random.Range(0.0f, 2.0f);
            pos += Vector3.forward * randX + Vector3.right * randZ;

            // 出現角度ランダム化
            float randRotX = Random.Range(0.0f, 180.0f);
            float randRotY = Random.Range(0.0f, 180.0f);
            rot = Quaternion.Euler(randRotX, randRotY, 0);

            // Effect_PrefabのインスタンスをRain_Objectに格納
            Tree_Object = Instantiate(Effect_Prefab, pos, rot);

            // lifeTime 秒後にRain_Objectを削除する関数を呼び出すようにセット
            Invoke(nameof(DelayMethod), lifeTime);
        }else{
            outputLog("Tree_Object disappear by use().");

            // Rain_Objectを破壊
            Destroy(Tree_Object);
            // 一応nullで代入
            Tree_Object = null;
            // セットした時限関数呼び出しをキャンセル
            CancelInvoke();
        }
    }

    // Invokeで呼び出される関数
    // Rain_Objectを破壊し，nullを代入する
    protected void DelayMethod(){
        outputLog("Tree_Object disappear by InvokedMethod");
        Destroy(Tree_Object);
        Tree_Object = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Box_gen2 : TrO_card_class{
    // 箱のPrefabは継承元クラスで宣言してる
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    // 箱出現の起点の高さ
    private float height = 0.2f;

    // 初期設定
    // 初ロード時に叩かれる
    public new void Start(){
        base.Start();
        // カード名設定
        setCardName("Box");

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
        //outputLog("use() start.");

        //outputLog("Box_Object appear.");
        Vector3 pos = this.transform.position + Vector3.up * height;
        Quaternion rot = Quaternion.Euler(90,0,0);

        // 出現位置ランダム化
        float randX = (1 - Random.Range(0.0f, 2.0f)) * 0.1f;
        float randZ = (1 - Random.Range(0.0f, 2.0f)) * 0.1f;
        pos += Vector3.forward * randX + Vector3.right * randZ;

        // 出現角度ランダム化
        float randRotX = Random.Range(0.0f, 180.0f);
        float randRotY = Random.Range(0.0f, 180.0f);
        rot = Quaternion.Euler(randRotX, randRotY, 0);

        // Effect_Prefabのインスタンスを生成
        Instantiate(Effect_Prefab, pos, rot);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// "Fly" : 特定のタグを持つオブジェクトを上空に射出
// 現状，Ballタグを持ってるオブジェクトにのみ反応
// FlyというよりもはやJumpでは？
public class card_Fly_gen2 : TD_card_class{
    // 加える力の大きさ
    private float power = 10f;

    // 初期設定
    // 初ロード時に叩かれる
    protected new void Start(){
        base.Start();
        // カード名設定
        setCardName("Fly");

        // 発動対象タグの設定
        // 今のところはタグだけ
        addTargetList("Ball");

        outputLog("Setup finish.");
    }

    // フレームごとに叩かれる
    protected new void Update(){
        // 継承元クラスのUpdate()を呼び出す
        base.Update();
    }

    // 実際の効果
    // 触れた対象に鉛直上方向へpower分の力を加える．
    // 発動対象タグを持っているかはOnCollisionEnter()で確認済み
    public override void use(Collision other){
        outputLog("use func. start.");

        // 鉛直上方向指定
        Vector3 direction = Vector3.up;

        // 運動力付与
        // ForceMode.Impulse => 一瞬だけ パルス波のイメージ？
        other.gameObject.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 発動待機状態で投げて，着弾地点にDoorオブジェクトを設置
public class card_Door_gen2 : ThO_card_class{
    // ドアのPrefabは継承元クラスで宣言してある
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    // 出現させたDoorオブジェクトの格納先
    protected GameObject Door_Object = null;

    // Doorの生存時間[sec]
    private float lifeTime = 60.0f;

    // 投げて手から離れた位置
    private Vector3 releasePos;

    // 初期設定
    // 初ロード時に叩かれる
    new void Start(){
        base.Start();
        // カード名設定
        setCardName("Door");

        // Prefabがちゃんとあるか
        checkPrefab();

        // 発動対象タグを追加
        addTargetList("Untagged");
        addTargetList("Ground");

        Debug.Log("Door(Gen2): Setup finish.");
    }

    // 毎フレーム叩かれる
    new void Update(){
        // 継承元クラスのUpdate()を叩く
        base.Update();
    }

    // 効果発動
    // 着弾時のカード位置にドアを設置
    // TODO: ドアの出現位置と角度の調整()
    public override void use(Collision other){
        outputLog("use() start.");

        // Door_Objectがnull->使ってないってことで．
        if(Door_Object == null){
            outputLog("Door_Object appear.");
            Vector3 pos = this.transform.position;
            Vector3 newPos = new Vector3(pos.x, pos.y, pos.z);
            float newYrot = calcAngle(pos, releasePos);
            Quaternion rot = Quaternion.Euler(0, newYrot-180, 0);

            // Effect_PrefabのインスタンスをDoor_Objectに格納
            // インスタンス作成時に，座標及び角度も指定可能 むしろここで設定しておくべき
            Door_Object = Instantiate(Effect_Prefab, newPos, rot);

            // lifeTime秒後にDoor_Objectを削除する関数を呼び出す
            Invoke(nameof(DelayMethod), lifeTime);
        }
    }

    // Invokeで呼び出される関数
    // Door_Objectを破壊し，nullを代入する
    protected void DelayMethod(){
        outputLog("Door_Object disappear by DelayMethod.");
        Destroy(Door_Object);
        Door_Object = null;
        Destroy(this.gameObject);
    }

    // updateIsHold() を変更
    // 手を離したときの位置座標を記録するように
    public new void updateIsHold(){
        // outputLog("updateIsHold()");
        if(!grab.isGrabbed){
            setIsHold(true);
        }else{
            if(getIsHold()){
                releasePos = this.transform.position;
            }
            setIsHold(false);
        }
    }

    // 角度計算
    public float calcAngle(Vector3 basePos, Vector3 targetPos){
        float deltaX = targetPos.x - basePos.x;
        float deltaZ = targetPos.z - basePos.z;
        float angle = Mathf.Atan2(deltaZ, deltaX) * Mathf.Rad2Deg;
        outputLog("calcAngle ="+angle);
        return angle;
    }
}
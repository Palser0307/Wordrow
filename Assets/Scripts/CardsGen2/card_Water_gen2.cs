using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 掴んだ手の方向まっすぐに水柱が出る…予定
// 水柱にはColliderがある…はずだよ？
public class card_Water_gen2 : TrO_card_class {
    // 水柱のPrefab変数は継承元クラスで宣言してる
    // PrefabにはちゃんとWaterタグをつけておく
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    protected GameObject WaterObj = null;

    private void Start() {
        base.Start();
        setCardName("Water");

        // FlagSystem
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            outputError("can't found sc");
        }
    }

    private void Update() {
        base.Update();
    }

    public override void use(Collision other = null){
        this.sc.fm.setFlag("useWater", true);
        if(WaterObj == null){
            OVRGrabbable grabbable;
            if(this.gameObject.TryGetComponent(out grabbable)){
                return;
            }
            // OVRGrabberを持ってるオブジェクト=手のGameObjectをとってくる
            GameObject grabberObj = grabbable.grabbedBy().gameObject;

            // 水柱オブジェクトの角度調整1(手に対して上方向)
            Quaternion rot1 = Quaternion.Euler(0,0,0);

            // 水柱オブジェクトの角度調整2(上方向から前方向への調整)
            Quaternion rot2 = Quaternion.Euler(0,0,0);

            // 水柱のインスタンス生成
            WaterObj = Instantiate(Effect_Prefab, this.gameObject.transform.position, rot1+rot2);
            WaterObj.transform.parent = this.gameObject.transform;
        }else{
            WaterObj.transform.parent = null;
            Destroy(WaterObj);
            WaterObj = null;
        }
    }
}
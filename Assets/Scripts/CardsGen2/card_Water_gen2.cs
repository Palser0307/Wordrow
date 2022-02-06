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

    private new void Start() {
        base.Start();
        setCardName("Water");

        // FlagSystem
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            outputError("can't found sc");
        }
    }

    private new void Update() {
        base.Update();

        // 手から放したら水柱は消える
        if(!getIsHold()){
            if(WaterObj != null){
                Destroy(WaterObj);
                WaterObj = null;
            }
        }
    }

    public override void use(Collision other = null){
        if(this.sc != null){
            this.sc.fm.setFlag("useWater", true);
        }
        outputLog("use() start.");
        if(WaterObj == null){
            outputLog("appear WaterObj");
            OVRGrabbable grabbable;
            if(!this.gameObject.TryGetComponent(out grabbable)){
                outputLog("can't found OVRGrabbable");
                return;
            }
            // OVRGrabberを持ってるオブジェクト=手のGameObjectをとってくる
            GameObject grabberObj = grabbable.grabbedBy.gameObject;

            // 水柱オブジェクトの角度調整変数
            float[] rot = new float[3]{0,0,0};

            // 水柱のインスタンス生成
            WaterObj = Instantiate(Effect_Prefab, grabberObj.transform.position, grabberObj.transform.rotation);
            WaterObj.transform.parent = grabberObj.gameObject.transform;

            Invoke(nameof(DelayMethod), 20.0f);
        }else{
            outputLog("disappear WaterObj");
            CancelInvoke();
            WaterObj.transform.parent = null;
            Destroy(WaterObj);
            WaterObj = null;
        }
    }
    void DelayMethod(){
        Destroy(WaterObj);
        WaterObj = null;
    }
}
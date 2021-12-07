using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 掴んだ手の方向まっすぐに火柱が出る…予定
// 火柱にはColliderがある…はずだよ？
public class card_Fire_gen2 : TrO_card_class {
    // 火柱のPrefab変数は継承元クラスで宣言してる
    // PrefabにはちゃんとFireタグをつけておく
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    protected GameObject FireObj = null;

    private new void Start() {
        base.Start();
        setCardName("Fire");

        // FlagSystem
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            outputError("can't found sc");
        }
    }

    private new void Update() {
        base.Update();
    }

    public override void use(Collision other = null){
        this.sc.fm.setFlag("useFire", true);
        if(FireObj == null){
            OVRGrabbable grabbable;
            if(this.gameObject.TryGetComponent(out grabbable)){
                return;
            }
            // OVRGrabberを持ってるオブジェクト=手のGameObjectをとってくる
            GameObject grabberObj = grabbable.grabbedBy.gameObject;

            // 火柱オブジェクトの角度調整変数
            float[] rot = new float[3]{0,0,0};

            // 火柱のインスタンス生成
            FireObj = Instantiate(Effect_Prefab, this.gameObject.transform.position, Quaternion.Euler(rot[0], rot[1], rot[2]));
            FireObj.transform.parent = this.gameObject.transform;
        }else{
            FireObj.transform.parent = null;
            Destroy(FireObj);
            FireObj = null;
        }
    }
}
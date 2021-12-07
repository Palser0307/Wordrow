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

    private void Start() {
        base.Start();
        setCardName("Fire");

        // FlagSystem
        if(!GameObject.Find("System_Scripts").TryGetComponent(out this.sc)){
            outputError("can't found sc");
        }
    }

    private void Update() {
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
            GameObject grabberObj = grabbable.grabbedBy().gameObject;

            // 火柱オブジェクトの角度調整1(手に対して上方向)
            Quaternion rot1 = Quaternion.Euler(0,0,0);

            // 火柱オブジェクトの角度調整2(上方向から前方向への調整)
            Quaternion rot2 = Quaternion.Euler(0,0,0);

            // 火柱のインスタンス生成
            FireObj = Instantiate(Effect_Prefab, this.gameObject.transform.position, rot1+rot2);
            FireObj.transform.parent = this.gameObject.transform;
        }else{
            FireObj.transform.parent = null;
            Destroy(FireObj);
            FireObj = null;
        }
    }
}
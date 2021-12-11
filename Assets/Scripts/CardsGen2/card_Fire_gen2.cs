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

    // isReady T/F でテクスチャが変わるように！
    [SerializeField]
    protected Material ReadyMat = null, NotReadyMat = null;

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

        // 手から放したら炎は消える
        if(!getIsHold()){
            if(FireObj != null){
                Destroy(FireObj);
                FireObj = null;
                this.GetComponent<Renderer>().material = NotReadyMat;
            }
        }
    }

    public override void use(Collision other = null){
        this.sc.fm.setFlag("useFire", true);
        outputLog("use() start");
        if(FireObj == null){
            this.GetComponent<Renderer>().material = ReadyMat;
            OVRGrabbable grabbable;
            if(!this.gameObject.TryGetComponent(out grabbable)){
                outputError("can't found OVRGrabbable");
                return;
            }
            // OVRGrabberを持ってるオブジェクト=手のGameObjectをとってくる
            GameObject grabberObj = grabbable.grabbedBy.gameObject;

            // 火柱オブジェクトの角度調整変数
            float[] rot = new float[3]{0,0,0};

            // 火柱のインスタンス生成
            FireObj = Instantiate(Effect_Prefab, this.gameObject.transform.position, Quaternion.Euler(rot[0], rot[1], rot[2]));
            FireObj.transform.parent = grabberObj.gameObject.transform;

            Invoke(nameof(DelayMethod), 20.0f);
        }else{
            this.GetComponent<Renderer>().material = NotReadyMat;
            FireObj.transform.parent = null;
            Destroy(FireObj);
            FireObj = null;
        }
    }

    void DelayMethod(){
        Destroy(FireObj);
        FireObj = null;
    }
}
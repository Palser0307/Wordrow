using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Tree_gen2 : TrO_card_class{
    // 丸太のPrefabは継承元クラスで宣言してる
    // [SerializeField]
    // protected GameObject Effect_Prefab;

    // pluralのカードオブジェクト
    [SerializeField]
    protected GameObject plural_card;

    // 丸太出現の起点の高さ
    private float height = 0.2f;

    // くっつけられるUpgrader名リスト
    protected List<string> jointableUpgraderList = new List<string>();

    // くっついたUpgraderとそのスクリプト
    protected GameObject UpgraderObj = null;
    protected upgrader UpgraderScript = null;

    // Flag
    protected tutorial_flags tf;

    // 丸太を一括管理するGameObject
    // 生成した丸太は全てここに
    // ちゃんとインスペクタでアタッチしておくこと
    [SerializeField]
    protected GameObject Trees;
    protected Trees TreeS;

    // 初期設定
    // 初ロード時に叩かれる
    public new void Start(){
        base.Start();
        // カード名設定
        setCardName("Tree");

        addJointableUpgraderList("plural");

        outputLog("Setup finish.");

        // フラグシステムテスト
        tf = GameObject.Find("System_Controller").GetComponent<tutorial_flags>();

        TreeS = Trees.GetComponent<Trees>();
        if(TreeS == null){
            outputLog("TreeS is NULL!!");
        }
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

        outputLog("Tree_Object appear.");
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
        //Instantiate(Effect_Prefab, pos, rot);
        TreeS.addList(Instantiate(Effect_Prefab, pos, rot));
    }

    protected void OnCollisionEnter(Collision other){
        outputLog("Oncollision Enter");
        // Upgrader接触時
        if(other.gameObject.tag == "Upgrader" && UpgraderObj == null){
            UpgraderObj = other.gameObject;
            UpgraderScript = UpgraderObj.GetComponent<upgrader>();
            if(UpgraderScript == null ||
                !haveJointableUpgraderList(UpgraderScript.getUpgraderName())){
                outputLog("reject"+(UpgraderScript!=null)+" "+!haveJointableUpgraderList(UpgraderScript.getUpgraderName()));
                outputLog(UpgraderScript.getUpgraderName());
                UpgraderObj = null;
                return;
            }

            outputLog("蝶☆合体");

            // カード生成
            GameObject card = Instantiate(this.plural_card,
            this.transform.position + Vector3.up * 0.1f,
            this.transform.rotation);

            // 丸太をまとめるGameObjectをアタッチする
            card.GetComponent<card_TreeS_plural>().setTreeS(Trees);

            // どっちかが握られてたら，その解除をする
            // TODO: UpgraderObj.grab == UpgraderObj_grab
            OVRGrabbable UpgraderObj_grab = this.UpgraderObj.GetComponent<OVRGrabbable>();
            if(grab.isGrabbed){
                // Grabberの把持解除
                OVRGrabber grabber = this.grab.grabbedBy;
                grabber.ForceRelease(this.grab);
            }else if(UpgraderObj_grab.isGrabbed){
                // Upgrager側をGrabしてるGrabberの把持解除
                OVRGrabber grabber = UpgraderObj_grab.grabbedBy;
                grabber.ForceRelease(UpgraderObj_grab);
            }

            // TEST
            // フラグ管理
            tf.Metamorphose = true;

            // カード及びアプグレのオブジェクト削除
            Destroy(this.UpgraderObj);
            Destroy(this.gameObject);
        }
    }

    // GETTER
    protected List<string> getJointableUpgraderList(){
        return this.jointableUpgraderList;
    }

    protected bool haveJointableUpgraderList(string UpgraderType){
        return this.jointableUpgraderList.Contains(UpgraderType);
    }

    // SETTER
    protected bool addJointableUpgraderList(string newUpgraderType){
        if(!haveJointableUpgraderList(newUpgraderType)){
            this.jointableUpgraderList.Add(newUpgraderType);
            return true;
        }else{
            return false;
        }
    }

}
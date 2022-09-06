using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_createShapes_gen2 : TD_card_class
{
    public GameObject createSprite;

    // 初期設定
    // 初ロード時に叩かれる
    protected new void Start(){
        base.Start();
        // カード名設定
        setCardName("createShapes");

        // 発動対象タグの設定
        // 今のところはタグだけ
        addTargetList("Target");

        outputLog("Setup finish.");
    }

    // フレームごとに叩かれる
    protected new void Update(){
        // 継承元クラスのUpdate()を呼び出す
        //base.Update();

        updateIsHold();
        vectorZero();
    }

    // 実際の効果
    // 台座（Target）の上にShapeを生成する
    // 発動対象タグを持っているかはOnCollisionEnter()で確認済み

    //collisionのほうは使わないので必要ないです
    public override void use(Collision other){
        outputLog("use func. start.");

        playAudio();

        //collision のTransformの位置にShapeを召喚

        Transform trans= other.gameObject.GetComponent<Transform>();

        Instantiate(createSprite,trans.position,trans.rotation);

    }
    public override void use(Collider other){
        outputLog("use func. start.");

        playAudio();

        //collision のTransformの位置にShapeを召喚
        Transform trans= other.gameObject.transform.Find("PlayerAnswer_Pillar").GetComponent<Transform>();

        Vector3 vecrot=trans.rotation.eulerAngles;

        vecrot.y=90;

        Vector3 vecpos = trans.position;

        vecpos.y = 1.5f;

        //生成
        var gosp = Instantiate(createSprite,vecpos,Quaternion.Euler(vecrot),trans) as GameObject;
        //名前をセット
        gosp.name = createSprite.name;

        GameObject go = other.gameObject;

        //生成したよフラグ
        if(other.gameObject.TryGetComponent(out QuizSystem  QuizSystem)){
            other.gameObject.GetComponent<QuizSystem>().isPlayerAnswerGenerated = true;
        }
        


        //カードを離して消滅させる
        this.gameObject.GetComponent<OVRGrabbable>().grabbedBy.ForceRelease(this.gameObject.GetComponent<OVRGrabbable>());
        Destroy(this.gameObject);

    }
}

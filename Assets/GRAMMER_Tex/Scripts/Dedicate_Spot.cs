using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カード置き場
//Collidar内で最後に持たれたカードの名前のテクスチャに切り替える

public class Dedicate_Spot : MonoBehaviour
{
    [SerializeField]

    //カードをはめる位置
    private GameObject display;

    //displayするマテリアル
    //将来的にはフォルダから名前で取ってきたい
    [SerializeField]
    private Texture2D[] texes;

    //texesと同じ順番で入れてくれ
    [SerializeField]
    private GameObject[] effects;

    //エフェクトが出る場所を保存
    //その場所にCreateEmpty等作ってしまえばよいではないか
    [SerializeField]
    private Transform effectTransform;

    //現在のTexに対応するeffectsの番号を保存
    private int effect_num=0;

    //effectを保持する変数
    public GameObject nowEffect = null;

    //文が決定されたかどうか
    bool determine=false;

    // フラグ関連 てかストーリー管理スクリプトへのアクセサ
    protected Story_Controller sc = null;

    private void Start() {
        sc = GameObject.Find("System_Scripts").GetComponent<Story_Controller>();
        if(sc == null){
            outputError("Story_Controller is not FOUND!");
        }else{
            outputLog("start finish");
        }
    }

    void Update(){

        //文が決定された
        if(determine==true){
            //今のエフェクトを消す
            if(nowEffect!=null){
                Destroy(nowEffect);
            }

            //新しいエフェクトを入れる
            nowEffect=Instantiate(effects[effect_num],effectTransform);

            determine=false;
        }
    }

    void OnTriggerEnter(Collider collision){
        GameObject go = collision.gameObject;

        //TagがCardであるオブジェクトと衝突したら，displayのテクスチャを切り替える
        if(go.tag == "Card"){

            string name=go.name;

            for(int i=0;i<texes.Length;i++){
                if(name == texes[i].name){
                    display.GetComponent<Renderer>().material.SetTexture("_MainTex",texes[i]);

                    //現在の選択を保存
                    effect_num=i;
                    Debug.Log("now EFFECT_NUM IS " + effect_num);
                    determine=true;

                    // フラグ
                    if(sc!=null && name=="tree"){
                        sc.fm.setFlag("isAlphaTree", true);
                    }
                }
            }

        }

    }

    // Log output
    protected void outputLog(string str){
        Debug.Log("Dedicate_Spot : " + str);
    }
    // Error Log output
    protected void outputError(string str){
        Debug.LogError("Dedicate_Spot : "+str);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// in "System_Scripts" Object
public class Alpha_Controller : MonoBehaviour{
    // HUDアクセサ
    [SerializeField]
    protected GameObject Alpha_HUD = null;

    // HUDアバター部分のアクセサ
    protected GameObject Alpha_AvaterObj;


    // HUDテキスト部分のアクセサ
    protected GameObject Alpha_TextObj;

    // HUDテキスト内のTextObjへのアクセサ
    protected GameObject Alpha_Text_string;

    protected Text text_string;


    // あーちゃんセリフ格納配列
    protected string[] alpha_wordsArray;

    // System_Controllerってオブジェクトにアタッチされてるんで
    protected GameObject Sys_Controller;

    // 用途不明カウント
    protected static int cnt = 0;

    // 現在のセリフ位置
    public int strPos = 0;

    // あーちゃんのシナリオ同期，管理用
    // active: あーちゃんのセリフ送り可能
    // inactive: あーちゃんのセリフ送り停止
    // next: 外部からのセリフ送り要求
    public string Status{get;set;} = "active";

    // セリフ更新可能時のLED
    // 要はポケモンのセリフウィンドウとかの下矢印みたいなもんだね
    // +---------------------+
    // |                     |
    // |            これ→   ↓|
    // +---------------------+
    protected GameObject Alpha_Led = null;

    void Awake(){
        // Alpha_HUDを探す
        if(Alpha_HUD == null){
            Alpha_HUD = GameObject.Find("Alpha_HUD");
        }

        Sys_Controller = this.gameObject;

        /*
        // 各GameObjectへのアクセサを代入
        Alpha_AvaterObj = GameObject.Find("Alpha_Avater");
        Alpha_TextObj = GameObject.Find("Alpha_Text");
        Alpha_Text_string = GameObject.Find("Text_string");
        text_string = Alpha_Text_string.GetComponent<Text>();
        */

        Transform children = Alpha_HUD.GetComponentInChildren<Transform>();
        if(children.childCount == 0){
            outputLog("this object haven't child object.");
        }
        foreach(Transform obj in children){
            switch (obj.gameObject.name){
                case "Alpha_Avater":
                    outputLog("set Alpha_Avater");
                    Alpha_AvaterObj = obj.gameObject;
                    break;
                case "Alpha_Text":
                    outputLog("set Alpha_Text");
                    Alpha_TextObj = obj.gameObject;
                    Alpha_Text_string = Alpha_TextObj.transform.GetChild(1).gameObject;
                    text_string = Alpha_Text_string.GetComponent<Text>();
                    if(text_string == null){
                        outputError("text_string is null");
                    }
                    text_string.text = wakeUpMessage;
                    break;
                case "Alpha_Led":
                    outputLog("set Alpha_Led");
                    Alpha_Led = obj.gameObject;
                    Alpha_Led.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        // とりあえず起動確認用のMessageを出力
        text_string.text = wakeUpMessage;
        reloadStr();
        outputLog("start() finished");
    }

    private void Start() {
        return;
    }

    void Update(){
        switch (Status){
            case "active":
                if(OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)){
                    nextStr();
                }
                Alpha_Led.SetActive(true);
                break;

            case "next":
                nextStr();
                Status = "active";
                break;

            case "inactive":
                Alpha_Led.SetActive(false);
                break;
            default:
                break;
        }
    }

    // セリフ送り
    public void nextStr(){
        if(strPos < words.Length-1){
            strPos++;
        }else{
            //strPos = 0;
        }
        string text = words[strPos];
        text_string.text = text;
        //outputLog(text_string.text);
    }

    // セリフリロード
    public void reloadStr(){
        string text = words[strPos];
        text_string.text = text;
    }

    // セリフ位置リセット
    public void resetStr(){
        strPos = 0;
        reloadStr();
    }

    // セリフ出力
    public void outputStr(string str){
        text_string.text = str;
    }

    /*
    +------------------------+
    | あーちゃん用セリフ関連 |
    |     セリフ内蔵版       |
    +------------------------+
    */
    // セリフのstring配列
    public string[] words = {
        "セリフ更新待ち～",
        "ヒャッハー！",
        "あべし！",
    };
    public static string wakeUpMessage = "毎日はEveryday";

    protected void outputLog(string str){
        Debug.Log("a_Ctrller.cs : " + str);
    }
    protected void outputError(string str){
        Debug.LogError("a_Ctrller.cs : " + str);
    }

}
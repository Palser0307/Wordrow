using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// in "System_Scripts" Object
public class Alpha_Controller : MonoBehaviour{
    // HUDアクセサ
    [SerializeField]
    protected GameObject Alpha_HUD;

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

    void Start(){
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
                    outputLog("Alpha_Avater");
                    Alpha_AvaterObj = obj.gameObject;
                    break;
                case "Alpha_Text":
                    outputLog("Alpha_Text");
                    Alpha_TextObj = obj.gameObject;
                    Alpha_Text_string = Alpha_TextObj.transform.GetChild(1).gameObject;
                    text_string = Alpha_Text_string.GetComponent<Text>();
                    if(text_string == null){
                        outputError("text_string is null");
                    }
                    text_string.text = wakeUpMessage;
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

    void Update(){
        switch (Status){
            case "active":
                if(OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)){
                    nextStr();
                }
                break;

            case "next":
                nextStr();
                Status = "active";
                break;

            case "inactive":
            default:
                break;
        }
    }

    // セリフ送り
    public void nextStr(){
        if(strPos < words.Length-1){
            strPos++;
        }else{
            strPos = 0;
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


    /*
    +------------------------+
    | あーちゃん用セリフ関連 |
    |     セリフ内蔵版       |
    +------------------------+
    */
    // セリフのstring配列
    public string[] words = {
        "・・・",
        "マスターの生体情報を確認中・・・",
        "・・・",
        "データベースに事前登録されている生体情報との一致を確認．",
        "I’m OK, My master.",
        "・・・初めまして，マスター．",
        "あなた様のアシスタントを務める，α(アルファ)と申します．",
        "以後，お見知りおきを．",
        "早速ですが，チュートリアルを開始させていただきます．",
        "あたりを見渡してみてください．",
        "目の前にあるカードが見えますか？\nそのカードを手に取ってみてください．",
        "カードがつかめましたね．",
        "この世界ではそのカードを使って様々なことができます．",
        "実際に試してみましょう．",
        "目の前が燃えていますね．",
        "さて，このままだと危険なので，火を消してみましょう．",
        "手に持っているカードを使ってみてください．",
        "どうやら燃えているものにはこのカードは使わないほうが良いようですね．",
        "それでは，今度はこちらのカードを使ってみてください．",
        "火が消えてくれましたね．",
        "このように，状況に合わせて使うカードを選択することが大事です．",
        "それでは，最後のチュートリアルに移りましょう．",
        "そのカードを使ってこの部屋から出てみましょう．",
        "前方の扉にカードを近づけてください．",
        "無事に扉が開いたようですね．",
        "以上でチュートリアルを終了します．",
        "Have a nice trip, Master.",
    };
    public static string wakeUpMessage = "毎日はEveryday";

    protected void outputLog(string str){
        Debug.Log("a_Ctrller.cs : " + str);
    }
    protected void outputError(string str){
        Debug.LogError("a_Ctrller.cs : " + str);
    }

}
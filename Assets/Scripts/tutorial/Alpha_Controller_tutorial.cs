﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// in "System_Scripts" Object
public class Alpha_Controller_tutorial : MonoBehaviour{
    [SerializeField]
    protected GameObject Alpha_HUD;
    [SerializeField]
    protected GameObject Alpha_Text;

    protected GameObject alpha_avater;
    protected GameObject alpha_text;
    // Alpha_HUDのテキスト部分オブジェクト
    protected GameObject Text_string;
    protected Text text_string;

    protected string[] alpha_wordsArray;

    // in System_Scripts Object
    protected GameObject Sys_Scripts = null;
    public Alpha_Words alpha_Scinario;

    protected static int cnt = 0;

    // あーちゃんのシナリオ同期，管理用
    // active: あーちゃんのセリフ送り可能
    // inactive: あーちゃんのセリフ送り停止
    protected static string status = "active";

    void Start(){
        Sys_Scripts = GameObject.Find("System_Scripts");
        alpha_Scinario = new Alpha_Words();
        // alpha_Scinario = (Alpha_Words)Alpha_Words.loadFromJSON();
        // Debug.Log(Alpha_Words.loadFromJSON().num);
        // Debug.Log("Length:"+alpha_Scinario.num);
        // alpha_Scinario.ToString();
        // Debug.Log(alpha_Scinario);

        // Text_stringを取得
        Text_string = Alpha_Text.transform.Find("Text_string").gameObject;
        text_string = Text_string.GetComponent<Text>();
        Debug.Log("text:"+text_string.text);
        text_string.text = wakeUpMessage;
    }

    void Update(){
        string nxt_str = "";
        /*
        if(cnt-- <= 0){
            cnt = 100;
            nxt_str = next_str();
            Debug.Log(nxt_str);
            text_string.text = nxt_str;
        }*/
        // Debug.Log("cnt:"+cnt);
        if(status == "inactive"){
            return;
        }
        if(OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)){
            Debug.Log(Sys_Scripts.GetComponent<Controller>().GetLHold());
            // next();
            // Debug.Log(next_str());
            nxt_str = next_str();
            Debug.Log(nxt_str);
            text_string.text = nxt_str;
        }
    }

    // 文字送り(つまり次のセリフへの遷移)
    // nextWord? 関数名わからん
    // 多分Controller.csから呼ばれると思う
    public void next(){
        Debug.Log("run : next()");
    }

    // セリフをロード
    // Resources/Alpha/ file_name のデータをロード
    void loadWords(string file_name){

    }

    /*
    +-----------------+
    | GETTER / SETTER |
    +-----------------+
    */
    // HUD_Alpha turn off
    public void turnOff(){
        Alpha_HUD.SetActive(false);
    }

    // HUD_Alpha turn on
    public void turnOn(){
        Alpha_HUD.SetActive(true);
    }

    // HUD_Alpha is active?
    public bool getActive(){
        return Alpha_HUD.activeSelf;
    }

    // あーちゃんstatusのセット
    public static void setStatus(string newStatus){
        if(
            newStatus == "active"
            || newStatus == "inactive"
        ){
            status = newStatus;
        }else{
            Debug.Log(newStatus + "is not STATUS name.");
        }
    }

    /*
    +----------------------+
    | あーちゃん用セリフ関連 |
    |     セリフ内蔵版      |
    +----------------------+
    */
    // セリフのstring配列
    public static string[] words = {
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

    // 現在のセリフの位置
    public static uint strPos = 0;
    // 今のセリフを返す
    public string now_str(){
        if(strPos >= words.Length){
            return (string)"セリフは以上です";
        }
        return words[strPos];
    }
    // 次のセリフを返す
    public string next_str(){
        strPos++;
        if(strPos >= words.Length){
            strPos = 0;
            return (string)"セリフは以上です．\nこれ以上は更新できません．";
        }
        return words[strPos];
    }

    //シナリオ用
    public uint getStrPos(){
        return strPos;
    }
}
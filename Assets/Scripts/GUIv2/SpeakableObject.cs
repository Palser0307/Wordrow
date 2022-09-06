using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// セリフのデータ
// 文字列と表示時間を持つ
[Serializable]
public class ScenarioLine{
    protected string word{get;}
    protected float delay{get;}

    public ScenarioLine(string w = "", float d = 5f){
        word = w;
        delay = d;
    }

    public string getWord(){
        return word;
    }
    public float getDelay(){
        return delay;
    }
}

public class SpeakableObject : MonoBehaviour{
    // 入力されるコマンドー
    [SerializeField]
    protected string speakString = "";

    // セリフ一覧
    protected Dictionary<string, List<ScenarioLine>> speakWords = new Dictionary<string, List<ScenarioLine>>();

    // セリフ用Key一覧
    // keyの存在確認用に置いているが，上のdicの既存関数にkey検索があるなら，関係各所を修正の上削除する
    protected List<string> keyList = new List<string>();

    // 表示させるHUDのオブジェクト
    protected GameObject SpeakerHUD_ctrller_Object = null;

    // 表示させるHUDのオブジェクトのHUD_Controllerコンポーネントへのアクセサ
    protected HUD_Controller SpeakerHUD_ctrller_Component = null;

    protected void Start(){
        // セリフの構文解析
        SeparateString();
        // セリフ一覧の出力
        // outputDic();

        // HUDの初期設定
        // HUD_Controllerを持ってるはずのHUDオブジェクトを探す
        search_HudCtrller_Component();
    }

    // speakStringの構文解析
    public bool SeparateString(){
        /*
        $key=init;
        はじめまして%0.1;
        // コメントアウト;
        alphaです%0.2;
        $end;
        */
        // $key=init;はじめまして%0.1;// コメントアウト;alphaです%0.2;$end;

        // $key=alpha;はじめまして%2;//;alphaです%2;$key=beta;はじめまして%2;//;betaです%2;$end;

        Debug.Log("SeparateString : run");
        if(speakString == ""){
            Debug.LogWarning("SeparateString : NEED STRING");
            return false;
        }
        // speakStringを";"でsplit
        string[] lines = speakString.Split(';');
        // 各行頭を見て，
        // "$" : コマンド
        // "/" : 次の文字が"/"ならコメントアウト
        // other : それもうセリフってことで
        string keyTemp = "";
        List<ScenarioLine> slTemp = new List<ScenarioLine>();
        foreach(string line in lines){
            Debug.Log(line);
            if(line == ""){
                continue;
            }
            switch(line[0]){
                case '$':
                    Debug.Log("Type : command");
                    // 二文字目以降の取得
                    string lineTemp = line.Substring(1,line.Length-1);
                    string[] a = lineTemp.Split('=');
                    if(a.Length == 2){
                        if(a[0] == "key"){
                            Debug.Log("Split Result: \"" + a[0] +"\" = \"" + a[1] + "\"");
                        }else{
                            Debug.Log("command ERROR");
                            break;
                        }
                    }else{
                        if(a[0] != "end"){
                            Debug.Log("split ERROR");
                            break;
                        }else{
                            speakWords.Add(keyTemp, slTemp);
                            break;
                        }
                    }
                    if(keyTemp != "" && keyTemp != a[1]){
                        speakWords.Add(keyTemp, slTemp);
                        slTemp = new List<ScenarioLine>();
                    }
                    keyTemp = a[1];
                    break;
                case '/':
                    if(line[1] == '/'){
                        Debug.Log("Type : comment out");
                    }else{
                        Debug.Log("TypeERROR : if you use comment?");
                    }
                    break;
                default:
                    Debug.Log("Type : line");
                    string[] b = line.Split('%');
                    if(b.Length != 2){
                        Debug.Log("SplitERROR");
                        break;
                    }else{
                        Debug.Log("Split Result: \"" + b[0] +"\" = \"" + b[1] + "\"");
                    }
                    slTemp.Add(new ScenarioLine(b[0], float.Parse(b[1])));
                    break;
            }
            Debug.Log("next");
        }
        // セリフなら，"%"を見つける
        // "%"の前がセリフ，後が遅延時間(表示時間)

        // keyListの更新
        updateKeyList();

        Debug.Log("end.");
        return true;
    }

    // セリフ一覧の出力
    void outputDic(){
        Debug.Log("outputDic run");
        foreach(var keyValuePair in this.speakWords){
            foreach(ScenarioLine line in keyValuePair.Value){
                Debug.Log(keyValuePair.Key + ":" + line.getWord() + " ,, " + line.getDelay());
            }
        }
        Debug.Log("end..");
    }

    // keyNameのセリフ一覧を取得する
    public List<ScenarioLine> getWord(string keyName){
        bool isExistWordKey = this.searchWordKey(keyName, true);
        if(isExistWordKey == true){
            return this.speakWords[keyName];
        }else{
            return null;
        }
        //return new ScenarioLine();
    }

    // キーとしてKeyNameを持つかの検索
    // あったらTrue，無かったらFalse
    public bool searchWordKey(string keyName, bool outputDebugLogs = false){
        // keyNameが存在するか
        bool isExistKeyName = this.speakWords.ContainsKey(keyName);
        // 無かった場合のエラー
        if(isExistKeyName == false && outputDebugLogs == true){
            outputError(keyName + " is not found.");
        }else{
            outputLog(keyName + " is found.");
        }
        return isExistKeyName;
    }

    // keyListの更新
    public void updateKeyList(){
        List<string> newList = new List<string>();
        foreach(var keyValuePair in this.speakWords){
            newList.Add(keyValuePair.Key);
        }
        keyList = newList;
    }

    // HUD_Controllerコンポーネントを持ってるはずのHUDオブジェクトを探す
    public bool search_HudCtrller_Component(){
        SpeakerHUD_ctrller_Object = GameObject.FindGameObjectWithTag("HUD");
        if(SpeakerHUD_ctrller_Object == null){
            return false;
        }
        if(SpeakerHUD_ctrller_Object.TryGetComponent(out this.SpeakerHUD_ctrller_Component) == false){
            return false;
        }
        return true;
    }

    // Log出力系
    protected virtual void outputLog(string str){
        Debug.Log("SpeakableObject.cs: " + str);
    }
    protected virtual void outputWarning(string str){
        Debug.LogWarning("SpeakableObject.cs: " + str);
    }
    protected virtual void outputError(string str){
        Debug.LogError("SpeakableObject.cs: " + str);
    }
}

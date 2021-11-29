using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour{
    // ストーリー名
    protected string storyName;

    // フラグ辞書
    protected Dictionary<string, object> flagDictionary;

    // コンストラクタ
    public FlagManager(string name){
        storyName = name;
        flagDictionary = new Dictionary<string, object>();
    }

    // フラグkeyNameを取得する
    public object getFlag(string keyName){
        bool isExist = searchFlagKey(keyName);
        object returnValue = null;
        if(isExist == true){
            returnValue = flagDictionary[keyName];
        }
        return returnValue;
    }

    // フラグkeyNameをnewValueで上書きする
    public bool setFlag(string keyName, object newValue){
        bool canSetFlag = searchFlagKey(keyName);
        if(canSetFlag == true){
            flagDictionary[keyName] = newValue;
            outputLog(keyName + ": ->" + newValue);
        }
        return canSetFlag;
    }

    // フラグkeyNameをinitで宣言
    public void initFlag(string keyName, object init){
        // keyNameが存在するか
        bool isExist = searchFlagKey(keyName);
        // keyNameが存在しなければフラグkeyNameを追加し，initで初期化
        if(isExist == false){
            flagDictionary[keyName] = init;
            outputLog(keyName + " is initialize");
        }
    }

    // keyNameのFlagがあるか
    public bool searchFlagKey(string keyName){
        // keyNameが存在するか(is exist)の変数
        bool isExist = flagDictionary.ContainsKey(keyName);
        // 無かったときのエラー文
        if(isExist == false){
            //outputLog(keyName + " is not found.");
        }
        return isExist;
    }

    public void outputLog(string str){
        Debug.Log("FlagManager: " + str);
    }
    public void outputError(string str){
        Debug.LogError("FlagManager: " + str);
    }
}

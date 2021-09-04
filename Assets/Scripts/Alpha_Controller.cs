using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha_Controller : MonoBehaviour{
    [SerializeField]
    protected GameObject Alpha_HUD;

    protected GameObject alpha_avater;
    protected GameObject alpha_text;

    protected string[] alpha_wordsArray;

    void Start(){

    }

    void Update(){

    }

    // 文字送り(つまり次のセリフへの遷移)
    // nextWord? 関数名わからん
    // 多分Controller.csから呼ばれると思う
    public void next(){

    }

    // セリフをロード
    // Resources/Alpha/ file_name のデータをロード
    void loadWords(string file_name){

    }

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
}

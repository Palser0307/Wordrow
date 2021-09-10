using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// in "System_Scripts" Object
public class Alpha_Controller : MonoBehaviour{
    [SerializeField]
    protected GameObject Alpha_HUD;

    protected GameObject alpha_avater;
    protected GameObject alpha_text;

    protected string[] alpha_wordsArray;

    // in System_Scripts Object
    protected GameObject Sys_Scripts = null;

    void Start(){
        Sys_Scripts = GameObject.Find("System_Scripts");
    }

    void Update(){
        if(OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)){
            Debug.Log("run : next()");
            Debug.Log(Sys_Scripts.GetComponent<Controller>().GetLHold());
            next();
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

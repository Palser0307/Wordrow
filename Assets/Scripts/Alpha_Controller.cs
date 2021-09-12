using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// in "System_Scripts" Object
public class Alpha_Controller : MonoBehaviour{
    [SerializeField]
    protected GameObject Alpha_HUD;

    protected GameObject alpha_avater;
    protected GameObject alpha_text;

    protected string[] alpha_wordsArray;

    // in System_Scripts Object
    protected GameObject Sys_Scripts = null;
    public Alpha_Words alpha_Scinario = null;

    protected static int cnt = 0;

    void Start(){
        Sys_Scripts = GameObject.Find("System_Scripts");
        alpha_Scinario = loadFromJSON();
        alpha_Scinario.ToString();
    }

    void Update(){
        if(OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)){
            // Debug.Log("run : next()");
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

    // JSONファイルの読み込み，オブジェクト化
    private string _dataPath;
    protected Alpha_Words alpha_words;
    public Alpha_Words loadFromJSON(){
        string inputString = Resources.Load<TextAsset>("Alpha/tutorial").ToString();
        Alpha_Words obj = JsonUtility.FromJson<Alpha_Words>(inputString);
        return obj;
    }
}

public class Alpha_Words{
    public Context[] num;
    public override string ToString(){
        string return_String = "";
        for(int i=0; i</*this.num.Length*/20; i++){
            return_String += "{id:"+this.num[i].id+", name:"+this.num[i].name+", text:"+this.num[i].text+"},";
        }
        return return_String;
    }
}
public class Context{
    public int id;
    public string name;
    public string text;
    public override string ToString(){
        return "{id:"+this.id+", name:"+this.name+", text:"+this.text+"}";
    }
}

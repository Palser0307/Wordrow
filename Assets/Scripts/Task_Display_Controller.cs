using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;// TextMeshPro用にinclude

public class Task_Display_Controller : MonoBehaviour {
    // Displayアクセサ
    [SerializeField]
    protected GameObject Task_Display = null;

    // TextMeshPro側オブジェクトのアクセサ
    protected GameObject Task_TextObj = null;

    // TextMeshProコンポーネントのアクセサ
    protected TextMeshPro text_Obj;

    // 表示文字列の待機変数
    public string displayString = "";

    // 外部から設定されている表示/非表示設定
    protected bool isActive = true;

    private void Awake() {
        Task_Display = GameObject.Find("Task_Display");
        Transform children = Task_Display.GetComponentInChildren<Transform>();
        foreach(Transform obj in children){
            switch(obj.gameObject.name){
                case "Text (TMP)":
                    Task_TextObj = obj.gameObject;
                    if(Task_TextObj.TryGetComponent(out text_Obj)){
                        outputLog("OK");
                        reloadStr();
                    }else{
                        outputLog("NG");
                    }
                    break;
                default:
                    break;
            }
        }
        // reloadStr();
        this.setActive(false);
    }

    private void Start() {
        return;
    }

    private void Update() {
        // 左手Xボタン(手前)押下時に，表示/非表示の切替
        if(OVRInput.GetDown(OVRInput.RawButton.X)){
            // 外部からの設定参照
            if(isActive == true){
                Task_Display.SetActive(!Task_Display.activeSelf);
            }
        }
    }

    // 有効化/無効化
    // newStatus : 有効化/無効化
    public void setActive(bool newStatus){
        if(isActive != newStatus){
            outputLog("setActive() -> "+newStatus);
            Task_Display.SetActive(newStatus);
            isActive = newStatus;
        }
    }

    public void reloadStr(){
        text_Obj.text = displayString;
    }

    // 文字列の置換
    // てか更新…？
    public void replaceStr(string newStr, bool needReload){
        displayString = newStr;
        if(needReload == true){
            reloadStr();
        }
    }

    // 文字列の消去
    public void deleteStr(bool needReload){
        displayString = "";
        if(needReload == true){
            reloadStr();
        }
    }

    // Log出力系
    void outputLog(string str){
        Debug.Log("TaskDisplayCtrl.cs : "+str);
    }
    void outputError(string str){
        Debug.LogError("TaskDisplayCtrl.cs : "+str);
    }
}
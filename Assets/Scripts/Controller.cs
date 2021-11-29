using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// in "System_Scripts" Object
public class Controller : MonoBehaviour{
    [SerializeField]
    GameObject leftController;
    [SerializeField]
    GameObject rightController;

    // HUDはプレイヤのGameObjectの子に入れておく
    // 以下3つはHUDsの子オブジェクトになってる…はず
    [SerializeField]
    GameObject HUD_Menu;
    [SerializeField]
    GameObject HUD_Tip;
    [SerializeField]
    GameObject HUD_Alpha;

    protected GameObject Grab_Object;

    // Is L/R Hand hold?
    protected bool LisHold = false;
    protected bool RisHold = false;

    // 掴めるもののタグリスト
    protected List<string> grabbableTagList = new List<string>();

    void Start(){
        HUD_Menu.SetActive(false);
        HUD_Tip.SetActive(false);
        addGrabbableTag("Card");
        addGrabbableTag("Grabbable");
        addGrabbableTag("Upgrader");
        Grab_Object = null;
    }

    void Update(){
        // HUD Menu表示/非表示
        if(OVRInput.GetDown(OVRInput.RawButton.B)){
            Debug.Log("Push B Button");
            bool menuActive = HUD_Menu.activeSelf;
            bool tipActive = HUD_Tip.activeSelf;
            HUD_Menu.SetActive(!menuActive);
            HUD_Tip.SetActive(!tipActive);
        }
    }

    //
    // GETTER / SETTER
    //
    public void SetLHold(bool newStatus){
        LisHold = newStatus;
    }
    public void SetRHold(bool newStatus){
        RisHold = newStatus;
    }

    public bool GetLHold(){
        return LisHold;
    }
    public bool GetRHold(){
        return RisHold;
    }

    public bool haveGrabbableTag(string tag){
        return this.grabbableTagList.Contains(tag);
    }
    public List<string> getGrabbableTag(){
        return this.grabbableTagList;
    }

    public bool addGrabbableTag(string tag){
        if(!haveGrabbableTag(tag)){
            this.grabbableTagList.Add(tag);
            return true;
        }else{
            return false;
        }
    }
}

public static class ResourcesFilesPath{
    public const string JSON_tutorial = "tutorial.json";
}
public static class ResourcesDirectoryPath{
    public const string CHILD = "Child";
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// in "System_Scripts" Object
public class Controller : MonoBehaviour{
    [SerializeField]
    GameObject leftController;
    [SerializeField]
    GameObject rightController;
    [SerializeField]
    GameObject Deck_Prefab;

    // HUDはプレイヤのGameObjectの子に入れておく
    // 以下3つはHUDsの子オブジェクトになってる…はず
    [SerializeField]
    GameObject HUD_Menu;
    [SerializeField]
    GameObject HUD_Tip;
    [SerializeField]
    GameObject HUD_Alpha;

    protected GameObject Deck_Object;

    protected GameObject Grab_Object;

    // Is L/R Hand hold?
    protected bool LisHold = false;
    protected bool RisHold = false;

    // 掴めるもののタグリスト
    protected List<string> grabbableTagList = new List<string>();

    void Start(){
        Deck_Object = null;
        HUD_Menu.SetActive(false);
        HUD_Tip.SetActive(false);
        addGrabbableTag("Card");
        addGrabbableTag("Grabbable");
        addGrabbableTag("Upgrader");
        Grab_Object = null;
    }

    void Update(){
        /*
        // 掴む処理
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)){
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(rightController.transform.position, 0.01f, Vector3.forward);
            foreach(var hit in hits){
                if(haveGrabbableTag(hit.collider.tag)){
                    // Debug.Log("Grip");
                    Grab_Object = hit.collider.gameObject;
                    Grab_Object.transform.parent = rightController.transform;
                    SetRHold(true);
                    break;
                }
            }
        }

        // 離す処理
        if(OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)){
            if(Grab_Object != null){
                Grab_Object.transform.parent = null;
                Grab_Object = null;
            }
        }
        */

        // HUD Menu表示/非表示
        if(OVRInput.GetDown(OVRInput.RawButton.B)){
            Debug.Log("Push B Button");
            bool menuActive = HUD_Menu.activeSelf;
            bool tipActive = HUD_Tip.activeSelf;
            HUD_Menu.SetActive(!menuActive);
            HUD_Tip.SetActive(!tipActive);
        }

        /*
        // 山札出現・消滅
        if(OVRInput.GetDown(OVRInput.RawButton.X)){
            Debug.Log("Push X Button");
            if(Deck_Object == null){
                Debug.Log("Deck appear");
                Deck_Object = Instantiate(Deck_Prefab);
                Deck_Object.transform.position = leftController.transform.position + leftController.transform.forward * 0.1f;
            }else{
                Debug.Log("Deck disappear");
                Destroy(Deck_Object);
                Deck_Object = null;
            }
        }

        // 山札の上にカード出現
        if(OVRInput.GetDown(OVRInput.RawButton.Y)){
            Debug.Log("Push Y Button.");
            if(Deck_Object != null){
                Debug.Log("Card appear");
                GameObject go = CreateCard();
                go.transform.position = Deck_Object.transform.position + Deck_Object.transform.up * 0.5f;
            }
        }
        */
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// in "System_Scripts" Object
public class Controller_tutorial : MonoBehaviour{
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

    // Is L/R Hand hold?
    protected bool LisHold = false;
    protected bool RisHold = false;

    void Start(){
        HUD_Menu.SetActive(false);
        HUD_Tip.SetActive(false);
    }

    void Update(){
        // 掴む処理
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)){
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(rightController.transform.position, 0.01f, Vector3.forward);
            foreach(var hit in hits){
                if(hit.collider.tag == "Card" || hit.collider.tag == "Grabbable"){
                    // Debug.Log("Grip");
                    hit.collider.transform.parent = rightController.transform;
                    SetRHold(true);
                    break;
                }
            }
        }

        // 離す処理
        if(OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)){
            for(int i = 0; i < rightController.transform.childCount; i++){
                var child = rightController.transform.GetChild(i);
                if(child.tag == "Card" || child.tag == "Grabbable"){
                    child.parent = null;
                    SetRHold(false);
                }
            }
        }

        // HUD Menu表示/非表示
        if(OVRInput.GetDown(OVRInput.RawButton.B)){
            Debug.Log("Push B Button");
            bool menuActive = HUD_Menu.activeSelf;
            bool tipActive = HUD_Tip.activeSelf;
            HUD_Menu.SetActive(!menuActive);
            HUD_Tip.SetActive(!tipActive);
        }

        // ブロックの上にカード出現
        if(OVRInput.GetDown(OVRInput.RawButton.Y)){
            Debug.Log("Push Y Button.");
            Debug.Log("Card appear");
            GameObject go = CreateCard("Door_tutorial");

            // カード出現位置 指定
            go.transform.position = new Vector3(10.0f, 1.5f, 10.0f);
        }
    }

    // カード出現
    void CardAppear(Vector3 basePos, Vector3 direction, GameObject newCard){
        // カード出現位置指定
        newCard.transform.position = basePos + direction * 0.2f;
    }

    // 特定のカード生成
    GameObject CreateCard(string name){
        string path = "Cards/Card_" + name;
        GameObject newCard = Instantiate((GameObject)Resources.Load(path));
        return newCard;
    }

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
}

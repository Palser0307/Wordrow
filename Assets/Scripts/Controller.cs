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

    // Is L/R Hand hold?
    protected bool LisHold = false;
    protected bool RisHold = false;

    void Start(){
        Deck_Object = null;
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
    }

    // カード出現
    void CardAppear(Vector3 basePos, Vector3 direction, GameObject newCard){
        // カード出現位置指定
        newCard.transform.position = basePos + direction * 0.2f;
    }

    // カード生成
    GameObject CreateCard(){
        GameObject newCard = Instantiate(RandomCard());
        // 物理演算の停止
        // newCard.GetComponent<Rigidbody>().isKinematic = true;
        return newCard;
    }

    // カード追加の度にリスト追加
    protected List<string> card_list = new List<string>{
        "Fly",
        "Rain",
        "Smoke",
    };
    // カードランダム選択
    GameObject RandomCard(){
        string path_1 = "Cards/Card_";

        int randomNum = Random.Range(0, card_list.Count);
        string path_2 = card_list[randomNum];

        string path = path_1 + path_2;

        GameObject newCard = (GameObject)Resources.Load(path);
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

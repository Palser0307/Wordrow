using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{
    [SerializeField]
    GameObject leftController;
    [SerializeField]
    GameObject rightController;
    [SerializeField]
    GameObject Deck_Prefab;

    protected GameObject Deck_Object;

    void Start(){
        Deck_Object = null;
    }

    void Update(){
        // 掴む処理
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)){
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(rightController.transform.position, 0.01f, Vector3.forward);
            foreach(var hit in hits){
                if(hit.collider.tag == "Card"){
                    // Debug.Log("Grip");
                    hit.collider.transform.parent = rightController.transform;
                    break;
                }
                /*
                if(hit.collider.tag == "Deck"){
                    Debug.Log("Grip Deck");
                    GameObject go = CreateCard();
                    go.transform.position = rightController.transform.position;
                    go.transform.parent = rightController.transform;
                    break;
                }
                */
            }
        }

        // 離す処理
        if(OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)){
            for(int i = 0; i < rightController.transform.childCount; i++){
                var child = rightController.transform.GetChild(i);
                if(child.tag == "Card"){
                    child.parent = null;
                }
            }
        }

        // カード出現処理
        // ランダムでカード出現
        if(OVRInput.GetDown(OVRInput.RawButton.B)){
            Debug.Log("Push B Button");
            // CardAppear(rightController.transform.position, rightController.transform.forward, CreateCard());
            GameObject go = CreateCard();
            go.transform.parent = rightController.transform;
            go.transform.position = rightController.transform.position;
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
        newCard.transform.position = basePos + direction * 0.5f;
    }

    // カード生成
    GameObject CreateCard(){
        GameObject newCard = Instantiate(RandomCard());
        // 物理演算の停止
        // newCard.GetComponent<Rigidbody>().isKinematic = true;
        return newCard;
    }

    protected List<string> card_list = new List<string>{
        "Fly",
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
}

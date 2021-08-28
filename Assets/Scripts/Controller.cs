using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{
    [SerializeField]
    GameObject leftController;
    [SerializeField]
    GameObject rightController;

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
        // まだブランクカード
        if(OVRInput.GetDown(OVRInput.RawButton.B)){
            Debug.Log("Push B Button");
            // CardAppear(rightController.transform.position, rightController.transform.forward, CreateCard());
            GameObject go = CreateCard();
            go.transform.parent = rightController.transform;
            go.transform.position = rightController.transform.position;
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

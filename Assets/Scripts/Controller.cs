using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour{
    [SerializeField]
    GameObject rightController;
    [SerializeField]
    GameObject leftController;

    void Update(){
        // 掴む処理
        if(OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)){
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(rightController.transform.position, 0.01f, Vector3.forward);
            foreach(var hit in hits){
                if(hit.collider.tag == "Card"){
                    hit.collider.transform.parent = rightController.transform;
                    break;
                }
            }
        }

        // 掴む処理
        if(OVRInput.GetDown(OVRInput.RawButton.B)){
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(rightController.transform.position, 0.01f, Vector3.forward);
            foreach(var hit in hits){
                if(hit.collider.tag == "Card"){
                    hit.collider.transform.parent = rightController.transform;
                    hit.collider.transform.position = rightController.transform.position;
                    hit.collider.transform.rotation = rightController.transform.rotation;
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
    }
}

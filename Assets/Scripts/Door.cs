using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ドアの開閉に関するSrc
public class Door : MonoBehaviour{
    [SerializeField]
    private GameObject Handler;

    // 把持されているかどうか
    // default:false
    private bool isHold = false;

    protected Vector3 handleStartPos;

    private void Start() {
        handleStartPos = Handler.transform.localPosition;
    }

    void Update(){
        if(Handler.transform.parent != null && !this.getIsHold()){
            setIsHold(true);
        }
        if(Handler.transform.parent == null &&  this.getIsHold()){
            setIsHold(false);
        }
        if(!this.getIsHold()){
            return;
        }
        Vector3 handlePos = Handler.transform.localPosition - handleStartPos;
        float angle = Mathf.Atan2((-1)*handlePos.z, handlePos.x) * Mathf.Rad2Deg;
        transform.Rotate(0, angle, 0);
    }

    public bool getIsHold(){
        return this.isHold;
    }
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHUD : MonoBehaviour{
    // 追従するターゲットのTransform
    // 設定されてない場合MainCameraを自動的に取得する
    [SerializeField]
    Transform target;

    // HUDの移動速度
    [SerializeField]
    float followMoveSpeed = 0.1f;

    // HUDの回転速度
    [SerializeField]
    float followRotateSpeed = 0.02f;

    // UIとTargetの角度差が一定値以上のとき，回転速度を上げる
    // そのための閾値
    [SerializeField]
    float rotateSpeedThreshold = 0.9f;

    // True: 位置のみ瞬時に同期する
    [SerializeField]
    bool isImmediateMove;

    // 各軸の回転をロックするフラグ
    // 文章用のHUDはZ軸を固定しておくとヨイカ？ヨシ！らしい
    [SerializeField]
    bool isLockXaxis;

    [SerializeField]
    bool isLockYaxis;

    [SerializeField]
    bool isLockZaxis;

    Quaternion rot;
    Quaternion rotDif;

    void Start(){
        if(!target){
            target = Camera.main.transform;
        }
    }

    void LateUpdate() {
        if(isImmediateMove){
            transform.position = target.position;
        }else{
            transform.position = Vector3.Lerp(transform.position, target.position, followMoveSpeed);
        }

        rotDif = target.rotation * Quaternion.Inverse(transform.rotation);
        rot = target.rotation;

        if(isLockXaxis){
            rot.x = 0;
        }
        if(isLockYaxis){
            rot.y = 0;
        }
        if(isLockZaxis){
            rot.z = 0;
        }

        if(rotDif.w < rotateSpeedThreshold){
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, followRotateSpeed * 4);
        }else{
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, followRotateSpeed);
        }
    }

    public void ImmediateSync(Transform targetTransform){
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour{
    [SerializeField]
    GameObject SmallFire_Prefab;
    [SerializeField]
    GameObject BigFire_Prefab;

    // ステータス一覧
    // small: 小さい火
    // big: 大きな火
    // extinguished: 火が消された状態
    // 現在のステータス
    protected static string status;
    // 次のステータス
    protected static string nextStatus;

    protected GameObject FireObject;

    // 焚火の火(Prefab)の中心の位置座標
    protected Vector3 FirePos;

    void Start(){
        // 初期ステータス代入
        status = nextStatus = "small";

        // 焚火と火のオブジェクトの相対位置
        // 火の位置調整はここを弄る
        Vector3 localPos = new Vector3(0.0f, 0.0f, 0.0f);
        // 焚火の位置座標と火の相対位置から，火の絶対座標を計算，代入する
        FirePos = this.transform.position + localPos;

        // 初期のSmallFireを出現
        FireObject = Instantiate(SmallFire_Prefab);
        FireObject.transform.position = FirePos;

    }

    void Update(){
        // 現在と次のステータスが一緒だったら以降の処理を省略
        if(status == nextStatus){
            return;
        }
        // 次のステータス(nextStatus)により大きくなったり小さくなったり，消えたり
        switch(nextStatus){
            case "small":
                // 火を小さくする処理
                Destroy(FireObject);
                FireObject = Instantiate(SmallFire_Prefab);
                FireObject.transform.position = FirePos;
                Debug.Log("Bonfire : -> small");
                break;
            case "big":
                // 火を大きくする処理
                Destroy(FireObject);
                FireObject = Instantiate(BigFire_Prefab);
                FireObject.transform.position = FirePos;
                Debug.Log("Bonfire : -> big");
                // 時間経過で火を小さくする処理
                Invoke(nameof(ToSmall), 10.0f);
                break;
            case "extinguished":
                // 火を消す処理
                Destroy(FireObject);
                break;
            default:
                // とりあえず例外処理用
                break;
        }
        // 現在のステータスを上書き
        status = nextStatus;
    }

    // 何かが接触したときに発動
    // ちな当たり判定は火ではなく焚火本体(木の部分？)
    void OnCollisionEnter(Collision other){
        GameObject otherObject = other.gameObject;
        switch(otherObject.tag){
            case "tree":
                // 木が当たったとき，火を大きくする
                nextStatus = "big";
                Destroy(other.gameObject);
                break;
            case "water":
                // 水が当たったとき，火を消す処理
                nextStatus = "extinguished";
                break;
            default:
                break;
        }
    }

    protected void ToSmall(){
        Debug.Log("Bonfire : -> 時間経過により焚火小さくなるよー");
        nextStatus = "small";
    }
}

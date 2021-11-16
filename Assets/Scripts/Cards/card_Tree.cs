using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// コンポーネントが足りない場合，自動追加
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
[RequireComponent(typeof(BoxCollider))]

// treeカード
// "tree": 右手に持って，右人差し指トリガーで使用，焚火の上に木が出現
// トリガータイプはTO(=Trigger Operation) 把持トリガー発動
// 焚火の位置探しが面倒だが，ワールドから焚火オブジェクト(名前:Bonfire?)を探して，その上にtreeを出現させる
// 焚火は1つだけの予定カッコカリ
// 焚火探索は要修正
public class card_Tree : card_class{
    [SerializeField][Header ("Tree Prefab")]
    GameObject Tree_Prefab;

    new protected void Start(){
        // 親のコンストラクタ呼出し
        base.Start();
        Debug.Log("card_tree.Start");
        setCardName("Tree");
        setTriggerType("TO");
    }

    // Update is called once per frame
    new protected void Update(){
        if(this.transform.parent != null && !this.getIsHold()){
            setIsHold(true);
        }
        if(this.transform.parent == null && this.getIsHold()){
            setIsHold(false);
        }

        // 手に持ってなかったら以降省略
        if(!this.getIsHold()){
            return;
        }
        // トリガー発動型
        if(this.getTriggerType() == "TO"){
            // 右人差し指トリガーで発動
            if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                this.use();
            }
        }
    }

    public override void use(Collision other = null){
        Debug.Log("card_tree : use()");

        /*
        // 焚火の探索
        GameObject Bonfire = GameObject.Find("Bonfire");
        // 焚火が見つからなかったら何もしない
        if(Bonfire == null){
            Debug.Log("card_tree.use() : Bonfire is not found.");
            return;
        }
        // 焚火の上0.5ぐらいに出現ポイント指定
        Vector3 AppearPos = Bonfire.transform.position + Bonfire.transform.up * 0.1f;
        */

        Vector3 AppearPos = new Vector3(12.7f, 1.8f, 3.8f);
        // 焚火の上に出現させる予定だった
        GameObject Tree_Object = Instantiate(Tree_Prefab);
        Tree_Object.transform.position = AppearPos;
        // Invoke(nameof(DelayMethod), 10.0f);
        // Destroy(Tree_Object);
        // CancelInvoke();
    }
}

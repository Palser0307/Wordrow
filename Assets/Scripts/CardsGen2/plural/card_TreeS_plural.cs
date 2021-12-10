using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_TreeS_plural : TrO_card_class{
    private float height = 0.2f;

    // 丸太を一括管理するGameObject
    [SerializeField]
    protected GameObject Trees;
    protected Trees TreeS;

    protected MeshRenderer SpellSphere;

    protected new void Start(){
        base.Start();
        setCardName("TreeS");
        SpellSphere = GameObject.Find("Spell").GetComponent<MeshRenderer>();
        Invoke(nameof(DelayMethod), 3.0f);
    }
    void DelayMethod(){
        SpellSphere.enabled = false;
    }

    // Update is called once per frame
    protected new void Update(){
        //base.Update();
        updateIsHold();
        callUse();
        rigid.velocity = Vector3.zero;
    }

    protected new void callUse(){
        if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) && getIsHold()){
            this.use();
            this.use();
            this.use();
        }
    }
    public override void use(Collision other = null){
        outputLog("use() start.");

        outputLog("Tree_Object appear.");
        Vector3 pos = this.transform.position + Vector3.up * height;
        Quaternion rot = Quaternion.Euler(90,0,0);

        // 出現位置ランダム化
        float randX = (1 - Random.Range(0.0f, 2.0f)) * 0.1f;
        float randZ = (1 - Random.Range(0.0f, 2.0f)) * 0.1f;
        pos += Vector3.forward * randX + Vector3.right * randZ;

        // 出現角度ランダム化
        float randRotX = Random.Range(0.0f, 180.0f);
        float randRotY = Random.Range(0.0f, 180.0f);
        rot = Quaternion.Euler(randRotX, randRotY, 0);

        // Effect_PrefabのインスタンスをTree_Objectに格納
        if(TreeS != null){
            TreeS.addList(Instantiate(Effect_Prefab, pos, rot));
        }else{
            Instantiate(Effect_Prefab, pos, rot);
        }
    }

    public void setTreeS(GameObject t){
        Trees = t;
        TreeS = Trees.GetComponent<Trees>();
        if(TreeS == null){
            outputLog("ERROR TreeS is NULL!!");
        }
    }
}

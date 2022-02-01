using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trees : MonoBehaviour{
    // Treeオブジェクトのリスト
    public List<GameObject> TreeS = new List<GameObject>();

    // 子オブジェクトのリスト
    private GameObject[] childObjList;

    protected GameObject forest;

    protected bool burningTrees=false;

    private void Start() {
        forest = this.gameObject;
    }

    private int count = 0;
    private void Update() {
        // Update()呼出しのn回に一回処理する
        if(count >= 10){
            // やる処理

            // childObjListの更新
            updateChildObjList();

            // どっか燃えてたら燃えてるフラグを立てる
            // どこも燃えて無かったら燃えてるフラグを折る
            bool noBurning = false;
            foreach(GameObject tree in childObjList){
                if(tree.TryGetComponent(out Tree t)){
                    if(t.Burning == true){
                        burningTrees = true;
                        noBurning = true;
                    }
                }else{
                    Debug.Log("");
                }
                /*
                Tree t = tree.GetComponent<Tree>();
                if(t.Burning == true){
                    burningTrees = true;
                    noBurning = true;
                }
                */
            }
            if(noBurning == false){
                burningTrees = false;
            }
            count = 0;
        }else{
            count++;
        }
    }

    //
    public void addList(GameObject newTree){
        newTree.transform.parent = forest.transform;
    }

    // childObjListの更新
    // 子オブジェクトの全てがTree前提
    protected void updateChildObjList(){
        int childrenCount = this.gameObject.transform.childCount;
        childObjList = new GameObject[childrenCount];

        for(int i = 0; i< childrenCount; i++){
            childObjList[i] = this.gameObject.transform.GetChild(i).gameObject;
        }
    }
}
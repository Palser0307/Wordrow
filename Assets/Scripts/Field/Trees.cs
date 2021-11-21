using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trees : MonoBehaviour{
    // Treeオブジェクトのリスト
    public List<GameObject> TreeS = new List<GameObject>();

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
            // どっか燃えてたら燃えてるフラグを立てる
            // どこも燃えて無かったら燃えてるフラグを折る
            bool noBurning = false;
            foreach(GameObject tree in TreeS){
                Tree t = tree.GetComponent<Tree>();
                if(t.Burning == true){
                    burningTrees = true;
                    noBurning = true;
                }
            }
            if(noBurning == false){
                burningTrees = false;
            }
        }else{
            count++;
        }
    }

    public void addList(GameObject newTree){
        newTree.transform.parent = forest.transform;
        TreeS.Add(newTree);
    }
}
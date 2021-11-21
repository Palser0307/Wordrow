using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 第二世代Tree及びTreesでInstantiateされたTreeオブジェクトにアタッチしておくスクリプト

public class Tree : MonoBehaviour{
    // 木は燃えているか？
    public bool Burning{get; set;} // 自動実装プロパティによる自動実装

    private void Start() {
        Burning = false;
    }
    private void Update() {
    }

    // TODO:
    //  火に触れたときに燃え移る処理を書く？
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card_Rain_gen2 : TrO_card_class{
    // 雨エフェクトのプレハブ
    [SerializeField]
    GameObject Rain_Prefab;
    // 雨の起点の高さ
    private float height = 10f;
}
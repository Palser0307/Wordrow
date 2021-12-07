using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// in "System_Scripts"

public class Field : MonoBehaviour {
    //[SerializeField]protected string storyName = null;

    // カードを呼び出す順番通りにアタッチすること
    [SerializeField]
    protected GameObject[] cards_prefab;

    protected GameObject[] cards_Object;

    private void Start() {
        cards_Object = new GameObject[cards_prefab.Length];
        return;
    }

    public void cardInstantiate(int card_num, Transform tf){
        if(card_num >= cards_prefab.Length){
            return;
        }
        if(cards_Object[card_num] != null){
            return;
        }
        cards_Object[card_num] = Instantiate(cards_prefab[card_num], tf);
    }
    public void cardInstantiate(int card_num, Vector3 pos, Quaternion rot){
        if(card_num >= cards_prefab.Length){
            return;
        }
        if(cards_Object[card_num] != null){
            return;
        }
        cards_Object[card_num] = Instantiate(cards_prefab[card_num], pos, rot);
    }

    public GameObject getCardObj(int card_num){
        if(card_num >= cards_Object.Length){
            return null;
        }
        return cards_Object[card_num];
    }

    public void destroyCardObj(int card_num){
        if(card_num >= cards_Object.Length){
            return;
        }
        Destroy(cards_Object[card_num]);
        cards_Object[card_num] = null;
    }
}
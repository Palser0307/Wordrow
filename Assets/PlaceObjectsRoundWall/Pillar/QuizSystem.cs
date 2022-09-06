using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizSystem : MonoBehaviour
{
    public int correctAnswer;
    public GameObject correctAnswerSprites;

    public bool isPlayerAnswerGenerated = false;

    // Start is called before the first frame update
    void Start()
    {

        Transform trans = this.gameObject.transform.Find("CorrectAnswer_Pillar").GetComponent<Transform>();

        Vector3 vecrot=trans.rotation.eulerAngles;

        vecrot.y=90;

        Vector3 vecpos = trans.position;

        vecpos.y = 1.5f;

        Instantiate(correctAnswerSprites,vecpos,Quaternion.Euler(vecrot),trans);

    }

    void Update(){

        //cardにより図形が出た
        if(isPlayerAnswerGenerated){
            Debug.Log("正解:" + correctAnswerSprites.name );
            //これでカードが何のカードかわかるようになったよ
            Debug.Log("カード" + this.gameObject.transform.Find("PlayerAnswer_Pillar").GetChild(0).gameObject.name);
        }

    }
}

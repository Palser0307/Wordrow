using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    //2DSprites
    public GameObject[] shapes;
    
    //Pillars
    public QuizSystem[] pillars;

    private int shapeCount=0;

    private int[] answer = new int[3];

    // Start is called before the first frame update
    void Awake()
    {
        //要素数
        shapeCount = shapes.Length;

        //問題数(Pillar数)に応じてシェイプを選択
        
        for(int i=0;i<pillars.Length;i++){
            
            answer[i]=Random.Range(0,shapeCount);
            
            //被っていないか確認
            if(i!=0){
                for(int j = i-1;j < 0;j--){
                    if(answer[j] == answer[i]){
                        //被っていたらとりあえず1を足す
                        answer[i] = (answer[i] + 1) % shapeCount;
                        //もう一回最初から検査
                        j = i-1;
                    }
                }
            }
        }

        //答えをQuizSystemに伝える
        for(int i=0;i<pillars.Length;i++){
            pillars[i].correctAnswer = answer[i];
            pillars[i].correctAnswerSprites = shapes[answer[i]];
        }
    }
}

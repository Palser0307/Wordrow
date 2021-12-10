using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string[] propositionList;
    public string Mode;
    public TextMeshPro Order;
    public Shoot Gun;

    public GameObject[] target;

    private int targetNo=0;

    private bool CLEAR=false;
    
    //指定したローカルポジションにInstantiateする
    void locateTarget(int no,Vector3 vec){

        //Instantiateを子要素として生成
        GameObject gm = Instantiate(target[no],this.gameObject.transform,false);
        gm.transform.localPosition=vec;
        gm.GetComponent<GenerateTargets>().Mode=Mode;
    }

    void processOfGame(){
            //モードの決定
        if(targetNo<propositionList.Length){
            Mode=propositionList[targetNo];
        }else{
            //あんまりよくない
            Random.InitState( System.DateTime.Now.Millisecond);
            Mode=propositionList[Random.Range(0,propositionList.Length)];
        }
        Order.text=Mode;
        Gun.TargetMode=Mode;
        
        Debug.Log("targetNo:"+targetNo);
        
        //次へ
        
        if(targetNo>=target.Length){
            CLEAR=true;
            }
            else
            {
            //召喚
            locateTarget(targetNo,new Vector3(0,0,0));
            targetNo++;
            }
    }

    void Start()
    {
        processOfGame();
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲットの個数
        //Debug.Log("targetNo:"+targetNo+" List:"+target.Length);
        if(!CLEAR){
            //ターゲットは壊されたかどうか
            //子要素があるかないかで調べる
            //Debug.Log("NOWCHILDCOUNT:"+this.transform.childCount);
            if(this.transform.childCount < 1){
                processOfGame();
            }
        }else{
            Debug.Log("RESULT"); 
            Order.text="!CLEAR!";
            Gun.TargetMode="CLEAR!!!";


            
        }
        

        //ターゲットの出力
        
    }
}

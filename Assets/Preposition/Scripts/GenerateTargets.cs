using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTargets : MonoBehaviour
{
    //生成してほしいフラグ
    //systemから受け取る
    public bool needGenerate{get;set;}=false;

    private int MAXCHILD;
    public string Mode="around";

//個数管理
    public  Dictionary<string,int> TargetCount = new Dictionary<string,int>();

    private string[] tagList={"on","under","above","below","in_front_of","behind","next_to","in","around",};

    void countAllTargetname(){
        //現在の子要素（TARGET）の数を取得
        int childCount=this.transform.childCount;
        
        //Targetのタグ数で管理
        for(int i=0;i<childCount;i++){
            foreach(string s in tagList){
                if(this.transform.GetChild(i).name==s){
                    TargetCount[s]++;
                    Debug.Log("key:"+s+"value"+TargetCount[s]);
                }
            }
        }
    }

    //ターゲットが全て壊されたかどうか
    bool targetIsDestroyed(string mode){
        //破壊すべき対象の個数
        int count = TargetCount[mode];

        //Debug.Log("mode:"+mode+"nowChildCount:"+this.transform.childCount+"nowThreshold"+MAXCHILD+","+count);
        
        //目的の対象をすべて破壊した
        if(this.transform.childCount <= MAXCHILD-count){
            return true;
        }
        
        return false;
    }

    void Awake()
    {
        //Dictionaryの初期化
        foreach(string s in tagList){
            TargetCount.Add(s,0);
        }
    }
    void Start()
    {
        

        //Targetのカウント
        countAllTargetname();
        MAXCHILD=this.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(targetIsDestroyed(Mode)){
            Destroy(this.gameObject);
        }
    }
}

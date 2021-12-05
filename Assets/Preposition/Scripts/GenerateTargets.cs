using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTargets : MonoBehaviour
{
    //生成してほしいフラグ
    //systemから受け取る
    public bool needGenerate{get;set;}
    public GameObject target;
    bool Generate(){
        //y-direction
        //On
        Instantiate(target,this.gameObject.transform,false).name="on";

        //Under
        Instantiate(target,new Vector3(0,-1,0),Quaternion.identity,this.gameObject.transform).name="under";

        //above/over
        Instantiate(target,new Vector3(0,2,0),Quaternion.identity,this.gameObject.transform).name="above";

        //below
        Instantiate(target,new Vector3(0,-2,0),Quaternion.identity,this.gameObject.transform).name="below";

        //z-direction
        //in_front_of
        Instantiate(target,new Vector3(0,0,-1),Quaternion.identity,this.gameObject.transform).name="in_front_of";

        //behind
        Instantiate(target,new Vector3(0,0,1),Quaternion.identity,this.gameObject.transform).name="behind";

        //x-direction
        //next_to
        Instantiate(target,new Vector3(-1,0,0),Quaternion.identity,this.gameObject.transform).name="next_to";

        //生成したのでfalseを返す
        return false;
    }

    void setName(string name,GameObject gm){
        gm.name=name;
    }
    void Start()
    {
        //生成
        needGenerate=Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

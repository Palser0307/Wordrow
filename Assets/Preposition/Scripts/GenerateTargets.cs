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
        locateTarget(new Vector3(0,1,0),"on");

        //Under
        locateTarget(new Vector3(0,-1,0),"under");

        //above/over
        locateTarget(new Vector3(0,2,0),"above");

        //below
        locateTarget(new Vector3(0,-2,0),"below");

        //z-direction
        //in_front_of
        locateTarget(new Vector3(0,0,-1),"in_front_of");

        //behind
        locateTarget(new Vector3(0,0,1),"behind");

        //x-direction
        //next_to
        locateTarget(new Vector3(-1,0,0),"next_to");

        //生成したのでfalseを返す
        return false;
    }

    void locateTarget(Vector3 vec,string name){

        GameObject gm = Instantiate(target,this.gameObject.transform,false);
        gm.name=name;
        gm.transform.localPosition=vec;
        gm.tag="Target";
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

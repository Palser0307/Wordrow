using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class around : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public float rad=1;

    double t=0;
           void Start(){
                


    }
    void Update()
    {
         t+=Mathf.PI*Time.deltaTime;
        //xzに回転させる
        this.transform.localPosition=new Vector3(rad*(float)Mathf.Sin((float)(t)),0,rad*(float)Mathf.Cos((float)t));
    }
}

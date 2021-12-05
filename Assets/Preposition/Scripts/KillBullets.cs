using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBullets : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider col){
        //ballはbulletについてるtag
        Debug.Log("KIll!");
        if(col.tag=="Ball"){
            
            Destroy(col.gameObject);
        }
    }
}

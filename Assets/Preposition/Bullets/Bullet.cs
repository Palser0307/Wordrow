using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 forward;
    public string Mode{get;set;}

    public ParticleSystem pc;
    // Start is called before the first frame update
    void Start()
    {
        forward = this.transform.forward.normalized;
        //生まれた瞬間に現在のゲームモードにおけるターゲットの名前を取得

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position+=forward*20*Time.deltaTime;
    }

    void OnCollisionEnter(Collision col){
        GameObject gm=col.gameObject;

        if(gm.tag=="Target"){
            //Targetにあたったら破壊
            AudioSource audio=gm.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            Debug.Log("BulletsHit:"+Mode);

            if(gm.name==Mode){
            Instantiate(pc,gm.transform.position,Quaternion.identity);
            audio=this.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            Destroy(gm);

            }
            //あたったら球はなくなる
            //Destroy(this.gameObject);
            Invoke(nameof(DestroyDelay), 0.5f);
        }
    }

    void DestroyDelay(){
        Destroy(this.gameObject);
    }
}

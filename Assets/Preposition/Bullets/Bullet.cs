using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 forward;
    public string Mode{get;set;}

    public ParticleSystem pc;

    protected ParticleSystem pc_obj;

    void Start()
    {
        forward = this.transform.forward.normalized;
        //生まれた瞬間に現在のゲームモードにおけるターゲットの名前を取得

        // ParticleSystemのLoopingをオフにする(無限消滅ループへのフェールセーフ)
        var main = this.pc.main;
        main.loop = false;
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
                this.pc_obj = Instantiate(pc,gm.transform.position,Quaternion.identity);
                Invoke(nameof(DestroyParticle), 2.0f);
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
    void DestroyParticle(){
        Destroy(this.pc_obj);
    }
}

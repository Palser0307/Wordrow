using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet;
    private bool isHold = false;

    OVRGrabbable grab;

    //現在のターゲットを教えてもらう
    public string TargetMode;

    //flash
    public GameObject flash;

    // Update is called once per frame
    
    void Start(){
        grab=this.GetComponent<OVRGrabbable>();
    }
    void Update()
    {
        //持たれた
        if(grab.isGrabbed){
            //triggerがひかれた
            if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)){
                //うったエフェクト
                this.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                
                //その方向へ球が出てほしい
                //手

                /*miss script
                */
                GameObject grabber = grab.grabbedBy.gameObject;
                
                Transform  trans = grabber.transform.parent;
                Debug.Log(trans.name);
                Vector3 pos = trans.position;
                Vector3 dir = trans.rotation.eulerAngles;

                //球の生成
                //ここでTEXT変えてもよい
                bullet.GetComponentInChildren<TextMeshPro>().text=TargetMode;
                

                //Modeの変更
                GameObject gm =Instantiate(bullet,pos,Quaternion.Euler(dir));
                gm.GetComponent<Bullet>().Mode =TargetMode;

                
                AudioSource audio=this.GetComponent<AudioSource>();
                audio.PlayOneShot(audio.clip);

                StartCoroutine(LetsVibration(0.1f,OVRInput.Controller.RTouch));
                
                
                

            }
        }
    }
    private IEnumerator LetsVibration(float waitTime,OVRInput.Controller con)
    {
            OVRInput.SetControllerVibration(0.3f, 0.5f, con);
            yield return new WaitForSeconds(waitTime);
            OVRInput.SetControllerVibration(0f, 0f, con);
        
    }


}

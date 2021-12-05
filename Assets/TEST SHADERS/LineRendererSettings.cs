using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    public GameModeManager GM;
    public TextMeshProUGUI GameModeDisplay;

    Vector3[] points;
    public Button btn;

    //audio用Flag
    private string btnaudio;

    private AudioSource AS;
    void Start()
    {
        //Line Renderer持ってるのは分かっているので，
        rend = gameObject.GetComponent<LineRenderer>();

        points = new Vector3[2];

        points[0]= Vector3.zero;

        points[1]= transform.position+new Vector3(0,0,20);

        rend.SetPositions(points);
        rend.enabled = true;

        AS=this.GetComponent<AudioSource>();


    }

    public LayerMask layerMask;

    //align 提携させる
    //ボタンは保持しておいて，trueとfalseで判断する
    public bool AlignLineRenderer(LineRenderer rend){
        Ray ray;
        ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;
        bool hitBtn=false;

        //レイキャストが何かにあたった
        if(Physics.Raycast(ray, out hit, layerMask))
        {
            
            //Button
            btn = hit.collider.gameObject.GetComponent<Button>();
            
            //基本はfalse
            hitBtn = false;

            //あたったのがbtnならtrue
            if(btn != null)
            {
            Debug.Log(btn.name);
            //レイの距離をその位置に合わせる
            points[1]=transform.forward+new Vector3(0,0,hit.distance);
            //色を変えてみる
            rend.startColor = Color.red;
            rend.endColor = Color.red;

            //hover audio
            if(btnaudio != btn.name){
                AS.PlayOneShot(AS.clip);
                btnaudio=btn.name;
            }

                hitBtn=true;
            }else{
                hitBtn=false;
                //レイは20m
                points[1]=transform.forward + new Vector3(0,0,20);
                //基本色は青
                rend.startColor=Color.blue;
                rend.endColor=Color.blue;
            }

        rend.SetPositions(points);
        rend.material.color=rend.startColor;


        }
        else
        {
            hitBtn=false;
            //レイは20m
            points[1]=transform.forward + new Vector3(0,0,20);
            //基本色は青
            rend.startColor=Color.blue;
            rend.endColor=Color.blue;
        }

        rend.SetPositions(points);
        rend.material.color=rend.startColor;
        return hitBtn;

    }

    // Update is called once per frame
    void Update()
    {
        if(AlignLineRenderer(rend) && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)){
            btn.onClick.Invoke();
        }
    }



    public void ButtonOnClick(){
        if(btn!= null){
            //現在のボタン
            GM.GAMEMODE=btn.name;
            Debug.Log("NOW Bottun"+btn.name);
            AudioSource audio=btn.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
            
            //displayの更新
            GameModeDisplay.text=btn.name;
        }
    }
    public void StartOnClick(){
        if(btn!= null){
            //現在のボタン
            GM.START=true;
            Debug.Log("NOW Bottun"+btn.name);
            AudioSource audio=btn.GetComponent<AudioSource>();
            audio.PlayOneShot(audio.clip);
        }
    }
}

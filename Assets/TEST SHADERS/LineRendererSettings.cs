using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineRendererSettings : MonoBehaviour
{
    [SerializeField] LineRenderer rend;

    Vector3[] points;

    public Image img;
    public GameObject panel;
    public Button btn;
    void Start()
    {
        //Line Renderer持ってるのは分かっているので，
        rend = gameObject.GetComponent<LineRenderer>();

        points = new Vector3[2];

        points[0]= Vector3.zero;

        points[1]= transform.position+new Vector3(0,0,20);

        rend.SetPositions(points);
        rend.enabled = true;

        img = panel.GetComponent<Image>();

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
            //レイの距離をその位置に合わせる
            points[1]=transform.forward+new Vector3(0,0,hit.distance);
            //色を変えてみる
            rend.startColor = Color.red;
            rend.endColor = Color.red;

            //Button
            btn = hit.collider.gameObject.GetComponent<Button>();
            
            //基本はfalse
            hitBtn = false;

            //あたったのがbtnならtrue
            if(btn != null)
            {
                hitBtn=true;
            }


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
        if(AlignLineRenderer(rend) && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.9){
            btn.onClick.Invoke();
        }
    }



    public void ColorChangeOnClick(){
        if(btn!= null){
            if(btn.name=="Return"){
                img.color=Color.red;
            }
            if(btn.name=="Exit"){
                img.color=Color.blue;
            }
        }
    }
}

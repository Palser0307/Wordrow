using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //Game Flag
    public string GAMEMODE;
    
    //GAME START SWITCH
    public bool START=false;





    void Start()
    {
        START = false;
        GAMEMODE = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(START){
            Debug.Log("START!");
            if(GAMEMODE == "PREPOSITION_1"){
                //PREPOSITION_1の発動
                Debug.Log("p1");
            }else if(GAMEMODE == "PREPOSITION_2"){
                //PREPOSITION_2の発動
                Debug.Log("p2");
            }

            //あとしょり　
            START=false;
        }
    }
}

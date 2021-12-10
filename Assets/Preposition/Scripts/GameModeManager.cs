using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameModeManager : MonoBehaviour
{
    // Start is called before the first frame update

    //Game Flag
    public string GAMEMODE;
    
    //GAME START SWITCH
    public bool START=false;

//初回はNULL
    private string nowStage=null;
    
    void Start()
    {
        START = false;
        GAMEMODE = null;
    }

    // Update is called once per frame
    void Update()
    {

        if(START){
            //前回読み込んだシーンを消す
            if(nowStage!=null){
                SceneManager.UnloadSceneAsync(nowStage);
            }

            nowStage="Scenes/PREPOSITION_STAGE/"+GAMEMODE;

            Debug.Log("START!");
            if(GAMEMODE == "PREPOSITION_1"){
                SceneManager.LoadScene(nowStage,LoadSceneMode.Additive);
            }else if(GAMEMODE == "PREPOSITION_2"){
                SceneManager.LoadScene(nowStage,LoadSceneMode.Additive);
            }else if(GAMEMODE == "BASE_ROOM"){
                SceneManager.LoadScene("Scenes/ScenarioPart/BaseRoom");
            }

            //あとしょり　
            START=false;
        }
    }
}

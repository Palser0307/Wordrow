using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour{
    // GameFlag
    public string GAMEMODE = "";

    // GAMESTART Flag
    public bool START = false;

    // init : null
    private string nowStage = null;

    // 使えるストーリー一蘭
    protected List<string> storyList = new List<string>();

    void Start(){
        START = false;
        GAMEMODE = null;
        addStoryList("Classroom(SiS)");
    }

    // Update is called once per frame
    void Update(){
        if(START == true){
            // 前回読み込んだシーンを消す
            if(nowStage != null){
                SceneManager.UnloadSceneAsync(nowStage);
            }

            // make address
            if(GAMEMODE == "Classroom"){
                GAMEMODE = "Classroom(SiS)";
            }
            if(haveStoryList(GAMEMODE) == true){
                nowStage = "Scenes/ScenarioPart/" + GAMEMODE;

                SceneManager.LoadScene(nowStage,LoadSceneMode.Additive);

                Debug.Log("StageManager: StageChange start");
            }

            // 後処理
            START = false;
        }
    }


/*
--------------------------------------------------
    他関数群
--------------------------------------------------
*/
    protected void addStoryList(string newStory){
        if(haveStoryList(newStory) == false){
            storyList.Add(newStory);
        }
    }

    protected bool haveStoryList(string story){
        return storyList.Contains(story);
    }

}

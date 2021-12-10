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
        addStoryList("ClassRoom(SiS)");
        addStoryList("Tutorial(SiS)");
    }

    // Update is called once per frame
    void Update(){
        if(START == true){
            // 前回読み込んだシーンを消す
            if(nowStage != null){
                SceneManager.UnloadSceneAsync(nowStage);
            }

            /*
            //if(haveStoryList(GAMEMODE) == true){
            if(GAMEMODE ==)
                nowStage = "Scenes/ScenarioPart/" + GAMEMODE;

                SceneManager.LoadScene(nowStage,LoadSceneMode.Additive);

                Debug.Log("StageManager: StageChange start");
            }else if(GAMEMODE == "PREPOSITION"){
                nowStage = "Scenes/" + GAMEMODE;
                SceneManager.LoadScene(nowStage);
                Debug.Log("StageManager: change to PREPOSITION MODE");
            }*/
            Debug.Log("SM: GAMEMODE -> "+GAMEMODE);
            switch (GAMEMODE){
                // Classroomへの遷移
                case "ClassRoom":
                    Debug.Log("ClassRoom");
                    nowStage = "Scenes/ScenarioPart/" + GAMEMODE + "(SiS)";
                    SceneManager.LoadScene(nowStage,LoadSceneMode.Additive);
                    break;
                case "ClassRoom(SiS)":
                    Debug.Log("ClassRoomSiS");
                    nowStage = "Scenes/ScenarioPart/" + GAMEMODE;
                    SceneManager.LoadScene(nowStage,LoadSceneMode.Additive);
                    break;

                // Tutorialへの遷移
                case "Tutorial":
                    Debug.Log("Tutorial");
                    nowStage = "Scenes/ScenarioPart/" + GAMEMODE + "(SiS)";
                    SceneManager.LoadScene(nowStage);
                    break;
                case "Tutorial(Sis)":
                    Debug.Log("TutorialSiS");
                    nowStage = "Scenes/ScenarioPart/" + GAMEMODE;
                    SceneManager.LoadScene(nowStage);
                    break;

                // PREPOSITIONへの遷移
                case "PREPOSITION":
                    Debug.Log("PREPOSITION");
                    nowStage = "Scenes/" + GAMEMODE;
                    SceneManager.LoadScene(nowStage);
                    break;
                default:
                    break;
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

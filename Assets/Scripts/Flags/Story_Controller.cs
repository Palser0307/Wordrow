using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// in "System_Scripts" Object
// インスペクタで設定されたステージのフラグ管理を行う…予定

public class Story_Controller : MonoBehaviour {
    // ステージ名をインスペクタからロード
    [SerializeField]
    protected string storyName = null;

    // ストーリーの進行度
    protected int storyCount = 0;

    // 使えるストーリー一覧
    protected List<string> storyList = new List<string>();

    // フラグ管理のインスタンス格納
    public FlagManager fm;

    // あーちゃんのセリフ
    protected string[] a_words;

    // Alpha_Controllerへのアクセサ
    protected Alpha_Controller alpha_ctrl = null;

    // Task_Display_Controllerへのアクセサ
    protected Task_Display_Controller task_disp_ctrl = null;

    private void Start() {
        addStoryList("Tutorial");
        addStoryList("Tutorial2");
        addStoryList("AlyxTest");
        setAlphaCtrl();
        setTaskDispCtrl();

        if(haveStoryList(storyName) == true){
            switch(storyName){
                case "Tutorial":
                    setup_Tutorial();
                    break;
                case "Tutorial2":
                    setup_Tutorial2();
                    break;
                case "AlyxTest":
                    setup_AlyxTest();
                    break;

                default:
                    break;
            }
        }else{
            outputError("\""+storyName+"\" is not Story Name!");
            storyName = null;
        }
    }

    protected void Update(){
        if(storyCount == alpha_ctrl.strPos){
            return;
        }
        storyCount = alpha_ctrl.strPos;

        switch(storyName){
            case "Tutorial":
                update_Tutorial();
                break;
            case "Tutorial2":
                update_Tutorial2();
                break;
            case "AlyxTest":
                update_AlyxTest();
                break;

            default:
                break;
        }
    }


/*
--------------------------------------------------
    Tutorial
--------------------------------------------------
*/
    protected void setup_Tutorial(){
        this.fm = new FlagManager("Tutorial");
        this.a_words = new string[]{
            "それでは，チュートリアルを始めましょう．",
            "あたりを見渡してみてください．",
            "目の前に箱が見えますか？その箱の位置まで歩いてみてください．",
            "Lコントローラのスティックで移動ができますよ．", // movePoint1(def:false)
            "そのまま目の前のSMOKEカードをつかんでみてください．",
            "Rスティックのハンドトリガーでカードをつかめますよ．", // grabSmoke(def:false)
            "カードがつかめましたね．",
            "Rコントローラのインデックストリガーを押してからカードを投げてみてください．", // useSmoke(def:false)
            "煙が出てきましたね．",
            "どうやらSMOKEカードを使うと煙が出るようです．",
            "次はRAINのカードを使ってみましょう．",
            "箱の上のRAINカードをつかんでみてください．", // grabRain(def:false)
            "そのままRコントローラのインデックストリガーを押してみてください．", // useRain(def:false)
            "雨が降ってきましたね．",
            "RAINカードは雨を発生させるようです．",
            "それでは次が最後のカードです．",
            "箱の上のFLYカードをつかんでみてください．", // grabFly(def:false)
            "そのカードを使ってこの部屋から出てみましょう．",
            "前方の黒い球体にFLYカードを使ってください．", // useFly(def:false)
            "無事に扉が出現しましたね．",
            "その扉に入ればここから出ることができますよ．",
            "それでは，以上でチュートリアルは終了です．",
            "Have a nice trip, Master.", // scenarioClear(def:false)
        };
        // フラグ設定
        this.fm.initFlag("arrivePoint1", false);
        this.fm.initFlag("grabSmoke", false);
        this.fm.initFlag("useSmoke", false);
        this.fm.initFlag("grabRain", false);
        this.fm.initFlag("useRain", false);
        this.fm.initFlag("grabFly", false);
        this.fm.initFlag("useFly", false);
        this.fm.initFlag("scenarioClear", false);

        // セリフ上書き
        this.alpha_ctrl.words = a_words;

        // Alpha_ctrlのStart()終わり待ち
        Invoke(nameof(delaymethod), 1.0f);
    }

    protected void update_Tutorial(){
        switch(storyCount){
            case 3:
                if((bool)fm.getFlag("arrivePoint1") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 5:
                if((bool)fm.getFlag("grabSmoke") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 7:
                if((bool)fm.getFlag("useSmoke") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 11:
                if((bool)fm.getFlag("grabRain") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 12:
                if((bool)fm.getFlag("useRain") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 16:
                if((bool)fm.getFlag("grabFly") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 18:
                if((bool)fm.getFlag("useFly") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;
            case 22:
                if((bool)fm.getFlag("scenarioClear") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                }
                break;

            default:
                break;
        }
    }

/*
--------------------------------------------------
    Tutorial2
--------------------------------------------------
*/
    protected void setup_Tutorial2(){
        this.fm = new FlagManager("Tutorial2");
        this.a_words = new string[]{
            "マスター，手に入れたカードはαに使うこともできるんです．",
            "実際に使ってみましょう．",
            "前のパネルを見てください．",
            "空いている部分にTREEカードを入れてみてください．",
            "Alpha is TREE.", // isAlphaTree?(def:false)
            "マスターがTREEに変化しました．",
            "αにカードを使っていただくと，マスターを変身させられるわけですね．",
            "今回はαに使っていただきましたが，",
            "マップ内のオブジェクトに使うと，オブジェクトを変身させることができます．",
            "この機能が必要な時は，ぜひαにご命令ください．",
            "それでは，前方に見える赤い柱に向かいましょう．",
            "そこに向かえば次のマップに移動できますよ．",
            "また御用の際はいつでもお呼び出し下さい．",
            "Thank you,Master.", // scenarioClear?(def:false)
        };
        this.fm.initFlag("isAlphaTree", false);
        this.fm.initFlag("scenarioClear", false);
        this.alpha_ctrl.words = a_words; // セリフ上書き
        //this.alpha_ctrl.resetStr();

        // Alpha_ctrlのStart()終わり待ち
        // Invokeはその実行を待たずに次に進めてくれるから…
        Invoke(nameof(delaymethod),1.0f);
        //alpha_ctrl.reloadStr();
    }

    protected void update_Tutorial2(){
        switch (storyCount){
            case 4: // alpha: "Alpha is TREE."
                if((bool)fm.getFlag("isAlphaTree") == false){
                    alpha_ctrl.Status = "inactive";
                }else{
                    alpha_ctrl.Status = "active";
                    alpha_ctrl.nextStr();
                }
                break;

            case 13: // alpha: "Thank you, Master."
                if((bool)fm.getFlag("scenarioClear") == false){
                    alpha_ctrl.Status = "inactive";
                }
                break;

            default:
                break;
        }
    }

/*
--------------------------------------------------
    AlyxTest
--------------------------------------------------
*/

    protected void setup_AlyxTest(){
        this.fm = new FlagManager("AlyxTest");
        this.a_words = new string[]{
            "セリフ1",
            "タスク説明1",
            "セリフ2",// 2: task1Clear :false
            "セリフ3：task1 Clear",
        };
        // Flag設定
        this.fm.initFlag("task1Clear", false);

        // セリフ上書き
        this.alpha_ctrl.words = a_words;

        Invoke(nameof(delaymethod), 1.0f);
    }

    protected void update_AlyxTest(){
        switch (storyCount){
            case 2:
                if((bool)fm.getFlag("task1Clear")==false){
                    alpha_ctrl.Status = "inactive";
                    task_disp_ctrl.setActive(true);
                    task_disp_ctrl.replaceStr("タスク1", true);
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                    task_disp_ctrl.setActive(false);
                }
                break;
            default:
                break;
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

    protected void setAlphaCtrl(){
        alpha_ctrl = this.gameObject.GetComponent<Alpha_Controller>();
        if(alpha_ctrl == null){
            outputError("alpha is not found");
        }
    }

    protected void setTaskDispCtrl(){
        if(this.gameObject.TryGetComponent(out task_disp_ctrl) == false){
            outputError("TaskDispCtrl is not found");
        }
    }

    protected void outputLog(string str){
        Debug.Log("Story_ctrl : "+str);
    }
    protected void outputError(string str){
        Debug.LogError("Story_ctrl : "+str);
    }
    protected void delaymethod(){
        this.alpha_ctrl.resetStr();
    }

}
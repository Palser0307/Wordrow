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

    private void Start() {
        addStoryList("Tutorial");
        addStoryList("Tutorial2");
        setAlphaCtrl();

        if(haveStoryList(storyName) == true){
            switch(storyName){
                case "Tutorial2":
                    setup_Tutorial2();
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
        storyCount = alpha_ctrl.strPos;

        switch(storyName){
            case "Tutorial2":
                update_Tutorial2();
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
    protected void setup_Tutorial(){
        fm = new FlagManager("Tutorial");

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
            "Thank you,Master.", // portal2end?(def:false)
        };
        this.fm.initFlag("isAlphaTree", false);
        this.fm.initFlag("scenarioClear", false);
        this.alpha_ctrl.words = a_words; // セリフ上書き
        //this.alpha_ctrl.resetStr();

        // Alpha_ctrlのStart()終わり待ち
        // Invokeはその実行を待たずに次に勧めてくれるから…
        Invoke(nameof(delaymethod),1.0f);
        //alpha_ctrl.reloadStr();
    }
    protected void delaymethod(){
        this.alpha_ctrl.resetStr();
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

    protected void outputLog(string str){
        Debug.Log("Story_ctrl : "+str);
    }
    protected void outputError(string str){
        Debug.LogError("Story_ctrl : "+str);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //
    protected Field field = null;

    // Prefab管理用
    public GameObject[] Prefab = null;

    // 一回限り実行用
    protected bool once = false;

    private void Start() {
        outputLog("Start");
        addStoryList("Tutorial");
        addStoryList("Tutorial2");
        addStoryList("AlyxTest");
        addStoryList("Object1");
        addStoryList("Classroom");

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
                case "Object1":
                    setup_Object1();
                    break;
                case "Classroom":
                    setup_Classroom();
                    break;

                default:
                    this.fm = new FlagManager("");
                    break;
            }
        }else{
            outputError("\""+storyName+"\" is not Story Name!");
            storyName = null;
            this.fm = new FlagManager("");
        }

        //this.field = this.gameObject.GetComponent<Field>();
        if(!this.gameObject.TryGetComponent(out this.field)){
            outputError("field is not found.");
        }
        outputLog("Start finish");
    }

    protected void Update(){
        /*
        if(storyCount == alpha_ctrl.strPos){
            return;
        }
        */
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
            case "Object1":
                update_Object1();
                break;
            case "Classroom":
                update_Classroom();
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
            "それでは，チュートリアルを始めましょう．\n左コントローラの人差し指トリガーで次に進めます",
            "あたりを見渡してみてください．",
            "目の前に箱が見えますか？その箱の位置まで歩いてみてください．",
            "左コントローラのスティックで移動ができますよ．", // movePoint1(def:false)
            "そのまま目の前のSMOKEカードをつかんでみてください．",
            "右コントローラの中指トリガーでカードをつかめますよ．", // grabSmoke(def:false)
            "カードがつかめましたね．",
            "右コントローラの人差し指トリガーを押して，黄色くなったらカードを投げてみてください．", // useSmoke(def:false)
            "煙が出てきましたね．",
            "どうやらSMOKEカードを使うと煙が出るようです．",
            "次はRAINのカードを使ってみましょう．",
            "箱の上のRAINカードをつかんでみてください．", // grabRain(def:false)
            "そのまま右コントローラの人差し指トリガーを押してみてください．", // useRain(def:false)
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
        this.fm.initFlag("arrivePoint2", false);

        // セリフ上書き
        this.alpha_ctrl.words = a_words;

        // Alpha_ctrlのStart()終わり待ち
        Invoke(nameof(delaymethod), 1.0f);
    }

    protected void update_Tutorial(){
        // ゴールについた判定
        if((bool)this.fm.getFlag("arrivePoint2") == true){
            this.alpha_ctrl.outputStr("次のワールドを読み込み中です");
            // 遷移処理
            string nextStage = "Scenes/ScenarioPart/" + "BaseRoom";
            SceneManager.LoadScene(nextStage);
        }
        switch(storyCount){
            case 3:
                if((bool)fm.getFlag("arrivePoint1") == false){
                    alpha_ctrl.Status = "inactive";
                    this.task_disp_ctrl.setActive(true);
                    this.task_disp_ctrl.replaceStr("前へ進む", true);
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                    this.task_disp_ctrl.setActive(false);
                }
                break;
            case 5:
                if((bool)fm.getFlag("grabSmoke") == false){
                    alpha_ctrl.Status = "inactive";
                    this.task_disp_ctrl.setActive(true);
                    this.task_disp_ctrl.replaceStr("Smokeを掴む", true);
                }else{
                    alpha_ctrl.nextStr();
                    alpha_ctrl.Status = "active";
                    this.task_disp_ctrl.setActive(false);
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
                if((bool)this.fm.getFlag("isAlphaTree") == false){
                    //outputLog("260: isAlphaTree : "+(bool)this.fm.getFlag("isAlphaTree"));
                    alpha_ctrl.Status = "inactive";
                }else{
                    //outputLog("263: isAlphaTree : "+(bool)this.fm.getFlag("isAlphaTree"));
                    alpha_ctrl.Status = "active";
                    alpha_ctrl.nextStr();
                }
                break;

            case 13: // alpha: "Thank you, Master."
                if((bool)this.fm.getFlag("scenarioClear") == false){
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
    Object1
--------------------------------------------------
*/
    protected void setup_Object1(){
        this.fm = new FlagManager("Object1");
        this.a_words = new string[]{
            "おや，お困りですか，マスター？",
            "目の前の大きな木が邪魔でポータルに入ることができませんね．",
            "近くに使えそうなカードがありませんか？", // grabFire
            "FIREのカードですか．使ってみましょう．", // burningTree true
            "おや，これは・・・",
            "どうやらFIREのカードはオブジェクトを燃やしてしまうようです．",
            "木が燃えてしまって危険ですね．",
            "火を消せるようなカードはありませんか？", // grabWater
            "WATERのカードですね．使ってみてください．",
            "水が出て・・・火も消えたようですね．", // burningTree false
            "それではもう一度使えそうなカードを探してみましょう．", // grabCut
            "CUTのカードですか．",
            "これならよさそうですね．",
            "使ってみましょう．", // useCut
            "・・・邪魔な木がなくなりましたね．",
            "CUTのカードはオブジェクトを切ることができるようです．",
            "それでは，次のステージへと進みましょう．",
            "Thank you,Master.", // scenarioClear
        };
        // フラグ管理
        this.fm.initFlag("grabTree", false);
        this.fm.initFlag("grabFire", false);
        this.fm.initFlag("burningTree", false);
        this.fm.initFlag("grabWater", false);
        this.fm.initFlag("grabCut", false);
        this.fm.initFlag("useCut", false);
        this.fm.initFlag("scenarioClear", false);

        // セリフ上書き
        this.alpha_ctrl.words = a_words;

        // Alpha_ctrlのStart()終わり待ち
        Invoke(nameof(delaymethod), 1.0f);
        outputLog("Object1 Start finish.");

    }

    protected void update_Object1(){
        switch (storyCount){
            case 2:
                if((bool)this.fm.getFlag("grabTree")==false){
                    this.field.cardInstantiate(0, new Vector3(0,1.6f,0), new Quaternion(0,0,0,0));
                    this.alpha_ctrl.Status = "inactive";
                    this.task_disp_ctrl.setActive(true);
                    this.task_disp_ctrl.replaceStr("カードを掴む", true);
                }else{
                    outputLog("grabed TreeCard!");
                    this.alpha_ctrl.nextStr();
                    this.alpha_ctrl.Status = "active";
                    this.task_disp_ctrl.setActive(false);
                }
                break;
            default:
                break;
        }
    }

/*
--------------------------------------------------
    Classroom
--------------------------------------------------
*/
    protected void setup_Classroom(){
        this.fm = new FlagManager("Classroom");
        this.a_words = new string[]{
            //ステージに到着
            //机に日誌が出現
            //黒板に落書き(絵)が出現
            "新しいステージですね．",
            "・・・え，マスターはこのステージに見覚えがあるのですか？",
            "既視感というものでしょうか．不思議なこともあるものですね．",
            "さて，このステージにはどんなタスクがあるのでしょうか？",
            "・・・おや，机の上に何かありますね．",
            "マスター，読んでみましょうか．",
            "読むために使えそうなカードが近くにありませんか？",
            //write,close,readカードが出現
            "・・・え？カードが3つもある？",
            "読むためのカードは1つのはずですが・・・．",
            "どのカードを使えばいいか，マスターはわかりますか？",

            //タスク1：日誌を読む(タスクの表示)
            //readカードを使用した場合：正解
                //タスクメッセージが表示される
                "黒板の落書きはちゃんと消しましょう",
                "黒板の落書き・・・",
                "黒板に描かれている絵のことでしょうか．",
            //write，closeカードを使用した場合：不正解
                //何も起きない：タスクメッセージは表示されない
                //"どうやら使うカードを間違えたようです．",

            //タスク2：黒板の落書きを消す
            //walk,jump,cleanカードが出現
            "それでは黒板の落書きを消してみましょう．",
            "またしても使えそうなカードが3つほどありますね．",
            "机の上の3つのカードから使うカードを選んでください．",
            //cleanカードを使用した場合：正解
                //黒板の絵が消えて元の黒板オブジェクトに戻る
                "黒板がきれいになりましたね．",
                "お見事です，マスター．",
            //walk,jumpカードを使用した場合：不正解
                //何も起きない：黒板の絵はそのまま
                //"どうやら使うカードを間違えたようです．",
            //タスクをすべてクリア
            "このステージでのタスクはすべて完了したようです．",
            "Thank you,Master.",
        };
        // フラグ設定
        this.fm.initFlag("readBook", 0);
        this.fm.initFlag("clearBoard", 0);
        this.fm.initFlag("ScenarioClear", false);

        // セリフ上書き
        this.alpha_ctrl.words = a_words;

        // カードをまとめてるGameObjectを非表示に
        Prefab[0].SetActive(false);
        Prefab[1].SetActive(false);

        //GameObject.Find("blackboard01_1").GetComponent<blackboard>().Start();

        // タスク表示を非表示
        this.task_disp_ctrl.setActive(false);

        this.alpha_ctrl.reloadStr();

        // Alpha_ctrlのStart()終わり待ち
        Invoke(nameof(delaymethod), 1.0f);
        outputLog("Classroom Start finish.");
    }

    protected void update_Classroom(){
        switch (storyCount){
            case 0:
                this.task_disp_ctrl.setActive(false);
                this.alpha_ctrl.reloadStr();
                break;
            case 6:// write,close,readカードが出現
                if(once==false){
                    once=true;
                    // No1を有効化
                    Prefab[0].SetActive(true);
                }
                break;
            case 9:// 本を読む
                this.alpha_ctrl.Status = "inactive";
                this.task_disp_ctrl.setActive(true);
                this.task_disp_ctrl.replaceStr("タスク\n日誌を読む", true);
                //outputLog("case 9: \"readBook\" -> "+(int)this.fm.getFlag("readBook"));
                switch((int)this.fm.getFlag("readBook")){
                    case 0:// なし
                        this.alpha_ctrl.Status = "inactive";
                        break;
                    case 1:// 正解:
                        this.alpha_ctrl.Status = "next";
                        this.task_disp_ctrl.replaceStr("「黒板の落書きはちゃんと消しましょう」", true);
                        break;
                    case 2:// 不正解
                        this.alpha_ctrl.Status = "inactive";
                        this.alpha_ctrl.outputStr("どうやら使うカードを間違えたようです");
                        break;
                }
                once = false;
                break;
            case 12:// walk, jump, cleanのカードが出現
                if(once==false){
                    once=true;
                    // No1の子オブジェクト(全部InstantCard)を強制リリース
                    InstantCard[] ics = Prefab[0].gameObject.GetComponentsInChildren<InstantCard>();
                    foreach(InstantCard ic in ics){
                        ic.forceRelease();
                    }
                    // No1を無効化
                    Prefab[0].SetActive(false);
                    // No2を有効化
                    Prefab[1].SetActive(true);
                }
                break;
            case 15:// 黒板
                once = false;
                this.alpha_ctrl.Status = "inactive";
                this.task_disp_ctrl.setActive(true);
                this.task_disp_ctrl.replaceStr("タスク\n黒板の落書きを消す", true);
                switch((int)this.fm.getFlag("clearBoard")){
                    case 0:// なし
                        this.alpha_ctrl.Status = "inactive";
                        break;
                    case 1:// 正解:黒板の落書きが消える
                        this.alpha_ctrl.Status = "next";
                        this.task_disp_ctrl.setActive(false);
                        Prefab[2].SetActive(false);
                        break;
                    case 2:// 不正解
                        this.alpha_ctrl.Status = "inactive";
                        this.alpha_ctrl.outputStr("どうやら使うカードを間違えたようです");
                        this.task_disp_ctrl.replaceStr("タスク\n黒板をきれいにしよう", true);
                        break;
                    default:
                        break;
                }
                break;
            case 16:// walk, jump, cleanのカードが出現
                if(once==false){
                    once=true;
                    // No2の子オブジェクト(全部InstantCard)を強制リリース
                    InstantCard[] ics = Prefab[1].gameObject.GetComponentsInChildren<InstantCard>();
                    foreach(InstantCard ic in ics){
                        ic.forceRelease();
                    }
                    // No2を有効化
                    Prefab[1].SetActive(false);
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
        if(!this.gameObject.TryGetComponent(out this.alpha_ctrl)){
            outputError("alpha_ctrl is not found.");
        }else{
            outputLog("alpha_ctrl is found.");
        }
    }

    protected void setTaskDispCtrl(){
        if(!this.gameObject.TryGetComponent(out this.task_disp_ctrl)){
            outputError("TaskDispCtrl is not found.");
        }else{
            outputLog("TaskDispCtrl is found.");
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
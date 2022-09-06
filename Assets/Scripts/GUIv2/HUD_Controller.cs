using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TextMeshProを使うためのnamespaceをload
using TMPro;

// FollowHUD用ドライバのようなもの
// このスクリプトからHUDを叩ける
// むしろこれ以外から叩くのは非常に面倒ってことで

/*
機能1：TxtHUDに文字列と表示時間を渡す
機能2：TxtHUDの表示/非表示の操作
*/
public class HUD_Controller : MonoBehaviour{
    // HUDのフェードの状態
    protected enum FadeStatus{
        None, FadeIn, FadeOut,
    }

    // フェードの時間
    [SerializeField]
    protected float fadeTime = 1.5f;

    // HUDのTMProアクセサ
    // セリフ用
    protected TextMeshProUGUI Lines_TMProUGUI_Component = null;
    protected GameObject Lines_GameObject = null;
    protected FadeStatus Lines_FadeStatus = FadeStatus.None;
    protected CanvasGroup Lines_CanvasGroup_Component = null;

    // ScenarioLineのキュー(FIFOのアレ)
    [SerializeField]
    protected Queue<ScenarioLine> scenarioLines_queue = new Queue<ScenarioLine>();

    void Start(){
        // とりあえず
        GameObject txt_hud_obj = this.gameObject.transform.FindChildRecursive("Text_HUD").gameObject;
        //Debug.Log(txt_hud_obj.name);
        Lines_GameObject = txt_hud_obj.transform.FindChildRecursive("Lines").gameObject;
        //Debug.Log(Lines_GameObject.name);
        if(Lines_GameObject.TryGetComponent(out this.Lines_TMProUGUI_Component) == false){
            Debug.LogError("ERROR!!!!!!!!!!!!!!!!!");
            return;
        }
        if(Lines_GameObject.TryGetComponent(out this.Lines_CanvasGroup_Component) == false){
            Debug.LogError("ERROR!!!!!!!!!!!!!!!!!");
            return;
        }

        // とりあえずテスト
        EnqueueLine(new ScenarioLine("起動テスト", 5f));
        outputNextLine();
    }

    void Update(){
        // フェード用のalpha操作部分
        if(Lines_FadeStatus != FadeStatus.None){
            float deltaAlpha = fadeTime * Time.deltaTime;
            switch(Lines_FadeStatus){
                case FadeStatus.FadeIn:
                    Lines_CanvasGroup_Component.alpha += deltaAlpha;
                    break;
                case FadeStatus.FadeOut:
                    Lines_CanvasGroup_Component.alpha -= deltaAlpha;
                    break;
                default:
                    break;
            }
            if(Lines_CanvasGroup_Component.alpha <= 0 || 1 <= Lines_CanvasGroup_Component.alpha){
                Lines_FadeStatus = FadeStatus.None;
            }
        }
    }

    // TxtHUD用待機キューにScenarioLineのListを渡す
    public void EnqueueLineList(List<ScenarioLine> sl_list){
        foreach(ScenarioLine sl in sl_list){
            EnqueueLine(sl);
        }
    }
    // TxtHUD用待機キューにScenarioLineを渡す
    public void EnqueueLine(ScenarioLine sl){
        scenarioLines_queue.Enqueue(sl);
    }

    // TxtHUD用待機キューからScenarioLineを取り出す
    public ScenarioLine DequeueLine(){
        if(scenarioLines_queue.Count == 0){
            return null;
        }else{
            return scenarioLines_queue.Dequeue();
        }
    }

    // TxtHUD用待機キューからScenarioLineを取り出して，表示する
    public void outputNextLine(){
        if(scenarioLines_queue.Count != 0){
            CancelInvoke();
            ScenarioLine sl = DequeueLine();
            Lines_GameObject.SetActive(true);
            Lines_CanvasGroup_Component.alpha = 1;
            Lines_TMProUGUI_Component.text = sl.getWord();
            // フェード用のを仕掛ける
            Invoke(nameof(DelayedLinesFadeOut), Mathf.Abs(sl.getDelay() - fadeTime));
            // オブジェクトをdisableにする仕掛け
            Invoke(nameof(DelayedDeactivate), sl.getDelay());
        }
    }

    // 即時描画
    public void quickOutputLine(ScenarioLine sl){
        CancelInvoke();
        Lines_GameObject.SetActive(true);
        Lines_FadeStatus = FadeStatus.None;
        Lines_CanvasGroup_Component.alpha = 1;
        Lines_TMProUGUI_Component.text = sl.getWord();
        Invoke(nameof(DelayedLinesFadeOut), Mathf.Abs(sl.getDelay() - fadeTime));
        Invoke(nameof(DelayedDeactivate), sl.getDelay());
    }

    protected void DelayedDeactivate(){
        Lines_GameObject.SetActive(false);
    }

    protected void DelayedLinesFadeOut(){
        Lines_FadeStatus = FadeStatus.FadeOut;
    }
}

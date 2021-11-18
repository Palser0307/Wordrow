﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 第二世代用upgrade
// 全統合版？

// 必須コンポーネントを補填
// 物理判定
[RequireComponent(typeof(Rigidbody))]
// 掴めるように
[RequireComponent(typeof(OVRGrabbable))]
// カプセル型当たり判定
[RequireComponent(typeof(CapsuleCollider))]

public class upgrader : MonoBehaviour{
    // タイプ
    public string Upgrade_Type = "";

    protected string UpgraderName = "";
    public string getUpgraderName(){
        return this.UpgraderName;
    }
    public void setUpgraderName(string newName){
        this.UpgraderName = newName;
    }

    private bool isHold = false;

    protected FixedJoint joint;

    protected List<string> UpgraderType = new List<string>();

    protected GameObject Card;

    protected GameObject DownCard;

    protected Rigidbody rigid;

    protected OVRGrabbable grab;

    void Start(){
        setupTypeList();
        addUpgraderType("plural");

        if(haveUpgraderType(Upgrade_Type)){
            setCardName(Upgrade_Type);
            setUpgraderName(Upgrade_Type);
        }
        // Rigidbodyアクセサを取得
        rigid = this.GetComponent<Rigidbody>();
        // 力学無視
        //rigid.isKinematic = true;
        // 重力無視
        rigid.useGravity = false;
        grab = this.GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update(){
        updateIsHold();
        rigid.velocity = Vector3.zero;
    }

    public void outputLog(string str){
        Debug.Log(getObjectName() + " : " + str);
    }

    // GETTER

    // get isHold
    public bool getIsHold(){
        return this.isHold;
    }

    // get Object Name
    public string getObjectName(){
        return this.name;
    }

    // SETTER

    public void setCardName(string newCardName){
        // this.cardName = newCardName;
        this.name = "Upgrader_" + newCardName;
    }

    // set isHold
    public void setIsHold(bool newStatus){
        this.isHold = newStatus;
    }

    public void updateIsHold(){
        if(getIsHold() != grab.isGrabbed){
            setIsHold(grab.isGrabbed);
        }
    }

    public bool haveUpgraderType(string type){
        return this.UpgraderType.Contains(type);
    }
    public void addUpgraderType(string type){
        if(!haveUpgraderType(type)){
            this.UpgraderType.Add(type);
        }
    }
    public void setupTypeList(){
        addUpgraderType("plural");
    }

}

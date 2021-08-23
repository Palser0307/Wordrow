using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour{
    private CharacterController characterController;
    private Vector3 moveDirection;
    public float JumpPower; // default: 0.5
    public float FallRate;  // default: 4

    void Start(){
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update(){
        if(characterController.isGrounded){
            if(OVRInput.GetDown(OVRInput.RawButton.A)){
                moveDirection.y = JumpPower;
                // Debug.Log("Jump");
            }
        }
        // Debug.Log(moveDirection.y);
        if(moveDirection.y < 0){
            moveDirection.y = 0;
        }else{
            moveDirection.y += (Physics.gravity.y * Time.deltaTime) / FallRate;
        }
        characterController.Move(moveDirection);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private Transform PlayerOverworld_TF;

    private Vector2 PlayerMoveVector;
    private MyInputManager inputManager;
    private PlayerInputActions PlayerInputs;
    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = true;
        Start_Or_OnEnable();
    }

    private void OnEnable()
    {
        if(hasStarted == true)
        {
            Start_Or_OnEnable();
        }
    }

    private void OnDisable()
    {
        //unsubscribe inputs to actions
        PlayerInputs.Overworld.Back.performed -= OpenPausePopup;
    }

    private void Start_Or_OnEnable()
    {
        GetReferences();
        //subscribe inputs to actions
        PlayerInputs.Overworld.Back.performed += OpenPausePopup;
    }

    // Update is called once per frame
    void Update()
    {
        //get movement from input
        PlayerMoveVector = PlayerInputs.Overworld.Movement.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        //move the player
        PlayerOverworld_TF.Translate(PlayerMoveVector * MovementSpeed);
    }

    private void OpenPausePopup(InputAction.CallbackContext context)
    {
        //inputManager.SubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, )
    }

    private void GetReferences()
    {
        if(inputManager == null)
        {
            inputManager = MyGameManager.Instance.GetInputManager();
        }

        if(PlayerInputs == null)
        {
            PlayerInputs = inputManager.GetPlayerInputs();
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private Transform PlayerOverworld_TF;
    [Space(20)]
    [SerializeField] private float RunAway_IframeDuration;
    [SerializeField] private SpriteRenderer PlayerOverworldSprite;
    [SerializeField] private Color DullColor;
    [SerializeField] private Color ActiveColor;
    [SerializeField] private BoxCollider2D Collider;

    private Vector2 PlayerMoveVector;
    private MyInputManager inputManager;
    private PlayerInputActions PlayerInputs;
    private bool hasStarted = false;
    private IEnumerator RunAway_IFrame_Coroutine;

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
        if(hasStarted == false)
        {
            return;
        }
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

    public void Interrupt_RunAwayIFrame()
    {
        if(RunAway_IFrame_Coroutine != null)
        {
            StopCoroutine(RunAway_IFrame_Coroutine);
            RunAway_IFrame_Coroutine = null;
        }
    }

    public void Start_RunAwayIFrame()
    {
        Toggle_Collider(false);

        Interrupt_RunAwayIFrame();

        RunAway_IFrame_Coroutine = RunAway_IFrame();
        if(gameObject.activeSelf == true)
        {
            StartCoroutine(RunAway_IFrame_Coroutine);
        }
        
    }

    public IEnumerator RunAway_IFrame()
    {
        float timer = 0f;

        float colorValue = 0f;

        float bounceAmount = 8f;
        float colorMaxPingPongValue = RunAway_IframeDuration/bounceAmount;

        while(timer < RunAway_IframeDuration)
        {
            yield return null;


            colorValue = Mathf.PingPong(timer / colorMaxPingPongValue, 1);

            PlayerOverworldSprite.color = Color.Lerp(ActiveColor, DullColor, colorValue);

            timer+=Time.deltaTime;
        }

        PlayerOverworldSprite.color = ActiveColor;

        //timer done, end invinsibility
        Toggle_Collider(true);
    }

    public void Toggle_Collider(bool newStatus)
    {
        Collider.enabled = newStatus;
    }

}

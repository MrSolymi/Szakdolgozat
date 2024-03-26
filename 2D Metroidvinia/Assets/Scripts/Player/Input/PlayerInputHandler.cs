using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Input = UnityEngine.Windows.Input;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    
    [SerializeField] private float inputHoldTime = 0.2f;
    private float _jumpInputStartTime;
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        //Debug.Log("Move input: " + context.ReadValue<Vector2>());
        
        RawMovementInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormalizedInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormalizedInputX = 0;
        }
        
        if (Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormalizedInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormalizedInputY = 0;
        }
    }
    
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }
        
        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }
        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void UseJumpInput() => JumpInput = false;
    
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}

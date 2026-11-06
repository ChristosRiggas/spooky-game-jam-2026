using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    InputSystem_Actions controls;

    // Continuous state (cached from events)
    public Vector2 Move { get; private set; }
    public bool JumpHeld { get; private set; }

    // Discrete events
    public event Action JumpPressed;
    public event Action JumpReleased;
    public event Action InteractPressed;

    void Awake() => controls = new InputSystem_Actions();

    void OnEnable()
    {
        var p = controls.Player;
        p.Move.performed += OnMove;
        p.Move.canceled += OnMove;
        p.Jump.performed += OnJump;
        p.Jump.canceled += OnJumpCanceled;
        p.Interact.performed += OnInteract;
        p.Enable();
    }

    void OnDisable()
    {
        var p = controls.Player;
        p.Move.performed -= OnMove;
        p.Move.canceled -= OnMove;
        p.Jump.performed -= OnJump;
        p.Jump.canceled -= OnJumpCanceled;
        p.Interact.performed -= OnInteract;
        p.Disable();
        Move = Vector2.zero;
        JumpHeld = false;
    }

    void OnDestroy() => controls?.Dispose();

    void OnMove(InputAction.CallbackContext c) => Move = c.ReadValue<Vector2>();
    void OnJump(InputAction.CallbackContext c) { JumpHeld = true; JumpPressed?.Invoke(); }
    void OnJumpCanceled(InputAction.CallbackContext c) { JumpHeld = false; JumpReleased?.Invoke(); }
    void OnInteract(InputAction.CallbackContext c) => InteractPressed?.Invoke();
}
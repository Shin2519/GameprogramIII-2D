using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public PlayerInput playerInput;
    public Rigidbody2D rb;
    public Animator animator;

    public virtual void Start() { }
    public virtual void Update(bool isGrounded) { }
    public virtual void End() { }

}

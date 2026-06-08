using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public PlayerInput playerInput;
    public Rigidbody2D rb;
    public Animator animator;

    public enum StateType
    {
        Move,
        Attack,
    }

    public virtual void Start() { }
    public virtual void Update(bool isGrounded,out StateType nextState) 
    {
        nextState = StateType.Move;
    }
    public virtual void End() { }

}

public class StateMove:State
{
    public float speed;
    public float jumpSpeed;
    public GameObject Floor;

    public override void Update(bool isGrounded,out StateType nextState)
    {
        //isGrounded = true;
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocityX = move.x * speed;

        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            Debug.Log("ƒWƒƒƒ“ƒvŒÄ‚Ño‚µ");
            rb.linearVelocityY = jumpSpeed;
        }

        if (playerInput.actions["Attack"].WasPressedThisFrame())
        {
            nextState = StateType.Attack; 
        }
        else
        {
            nextState = StateType.Move;
        }
    }
}
public class StateAttack : State
{
    public override void Start()
    {
        animator.Play("Attack");
    }

    public override void Update(bool isGrounded, out StateType nextState)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            nextState = StateType.Move;
        }
        else
        {
            nextState = StateType.Attack;
        }
    }
}

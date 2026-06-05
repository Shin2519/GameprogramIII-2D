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

public class StateMove:State
{
    public float speed;
    public float jumpSpeed;

    public override void Update(bool isGrounded)
    {
        //isGrounded = true;
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        rb.linearVelocityX = move.x * speed;

        if (playerInput.actions["Jump"].WasPressedThisFrame())
        {
            Debug.Log("ƒWƒƒƒ“ƒvŒÄ‚Ño‚µ");
            rb.linearVelocityY = jumpSpeed*Time.deltaTime;
        }
    }
}

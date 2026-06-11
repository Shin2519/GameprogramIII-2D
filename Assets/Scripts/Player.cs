using R3;               // R3 core
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using static State;

public class Player : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    bool isGrounded;
    

    public float MaxLife => 100f;
    public ReactiveProperty<float> life { get; private set; } = new();

    PlayerInput playerInput;
    Rigidbody2D rb;
    Animator animator;

    State state;
    StateMove stateMove = new();
    StateAttack stateAttack = new();

    

    State[] states = new State[]
    {
        new StateMove(),
        new StateAttack(),
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        state = states[(int)StateType.Move];
        states[(int)StateType.Move].playerInput = playerInput;
        states[(int)StateType.Move].rb = rb;
        (states[(int)StateType.Move]as StateMove).speed = speed;
        (states[(int)StateType.Move]as StateMove).jumpSpeed = jumpSpeed;
        states[(int)StateType.Move].animator = animator;
        states[(int)StateType.Attack].playerInput = playerInput;
        states[(int)StateType.Attack].rb = rb;
        states[(int)StateType.Attack].animator = animator;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update(isGrounded,out var nextState);
        if (state != states[(int)nextState])
        {
            state.End();
            state = states[(int)nextState];
            state.Start();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            if (isGrounded == false)
            {
                isGrounded = true;
            }
        }
    }

}

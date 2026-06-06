using UnityEngine;
using UnityEngine.InputSystem;
using R3;               // R3 core
using R3.Triggers;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        life.Value = MaxLife;

        Debug.Log(playerInput.actions["Jump"]);

        state = stateMove;
        stateMove.playerInput = playerInput;
        stateMove.rb = rb;
        stateMove.speed = speed;
        stateMove.jumpSpeed = jumpSpeed;
        stateMove.animator = animator;
    }

    // Update is called once per frame
    void Update()
    {
        state.Update(isGrounded);
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

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private float speed = 2f;

    private Animator animator;
    private Rigidbody2D rb;
    private InputAction moveAction;
    private Vector2 lastMovement;

    private int AnimMoveX = Animator.StringToHash("AnimMoveX");
    private int AnimMoveY = Animator.StringToHash("AnimMoveY");
    private int AnimLastMoveX = Animator.StringToHash("AnimLastMoveX");
    private int AnimLastMoveY = Animator.StringToHash("AnimLastMoveY");
    private int AnimIsMoving = Animator.StringToHash("AnimIsMoving");

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        GameManager.Instance.SetPlayerController(this);
    }

    private void Start()
    {
        if (inputActionAsset == null)
        {
            Debug.LogError("Input Action Asset missing on PlayerController");
            return;
        }

        moveAction = inputActionAsset.FindAction("Player/Move");
        moveAction.Enable();
    }

    private void FixedUpdate()
    {
        var move = moveAction.ReadValue<Vector2>();
        var movement = move * speed;

        if (move.x != 0 || move.y != 0)
        {
            lastMovement = move;
        }

        rb.MovePosition(rb.position + movement * Time.deltaTime);

        AnimateCharacter(move);
    }

    private void AnimateCharacter(Vector2 moveDirection)
    {
        animator.SetFloat(AnimMoveX, moveDirection.x);
        animator.SetFloat(AnimMoveY, moveDirection.y);
        animator.SetFloat(AnimLastMoveX, lastMovement.x);
        animator.SetFloat(AnimLastMoveY, lastMovement.y);
        animator.SetBool(AnimIsMoving, moveDirection.magnitude > 0.1f);


    }
}

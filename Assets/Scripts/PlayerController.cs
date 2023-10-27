using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header ("Reference settings")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private SpriteRenderer hatRenderer;
    [SerializeField] private SpriteRenderer shirtRenderer;
    [SerializeField] private SpriteRenderer pantsRenderer;
    [Header("Controllable Variables")]
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

    public CoinController CoinController { get; set; }
    public InventorySystem InventorySystem { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        InventorySystem = GetComponent<InventorySystem>();
        CoinController = GetComponent<CoinController>();

        if (InventorySystem != null )
        {
            InventorySystem.Initialize();
        }

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

    public void UnequipItem(Equipment equipment)
    {
        switch (equipment.Type)
        {
            case EquipmentType.Hat:
                {
                    hatRenderer.gameObject.SetActive(false);
                }
                break;
            case EquipmentType.Shirt:
                {
                    shirtRenderer.gameObject.SetActive(false);
                }
                break;
            case EquipmentType.Pants:
                {
                    pantsRenderer.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void EquipItem(Equipment equipment)
    {
        switch (equipment.Type)
        {
            case EquipmentType.Hat:
                {
                    hatRenderer.sprite = equipment.VisualItemSprite;
                    hatRenderer.gameObject.SetActive(true);
                }
                break;
            case EquipmentType.Shirt:
                {
                    shirtRenderer.sprite = equipment.VisualItemSprite;
                    shirtRenderer.gameObject.SetActive(true);
                }
                break;
            case EquipmentType.Pants:
                {
                    pantsRenderer.sprite = equipment.VisualItemSprite;
                    pantsRenderer.gameObject.SetActive(true);
                }
                break;
        }
    }
}

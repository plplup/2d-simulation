using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Sprite openedDoorSprite;
    [SerializeField] private Sprite closedDoorSprite;
    [SerializeField] private SpriteRenderer doorSpriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorSpriteRenderer.sprite = openedDoorSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        doorSpriteRenderer.sprite = closedDoorSprite;
    }
}

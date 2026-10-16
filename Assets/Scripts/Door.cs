using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    //[SerializeField] private MeshRenderer meshRenderer;
    public Sprite closedSprite, openSprite;
    public SpriteRenderer spriteRenderer;
    public bool isOpened = false;
    public bool canBeOpenedByKey;

    private void OnCollisionEnter(Collision collision)
    {
        if (canBeOpenedByKey && !isOpened && collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.transform.parent.GetComponent<PlayerController>().KeyCount() > 0)
            {
                collision.gameObject.transform.parent.GetComponent<PlayerController>().RemoveKey();
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        boxCollider.enabled = false;
        spriteRenderer.sprite = openSprite;
        isOpened = true;
    }

    public void CloseDoor()
    {
        boxCollider.enabled = true;
        spriteRenderer.sprite = closedSprite;
        isOpened = false;
    }


}

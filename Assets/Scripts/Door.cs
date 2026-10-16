using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public String sendToScene;
    [SerializeField] private BoxCollider boxCollider;
    //[SerializeField] private MeshRenderer meshRenderer;
    public Sprite closedSprite, openSprite;
    public SpriteRenderer spriteRenderer;
    public bool isOpened = false;
    public bool canBeOpenedByKey;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            if (canBeOpenedByKey && !isOpened)
            {
                if (collision.gameObject.transform.parent.GetComponent<PlayerController>().KeyCount() > 0)
                {
                    collision.gameObject.transform.parent.GetComponent<PlayerController>().RemoveKey();
                    OpenDoor();
                }
            }

            if(isOpened && sendToScene != "")
            {
                PlayerController p = collision.gameObject.transform.parent.GetComponent<PlayerController>();
                
                // reset collected key save data to be empty after completion if exist
                if(p.keyTracker != null){
                    if (p.keyTracker.isActiveAndEnabled == true)
                    {
                        p.keyTracker.SaveEmptyKeyData();    
                    }
                }

                SceneManager.LoadScene(sendToScene);
            }
        }

    }

    public void OpenDoor()
    {
        if(sendToScene == "") {
            boxCollider.enabled = false;
        }
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

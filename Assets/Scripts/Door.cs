using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MeshRenderer meshRenderer;

   

    public void OpenDoor()
    {
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
    }

    public void CloseDoor()
    {
        boxCollider.enabled = true;
        meshRenderer.enabled = true;
    }


}

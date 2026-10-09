using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    Outline outline;
    public void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public virtual void OnInteract(Vector3 playerFacing, PlayerController player)
    {
        // This should be overriden by extended classes
    } 



    public void EnableOutline() {outline.enabled = true;}
    public void DisableOutline() {outline.enabled = false;}

}

using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    Outline outline;
    public void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }
    public void EnableOutline() {outline.enabled = true;}
    public void DisableOutline() {outline.enabled = false;}

}

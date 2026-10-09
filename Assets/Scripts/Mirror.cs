using UnityEngine;

public class Mirror : InteractableObject
{
    public override void OnInteract(Vector3 playerFacing, PlayerController player)
    {
        player.SwapCamera();
    }
}

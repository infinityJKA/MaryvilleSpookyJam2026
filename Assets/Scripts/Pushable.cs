using UnityEngine;
using System.Collections;

public class Pushable : InteractableObject
{
    public bool isMoving = false;
    [SerializeField] private float pushDuration = 0.25f;


    public override void OnInteract(Vector3 playerFacing)
    {
        Push(playerFacing);
    }

    public void Push(Vector3 pushDir)
    {
        if (isMoving) return;

        Vector3 targetPosition = transform.position + pushDir;

        // Check if the path is clear before moving
        if (!Physics.Raycast(transform.position, pushDir, 1f, -1))
        {
            StartCoroutine(SmoothMove(targetPosition));
        }
        else
        {
            Debug.Log("Path is blocked!");
        }
    }

    private IEnumerator SmoothMove(Vector3 target)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < pushDuration)
        {
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / pushDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }



}

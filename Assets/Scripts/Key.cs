using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().AddKey();
            Destroy(gameObject);
        }
    }
}

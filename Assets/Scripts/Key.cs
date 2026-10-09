using UnityEditor.UI;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().AddKey();
            gameObject.SetActive(false);
        }
    }
}

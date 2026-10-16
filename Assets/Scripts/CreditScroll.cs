using TMPro;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    [SerializeField] RectTransform credits;
    public int creditStartPos;
    void FixedUpdate()
    {

        credits.anchoredPosition += new Vector2(0, 50 * Time.deltaTime);
    }

    public void resetCredits()
    {
        credits.anchoredPosition = new Vector2(0, creditStartPos);
    }
}

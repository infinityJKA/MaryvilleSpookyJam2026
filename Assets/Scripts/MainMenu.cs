using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject creditsUI;
    CreditScroll creditScroll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        creditScroll = creditsUI.GetComponent<CreditScroll>();
        creditsUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RollCredits()
    {
        creditsUI.SetActive(true);
    }

    public void CloseCredits()
    {
        creditScroll.resetCredits();
        creditsUI.SetActive(false);
    }
}

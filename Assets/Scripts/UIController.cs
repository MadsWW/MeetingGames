using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] UIPanels;
    public GameObject backButton;
    public Text CoinText;

    private GameObject lastActive;

    private void Start()
    {
        EnablePanel(UIPanels[0]);
    }

    public void EnablePanel(GameObject go)
    {
        ActivatedMenuPanel(go);
        if (lastActive) { lastActive.SetActive(false); }
        go.SetActive(true);
        lastActive = go;
    }

    private void ActivatedMenuPanel(GameObject go)
    {
        bool isMenuPanel = (go == UIPanels[0]);
        if (isMenuPanel) { backButton.SetActive(false); }
        else { backButton.SetActive(true); }
    }
}

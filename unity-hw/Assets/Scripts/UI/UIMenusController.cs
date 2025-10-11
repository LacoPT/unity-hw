using UnityEngine;

public class UIMenusController : MonoBehaviour
{
    [SerializeField] private Canvas GameUI;
    [SerializeField] private CanvasGroup SettingsMenu;

    public void HideGameUI()
    {
        GameUI.gameObject.SetActive(false);
    }

    public void ShowGameUI()
    {
        GameUI.gameObject.SetActive(true);
    }

    public void HideSettingsMenu()
    {
        SettingsMenu.alpha = 0;
        SettingsMenu.blocksRaycasts = false;
        SettingsMenu.interactable = false;
    }

    public void ShowSettingsMenu()
    {
        SettingsMenu.alpha = 100;
        SettingsMenu.blocksRaycasts = true;
        SettingsMenu.interactable = true;  
    }
}

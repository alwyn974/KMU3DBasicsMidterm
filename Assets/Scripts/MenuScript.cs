using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        GameManager.Instance.QuitGame();
    }
    
    public void SoundToggle()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        GameManager.Instance.ShowMusicSettings();
    }
}

using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public void StartGame()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.clickSound);
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.clickSound);
        GameManager.Instance.QuitGame();
    }
    
    public void SoundToggle()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.clickSound);
        GameManager.Instance.ShowMusicSettings();
    }
}

using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        StartCoroutine(StartGameCoroutine());
    }
    
    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        StartCoroutine(QuitGameCoroutine());
    }
    
    private IEnumerator QuitGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.QuitGame();
    }

    public void SoundToggle()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        StartCoroutine(SoundToggleCoroutine());
    }
    
    private IEnumerator SoundToggleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ShowMusicSettings();
    }
}
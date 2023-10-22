using TMPro;
using UnityEngine;

public class WinLoseScript : MonoBehaviour
{
    private TMP_Text _winLoseText;
    
    void Start()
    {
        _winLoseText = GetComponent<TMP_Text>();
        if (GameManager.Instance.Life > 0)
        {
            _winLoseText.text = "You Win!";
        }
        else
        {
            GameManager.Instance.PlaySound(GameManager.Instance.LoseSound);
            _winLoseText.text = "You Lose!";
        }
        _winLoseText.text += "\nScore: " + GameManager.Instance.Score;
    }

    public void Quit()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        GameManager.Instance.QuitGame();
    }
    
    public void Restart()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        GameManager.Instance.RestartGame();
    }
}

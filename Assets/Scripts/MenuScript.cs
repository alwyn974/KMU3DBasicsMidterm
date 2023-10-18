using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
    
    public void SoundToggle()
    {
        GameManager.Instance.SoundToggle();
    }
}

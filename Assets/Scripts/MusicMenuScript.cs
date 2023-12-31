using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicMenuScript : MonoBehaviour
{
    public Slider slider;
    public TMP_Text sliderText;
    private GameManager _gameManager;
    private float _volume;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        slider.value = _gameManager.Volume;
        _volume = slider.value;
        sliderText.text = $"Volume: {(int)(slider.value * 100)}%";
    }
    
    public void UpdateSliderText()
    {
        sliderText.text = $"Volume: {(int)(slider.value * 100)}%";
        _volume = slider.value;
    }
    
    public void Confirm()
    {
        GameManager.Instance.Volume = _volume;
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        StartCoroutine(LoadMainMenu());
    }

    public void Cancel()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.ClickSound);
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenuScene");
    }

}

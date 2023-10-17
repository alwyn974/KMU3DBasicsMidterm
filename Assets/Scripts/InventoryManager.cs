using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public TMP_Text infoText;
    private int _life = 3;
    private int _coins;
    private int _score;

    void Start()
    {
        UpdateText();
    }

    public int Life
    {
        get => _life;
        set => _life = value;
    }

    public int Coins
    {
        get => _coins;
        set => _coins = value;
    }

    public int Score
    {
        get => _score;
        set => _score = value;
    }
    
    public void AddLife(int value = 1)
    {
        _life += value;
        UpdateText();
    }
    
    public void AddCoin(int value = 1)
    {
        _coins += value;
        AddScore(value * 100);
        UpdateText();
    }
    
    public void AddScore(int value)
    {
        _score += value;
        UpdateText();
    }
    
    
    public void Reset()
    {
        _life = 3;
        _coins = 0;
        _score = 0;
        UpdateText();
    }

    public void UpdateText()
    {
        infoText.text = $"Life: {_life}\nCoins: {_coins}\nScore: {_score}";
    }
}

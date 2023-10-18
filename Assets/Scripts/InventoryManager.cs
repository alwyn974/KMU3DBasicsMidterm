using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public TMP_Text infoText;
    public TMP_Text lifeText;
    public TMP_Text coinsText;
    private static int _life = 3;
    private static int _coins;
    private static int _score;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
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
        // infoText.text = $"Life: {_life}\nCoins: {_coins}\nScore: {_score}";
        infoText.text = $"Score: {_score}";
        lifeText.text = $"x{_life}";
        coinsText.text = $"x{_coins}";
    }
}
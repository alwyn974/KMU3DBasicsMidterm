using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TMP_Text infoText;
    public TMP_Text lifeText;
    public TMP_Text coinsText;
    private static int _life = 3;
    private static int _coins;
    private static int _score;
    private static int _highScore;
    private static Level _level;

    private static Dictionary<Level, bool> _bossKilled = _bossKilled = new Dictionary<Level, bool>
    {
        { Level.One, false },
        { Level.Two, false },
        { Level.Three, false }
    };

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

    public int HighScore
    {
        get => _highScore;
        set => _highScore = value;
    }

    public Level Level
    {
        get => _level;
        set => _level = value;
    }
    
    public Dictionary<Level, bool> BossKilled
    {
        get => _bossKilled;
        set => _bossKilled = value;
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
    }

    public void AddScore(int value)
    {
        _score += value;
        UpdateText();
    }

    public void Reset() // aka restart
    {
        _life = 3;
        _coins = 0;
        _score = 0;
        _level = Level.One;
        _bossKilled = new Dictionary<Level, bool>
        {
            { Level.One, false },
            { Level.Two, false },
            { Level.Three, false }
        };
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

public enum Level
{
    One,
    Two,
    Three
}
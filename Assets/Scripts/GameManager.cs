using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TMP_Text infoText;
    public TMP_Text lifeText;
    public TMP_Text coinsText;
    public AudioClip ClickSound { get; private set; }
    public AudioClip JumpSound { get; private set; }
    public AudioClip BossShootSound { get; private set; }
    public AudioClip FlagDownSound { get; private set; } // win
    public AudioClip CastleSound { get; private set; } // win
    public AudioClip CoinSound { get; private set; }
    public AudioClip PowerUpSound { get; private set; } // bonus
    public AudioClip LoseSound { get; private set; } // lose
    public AudioClip PlayerDeath { get; private set; }
    public AudioClip PlayerShoot { get; private set; }
    public AudioClip PowerUpSpawn { get; private set; }
    public AudioClip InvincibleMusic { get; private set; }
    public AudioClip PowerDown { get; private set; }
    public AudioClip BossDeath { get; private set; }
    public AudioClip PlayerShootHit { get; private set; }
    public AudioClip StompSound { get; private set; }

    private static float _volume = 0.5f;
    private static int _life = 3;
    private static int _coins;
    private static int _score;
    private static int _highScore;
    private static Level _level;

    public void StartGame()
    {
        SceneManager.LoadScene($"Level0{(int)_level + 1}Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Reset();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ShowMusicSettings()
    {
        SceneManager.LoadScene("SettingsMenuScene");
    }

    private static Dictionary<Level, bool> _bossKilled;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("oh");
        Reset();
        // instanciate audioclip from code with the path of Assets/Sounds/...
        ClickSound = Resources.Load<AudioClip>("Sounds/minecraft_click");
        JumpSound = Resources.Load<AudioClip>("Sounds/smw_jump");
        BossShootSound = Resources.Load<AudioClip>("Sounds/boss7_shot");
        FlagDownSound = Resources.Load<AudioClip>("Sounds/flag_down");
        CastleSound = Resources.Load<AudioClip>("Sounds/sm64_high_score");
        CoinSound = Resources.Load<AudioClip>("Sounds/coin");
        PowerUpSound = Resources.Load<AudioClip>("Sounds/nsmbwiiPowerup");
        LoseSound = Resources.Load<AudioClip>("Sounds/gameover 1");
        PlayerDeath = Resources.Load<AudioClip>("Sounds/nsmbwiiDeath");
        PlayerShoot = Resources.Load<AudioClip>("Sounds/nsmbwiiFireBall");
        PowerUpSpawn = Resources.Load<AudioClip>("Sounds/smw_power-up_appears");
        InvincibleMusic = Resources.Load<AudioClip>("Sounds/Super Mario Bros Invincibility Theme Sound Effect");
        PowerDown = Resources.Load<AudioClip>("Sounds/nsmbwiiPowerDown");
        PlayerShootHit = Resources.Load<AudioClip>("Sounds/nsmbwiiFireBallHit1");
        StompSound = Resources.Load<AudioClip>("Sounds/smw_stomp");
        BossDeath = Resources.Load<AudioClip>("Sounds/E3L6N5R-boss-death-4");
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

    public float Volume
    {
        get => _volume;
        set => _volume = value;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, _volume);
    }

    public void PlaySound(AudioSource source, AudioClip clip)
    {
        if (clip)
            source.PlayOneShot(clip, _volume);
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
            // { Level.Three, false }
        };
        UpdateText();
    }

    public void UpdateText()
    {
        // infoText.text = $"Life: {_life}\nCoins: {_coins}\nScore: {_score}";
        if (infoText)
            infoText.text = $"Score: {_score}";
        if (lifeText)
            lifeText.text = $"x{_life}";
        if (coinsText)
            coinsText.text = $"x{_coins}";
    }
}

public enum Level
{
    One,
    Two,
    // Three
}
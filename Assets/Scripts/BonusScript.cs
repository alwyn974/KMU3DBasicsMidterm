using UnityEngine;

public class BonusScript : MonoBehaviour
{
    public BonusType type;
    private PlayerScript _playerScript;

    private void Start()
    {
        _playerScript = FindObjectOfType<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("player")) return;
        _playerScript.OnPlayerGetBonus(type);
        Destroy(gameObject);
    }
}
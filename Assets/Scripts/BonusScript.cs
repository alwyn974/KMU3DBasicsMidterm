using System;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    private BoxCollider2D _collider2D;
    public BonusType type;
    private PlayerScript _playerScript;

    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        _playerScript = FindObjectOfType<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("player")) return;
        switch (type)
        {
            case BonusType.Shield:
                _playerScript.InventoryManager.AddScore(1000);
                // TODO: add shield
                break;
            case BonusType.Flower:
                _playerScript.InventoryManager.AddScore(200);
                // TODO: add flower
                break;
            case BonusType.Coin:
                _playerScript.InventoryManager.AddCoin();
                break;
        }
        Destroy(gameObject);
    }
}
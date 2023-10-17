using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlockScript : MonoBehaviour
{
    public Sprite bonusShootSprite;
    public Sprite bonusInvincibleSprite;
    public Sprite bonusCoinSprite;
    public BonusType type;
    public PlayerScript playerScript;
    
    private bool _used = false;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_used) return;
        if (!other.gameObject.CompareTag("player")) return;
     
        // check if collision is from bottom
        if (other.contacts[0].normal.y < 0.5f) return;
        
        _used = true;
        
        switch (type)
        {
            case BonusType.Shield:
                // spawn prefab
                break;
            case BonusType.Flower:
                // spawn prefab
                break;
            case BonusType.Coin:
                // spawn prefab ?
                playerScript.InventoryManager.AddCoin();
                break;
        }
        Debug.Log(other.gameObject.name);
        Debug.Log(type);
        
        _animator.SetBool("isUsed", _used);
    }
}

public enum BonusType {
    Shield, Flower, Coin
}

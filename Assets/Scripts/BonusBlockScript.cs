using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BonusBlockScript : MonoBehaviour
{
    public GameObject bonusFlowerPrefab;
    public GameObject bonusShieldPrefab;
    public GameObject bonusCoinPrefab;
    public BonusType type;

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

        // create ternary based on type
        var prefab = type == BonusType.Shield ? bonusShieldPrefab :
            type == BonusType.Flower ? bonusFlowerPrefab : bonusCoinPrefab;
        GameManager.Instance.PlaySound(GameManager.Instance.PowerUpSpawn);
        Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity);

        _animator.SetBool("isUsed", _used);
    }
}

public enum BonusType
{
    Shield,
    Flower,
    Coin
}
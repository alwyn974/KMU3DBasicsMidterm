using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScript : MonoBehaviour
{
    public SpriteRenderer flagSpriteRenderer;
    public PlayerScript playerScript;
    
    private bool _flagDown;
    private Transform _standTransform;

    private void Start()
    {
        _standTransform = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        // get the bottom of the flag stand
        var standBottom = _standTransform.position.y - _standTransform.localScale.y * 3;
        if (_flagDown)
        {
            if (flagSpriteRenderer.transform.position.y > standBottom)
                flagSpriteRenderer.transform.Translate(Vector2.down * 0.1f);
            else
                flagSpriteRenderer.GetComponent<Animator>().SetTrigger("Down");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.gameObject.CompareTag("player"))) return;
        var gameManager = playerScript.GameManager;
        if (gameManager.BossKilled[gameManager.Level] && !_flagDown)
        {
            _flagDown = true;
        }
    }
}

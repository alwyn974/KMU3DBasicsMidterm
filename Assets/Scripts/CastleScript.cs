using UnityEngine;

public class CastleScript : MonoBehaviour
{
    public PlayerScript playerScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.gameObject.CompareTag("player"))) return;
        var gameManager = playerScript.GameManager;
        if (gameManager.BossKilled[gameManager.Level])
            playerScript.NextLevel();
    }
}
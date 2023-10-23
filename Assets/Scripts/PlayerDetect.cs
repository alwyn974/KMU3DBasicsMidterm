using System.Collections;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    public bool playerDetected { get; private set; }

    public Vector2 directionTarget => player.transform.position - detectorOrigin.position;

    public Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.zero;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;

    public LayerMask detectionLayer;

    private GameObject player;

    public Color debugDetectColor = Color.red;
    public Color debugNotDetectColor = Color.green;
    public bool debug = true;

    public GameObject Player
    {
        get => player;
        set
        {
            player = value;
            playerDetected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset,
            detectorSize, 0, detectionLayer);
        if (collider != null)
        {
            player = collider.gameObject;
            playerDetected = true;
        }
        else
        {
            player = null;
            playerDetected = false;
        }
    }

    public void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = playerDetected ? debugDetectColor : debugNotDetectColor;
            Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);
        }
    }
}
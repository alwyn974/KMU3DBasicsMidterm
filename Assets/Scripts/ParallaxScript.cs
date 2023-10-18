using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    private SpriteRenderer[] _spriteRenderers = new SpriteRenderer[3];
    public Sprite backgroundImage;
    public Camera gameCamera;

    public float parallaxValue;
    public int layerOrder;

    private Vector2 _length;
    private Vector3 _startPos;

    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _startPos = transform.position;
        for (var i = 0; i < 3; i++)
        {
            _spriteRenderers[i].sprite = backgroundImage;
            _spriteRenderers[i].sortingOrder = layerOrder;

            Vector3 tmpPos;
            switch (i)
            {
                case 1:
                    _length = _spriteRenderers[0].bounds.size;
                    tmpPos = _spriteRenderers[i].gameObject.transform.position;
                    tmpPos.x += _length.x;
                    _spriteRenderers[i].gameObject.transform.position = tmpPos;
                    break;
                case 2:
                    tmpPos = _spriteRenderers[i].gameObject.transform.position;
                    tmpPos.x -= _length.x;
                    _spriteRenderers[i].gameObject.transform.position = tmpPos;
                    break;
            }
        }
    }

    void Update()
    {
        var relativePos = gameCamera.transform.position * parallaxValue;
        var dist = gameCamera.transform.position - relativePos;

        if (dist.x > _startPos.x + _length.x)
            _startPos.x += _length.x;
        if (dist.x < _startPos.x - _length.x)
            _startPos.x -= _length.x;

        relativePos.z = 0;
        transform.position = _startPos + relativePos;
    }
}
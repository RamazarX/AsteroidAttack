using UnityEngine;

public class empWaveEffect : MonoBehaviour
{
    public float expandSpeed = 15f;
    public float maxRadius = 15f;
    private SpriteRenderer sr;
    private Color waveColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        waveColor = sr.color;
        transform.localScale = Vector3.zero;
    }
    void Update()
    {
        transform.localScale += Vector3.one * expandSpeed * Time.deltaTime;
        float alpha = 1f - (transform.localScale.x / maxRadius);
        waveColor.a = Mathf.Clamp01(alpha);
        sr.color = waveColor;
        if (transform.localScale.x >= maxRadius)
        {
            Destroy(gameObject);
        }
    }
}

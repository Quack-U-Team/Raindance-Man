using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    private Light2D candleLight;
    public float flickerAmount = 0.2f;
    public float flickerSpeed = 0.3f;
    private float baseIntensity;

    void Start()
    {
        candleLight = GetComponent<Light2D>();
        if (candleLight != null)
        {
            baseIntensity = candleLight.intensity;
        }
    }

    void Update()
    {
        if (candleLight != null)
        {
            candleLight.intensity = baseIntensity + Mathf.Sin(Time.time * flickerSpeed) * flickerAmount;
        }
    }
}
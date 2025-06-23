using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Candle : MonoBehaviour
{
    private Light2D candleLight;
    public float flickerAmount = 0.2f;
    public float flickerSpeed = 0.3f;

    void Start()
    {
        candleLight = GetComponent<Light2D>();
    }

    void Update()
    {
        if (candleLight != null)
        {
            candleLight.intensity *= Mathf.Sin(Time.time * flickerSpeed) * flickerAmount;
        }
    }
}
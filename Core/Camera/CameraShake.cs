using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float intensidad = 0.05f;
    public float velocidad = 20f;

    private Vector3 posInicial;

    private void Start()
    {
        posInicial = transform.localPosition;
    }

    private void Update()
    {
        float x = Mathf.Sin(Time.time * velocidad) * intensidad;
        float y = Mathf.Cos(Time.time * velocidad * 1.3f) * intensidad;
        transform.localPosition = posInicial + new Vector3(x, y, 0);
    }
}

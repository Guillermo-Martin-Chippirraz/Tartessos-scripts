using UnityEngine;

public class AgroSystem : MonoBehaviour
{
    public bool hasAgro = false;
    public float agroDuration = 5f;
    private float agroTimer;

    public void TriggerAgro()
    {
        hasAgro = true;
        agroTimer = agroDuration;
    }
    void Update()
    {
        if (!hasAgro) return;

        agroTimer -= Time.deltaTime;
        if (agroTimer <= 0)
        {
            hasAgro = false;
        }
    }
}

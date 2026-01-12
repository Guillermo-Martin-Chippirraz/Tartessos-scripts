using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MetricsManager : MonoBehaviour
{
    public static MetricsManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator RegistrarEvento(int idUsuario, string accion, int valor = 0)
    {
        var dto = new
        {
            id_usuario = idUsuario,
            accion = accion,
            valor = valor
        };

        string url = ApiClient.baseURL + "/api/v1/metricas/evento";
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error registrando métrica: " + req.error);
        }
    }
}
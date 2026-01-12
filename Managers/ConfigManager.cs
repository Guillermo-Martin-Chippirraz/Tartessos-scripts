using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator ObtenerConfiguracion(Action<ConfiguracionSistemaDTO> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/configuracion";

        UnityWebRequest req = ApiClient.AuthGet(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener configuración: " + req.error);
            callback(null);
            yield break;
        }

        ConfiguracionSistemaDTO config = JsonUtility.FromJson<ConfiguracionSistemaDTO>(req.downloadHandler.text);
        callback(config);
    }

    public IEnumerator ActualizarConfiguracion(string json, Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/configuracion";

        UnityWebRequest req = ApiClient.AuthPut(url, json);
        yield return req.SendWebRequest();

        callback(req.result == UnityWebRequest.Result.Success);
    }
}
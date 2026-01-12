using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PreferencesManager : MonoBehaviour
{
    public static PreferencesManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator ObtenerPreferencias(string jugadorId, Action<PreferenciasDTO> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/preferencias/" + jugadorId;

        UnityWebRequest req = ApiClient.AuthGet(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener preferencias: " + req.error);
            callback(null);
            yield break;
        }

        PreferenciasDTO prefs = JsonUtility.FromJson<PreferenciasDTO>(req.downloadHandler.text);
        callback(prefs);
    }

    public IEnumerator ActualizarPreferencias(string jugadorId, PreferenciasDTO dto, Action<PreferenciasDTO> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/preferencias/" + jugadorId;

        UnityWebRequest req = ApiClient.AuthPut(url, dto);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al actualizar preferencias: " + req.error);
            callback(null);
            yield break;
        }

        PreferenciasDTO prefs = JsonUtility.FromJson<PreferenciasDTO>(req.downloadHandler.text);
        callback(prefs);
    }
}
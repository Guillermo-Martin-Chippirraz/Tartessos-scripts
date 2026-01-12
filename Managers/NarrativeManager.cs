using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator ObtenerEvento(string id, Action<EventoDTO> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/eventos/" + id;

        UnityWebRequest req = ApiClient.AuthGet(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener evento: " + req.error);
            callback(null);
            yield break;
        }

        EventoDTO evento = JsonUtility.FromJson<EventoDTO>(req.downloadHandler.text);
        callback(evento);
    }
}
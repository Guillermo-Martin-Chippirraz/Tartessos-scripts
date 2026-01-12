using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLogManager : MonoBehaviour
{
    public static NetworkLogManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator RegistrarEvento(LogRedRequest dto, Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/red/logs";
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error registrando log de red: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }
}
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ChatManager : MonoBehaviour
{
    public static ChatManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator EnviarMensaje(int idSesion, ChatRequest dto, Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/chat" + idSesion;
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error enviando mensaje: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }

    public IEnumerator ObtenerChat(int idSesion, Action<string> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/chat/" + idSesion;
        UnityWebRequest req = ApiClient.AuthGet(url);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error obteniendo chat: " + req.error);
            callback(null);
            yield break;
        }

        callback(req.downloadHandler.text);
    }
}
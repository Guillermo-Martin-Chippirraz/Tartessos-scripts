using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LogrosManager : MonoBehaviour
{
    public static LogrosManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator DesbloquearLogro(LogroRequest dto, Action<string> callback)
    {
        string url = ApiClient.baseURL + "/logros/desbloquear";
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error logro: " + req.error);
            callback(null);
            yield break;
        }

        callback(req.downloadHandler.text);
    }
}

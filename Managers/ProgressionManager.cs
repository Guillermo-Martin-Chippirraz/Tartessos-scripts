using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator EnviarExperiencia(ExperienciaRequest dto, Action<ExperienciaResponse> callback)
    {
        string url = ApiClient.baseURL + "/progresion/experiencia";
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if(req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error XP: " + req.error);
            callback(null);
            yield break;
        }

        ExperienciaResponse response = JsonUtility.FromJson<ExperienciaResponse>(req.downloadHandler.text);

        callback(response);
    }

    public IEnumerator DesbloquearHabilidad(HabilidadRequest dto, Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/progresion/habilidad";
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error habilidad: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }
}
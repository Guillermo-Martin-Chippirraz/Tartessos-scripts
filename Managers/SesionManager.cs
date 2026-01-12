using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SesionManager : MonoBehaviour
{
    public static SesionManager Instance;

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /* ============================================================
       CREAR SESIÓN
       ============================================================ */
    public IEnumerator CrearSesion(CrearSesionRequest dto, Action<CrearSesionResponse> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/sesiones";
        UnityWebRequest req = ApiClient.AuthPost(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error creando sesión: " + req.error);
            callback(null);
            yield break;
        }

        CrearSesionResponse response =
            JsonUtility.FromJson<CrearSesionResponse>(req.downloadHandler.text);

        callback(response);
    }

    /* ============================================================
       ACTUALIZAR SESIÓN
       ============================================================ */
    public IEnumerator ActualizarSesion(int idSesion, ActualizarSesionRequest dto, Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/sesiones/" + idSesion;
        UnityWebRequest req = ApiClient.AuthPut(url, dto);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error actualizando sesión: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }

    /* ============================================================
       OBTENER SESIÓN
       ============================================================ */
    public IEnumerator ObtenerSesion(int idSesion, Action<string> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/sesiones/" + idSesion;
        UnityWebRequest req = ApiClient.AuthGet(url);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Error obteniendo sesión: " + req.error);
            callback(null);
            yield break;
        }

        callback(req.downloadHandler.text);
    }
}

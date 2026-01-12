using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public string CombateId { get; private set; }
    public CombateDTO CombateActual { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator IniciarCombate(IniciarCombateRequest body, Action<CombateDTO> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/combate/iniciar";

        UnityWebRequest req = ApiClient.AuthPost(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al iniciar combate: " + req.error);
            callback(null);
            yield break;
        }

        CombateDTO combate = JsonUtility.FromJson<CombateDTO>(req.downloadHandler.text);
        CombateActual = combate;
        CombateId = combate._id;

        callback(combate);
    }

    public IEnumerator EnviarAccion(AccionRequest accion, Action<CombateDTO> callback)
    {
        if (string.IsNullOrEmpty(CombateId))
        {
            Debug.LogError("No hay combate activo");
            callback(null);
            yield break;
        }

        string url = ApiClient.baseURL + "/api/v1/combate/" + CombateId + "/accion";

        UnityWebRequest req = ApiClient.AuthPut(url, accion);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al enviar acción: " + req.error);
            callback(null);
            yield break;
        }

        CombateDTO combate = JsonUtility.FromJson<CombateDTO>(req.downloadHandler.text);
        CombateActual = combate;

        callback(combate);
    }

    public IEnumerator ObtenerCombate(Action<CombateDTO> callback)
    {
        if (string.IsNullOrEmpty(CombateId))
        {
            Debug.LogError("No hay combate activo");
            callback(null);
            yield break;
        }

        string url = ApiClient.baseURL + "/api/v1/combate/" + CombateId;

        UnityWebRequest req = ApiClient.AuthGet(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al obtener combate: " + req.error);
            callback(null);
            yield break;
        }

        CombateDTO combate = JsonUtility.FromJson<CombateDTO>(req.downloadHandler.text);
        CombateActual = combate;

        callback(combate);
    }

    public bool CombateFinalizado()
    {
        return CombateActual != null &&
               CombateActual.estado_actual != null &&
               CombateActual.estado_actual.fase == "finalizado";
    }
}
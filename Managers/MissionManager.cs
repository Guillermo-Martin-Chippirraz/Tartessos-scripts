using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    public List<MisionDTO> MisionesActivas = new List<MisionDTO>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator AceptarMision(AceptarMisionDTO dto, Action<MisionDTO> callback)
    {
        string url = ApiClient.baseURL + "/api/v1/misiones/aceptar";

        UnityWebRequest req = ApiClient.AuthPost(url, dto);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error al aceptar misión: " + req.error);
            callback?.Invoke(null);
            yield break;
        }

        MisionDTO mision = JsonUtility.FromJson<MisionDTO>(req.downloadHandler.text);
        MisionesActivas.Add(mision);
        callback?.Invoke(mision);
    }

    public IEnumerator CompletarObjetivo(string misionId, string objId, Action<MisionDTO> callback)
    {
        string url = ApiClient.baseURL + $"/api/v1/misiones/{misionId}/objetivos/{objId}";

        UnityWebRequest req = ApiClient.AuthPut(url, new EmptyBody());
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al completar objetivo: " + req.error);
            callback?.Invoke(null);
            yield break;
        }

        MisionDTO mision = JsonUtility.FromJson<MisionDTO>(req.downloadHandler.text);
        ActualizarCacheMision(mision);
        callback?.Invoke(mision);
    }

    public IEnumerator ActualizarProgreso(string misionId, ProgresoDTO dto, Action<MisionDTO> callback)
    {
        string url = ApiClient.baseURL + $"/api/v1/misiones/{misionId}/progreso";

        UnityWebRequest req = ApiClient.AuthPut(url, dto);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al actualizar progreso: " + req.error);
            callback?.Invoke(null);
            yield break;
        }

        MisionDTO mision = JsonUtility.FromJson<MisionDTO>(req.downloadHandler.text);
        ActualizarCacheMision(mision);
        callback?.Invoke(mision);
    }

    private void ActualizarCacheMision(MisionDTO misionActualizada)
    {
        int index = MisionesActivas.FindIndex(m => m._id == misionActualizada._id);
        if (index >= 0)
        {
            MisionesActivas[index] = misionActualizada;
        } else
        {
            MisionesActivas.Add(misionActualizada);
        }
    }

    [Serializable]
    private class EmptyBody {}
}
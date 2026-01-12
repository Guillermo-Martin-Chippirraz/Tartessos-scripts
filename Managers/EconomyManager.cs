using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /* ============================================================
       1. GANAR MONEDA
       ============================================================ */
    public IEnumerator Ganar(int idPartida, string moneda, int cantidad, System.Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/economia/ganar";

        var body = new GanarRequest
        {
            id_partida = idPartida,
            moneda = moneda,
            cantidad = cantidad
        };

        UnityWebRequest req = ApiClient.AuthPost(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al ganar moneda: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }

    /* ============================================================
       2. COMPRAR EN TIENDA
       ============================================================ */
    public IEnumerator Comprar(int idPartida, int idTienda, int idItem, int cantidad, string moneda, System.Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/economia/comprar";

        var body = new ComprarRequest
        {
            id_partida = idPartida,
            id_tienda = idTienda,
            id_item = idItem,
            cantidad = cantidad,
            moneda = moneda
        };

        UnityWebRequest req = ApiClient.AuthPost(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al comprar: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }

    /* ============================================================
       3. INTERCAMBIAR MONEDAS
       ============================================================ */
    public IEnumerator Intercambiar(int idPartida, string origen, string destino, int cantidad, float tasa, System.Action<bool, int> callback)
    {
        string url = ApiClient.baseURL + "/economia/intercambiar";

        var body = new IntercambiarRequest
        {
            id_partida = idPartida,
            origen = origen,
            destino = destino,
            cantidad = cantidad,
            tasa = tasa
        };

        UnityWebRequest req = ApiClient.AuthPost(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al intercambiar moneda: " + req.error);
            callback(false, 0);
            yield break;
        }

        var json = req.downloadHandler.text;
        var response = JsonUtility.FromJson<IntercambiarResponse>(json);

        callback(true, response.cantidadDestino);
    }

    /* ============================================================
       4. ACTUALIZAR SALDO (ADMIN/DEBUG)
       ============================================================ */
    public IEnumerator ActualizarSaldo(int idPartida, string moneda, int saldo, System.Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/economia/saldo";

        var body = new ActualizarSaldoRequest
        {
            id_partida = idPartida,
            moneda = moneda,
            saldo = saldo
        };

        UnityWebRequest req = ApiClient.AuthPut(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error al actualizar saldo: " + req.error);
            callback(false);
            yield break;
        }

        callback(true);
    }
}

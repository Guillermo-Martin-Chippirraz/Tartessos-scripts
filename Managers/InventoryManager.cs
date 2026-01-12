using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ItemData> Items = new List<ItemData>();
    public int idPartida;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator LoadInventory(int idPartida, System.Action<bool> callback)
    {
        this.idPartida = idPartida;
        
        string url = ApiClient.baseURL + "/inventario/" + idPartida;

        UnityWebRequest req = ApiClient.AuthGet(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error cargando inventario: " + req.error);
            callback(false);
            yield break;
        }

        var json = req.downloadHandler.text;
        var response = JsonUtility.FromJson<InventoryResponse>(json);

        Items = response.items;
        callback(true);
    }

    public IEnumerator AddItem(int idItem, int cantidad, System.Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/inventario/" + idPartida + "/add";

        var body = new
        {
            id_item = idItem,
            cantidad = cantidad
        };

        UnityWebRequest req = ApiClient.AuthPost(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error añadiendo ítem: " + req.error);
            callback(false);
            yield break;
        }

        yield return LoadInventory(idPartida, callback);
    }

    public IEnumerator UseItem(int idItem, System.Action<bool, ItemEffect> callback)
    {
        string url = ApiClient.baseURL + "/inventario/" + idPartida + "/use";

        var body = new
        {
            id_item = idItem
        };

        UnityWebRequest req = ApiClient.AuthPost(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error usando ítem: " + req.error);
            callback(false, null);
            yield break;
        }

        var json = req.downloadHandler.text;
        var response =  JsonUtility.FromJson<UseItemResponse>(json);

        yield return LoadInventory(idPartida, (ok) =>
        {
            callback(ok, response.efecto);
        });
    }

    public IEnumerator EquipItem(int idItem, bool equipado, System.Action<bool> callback)
    {
        string url = ApiClient.baseURL + "/inventario/" + idPartida + "/equip";

        var body = new
        {
            id_item = idItem,
            equipado = equipado
        };

        UnityWebRequest req = ApiClient.AuthPut(url, body);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error equipando ítem: " + req.error);
            callback(false);
            yield break;
        }

        yield return LoadInventory(idPartida, callback);
    }
}

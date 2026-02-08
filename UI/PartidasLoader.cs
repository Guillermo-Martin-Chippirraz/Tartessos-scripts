using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class PartidasLoader : MonoBehaviour
{
    public GameObject slotTemplate;
    public Transform contenedorSlots;

    public GameObject panelCargando;
    public TextMeshProUGUI textoCargando;

    public GameObject panelError;
    public TextMeshProUGUI textoError;

    private void OnEnable()
    {
        StartCoroutine(CargarSlots());
    }

    private IEnumerator CargarSlots()
    {
        panelError.SetActive(false);
        panelCargando.SetActive(true);
        textoCargando.text = "Cargando partidas...";

        foreach (Transform child in contenedorSlots)
            if (child != slotTemplate.transform) Destroy(child.gameObject);

        string token = PlayerPrefs.GetString("jwt_token", "");
        string url = ApiClient.baseURL + "/game/slots?singleSlot=false";

        UnityWebRequest req = UnityWebRequest.Get(url);
        req.SetRequestHeader("Authorization", "Bearer " + token);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            MostrarError("Error al obtener partidas");
            yield break;
        }

        SlotResponse response = JsonUtility.FromJson<SlotResponse>(req.downloadHandler.text);

        foreach (var slot in response.partidas)
        {
            GameObject go = Instantiate(slotTemplate, contenedorSlots);
            go.SetActive(true);
            go.GetComponent<SlotPartidaUI>().Configurar(slot);
        }

        panelCargando.SetActive(false);
    }

    private void MostrarError(string msg)
    {
        panelCargando.SetActive(false);
        panelError.SetActive(true);
        textoError.text = msg;
    }
}

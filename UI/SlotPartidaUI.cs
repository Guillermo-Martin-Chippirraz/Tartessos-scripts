using UnityEngine;
using TMPro;

public class SlotPartidaUI : MonoBehaviour
{
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoPersonaje;
    public TextMeshProUGUI textoUltimoGuardado;

    private int idPartida;

    public void Configurar(SlotPartida slot)
    {
        idPartida = slot.id_partida;

        textoTitulo.text = "Partida #" + slot.id_partida;
        textoPersonaje.text = slot.personaje_principal ?? "Sin personaje";
        textoUltimoGuardado.text = "Último guardado: " + slot.ultimo_guardado;
    }

    public void OnClick()
    {
        PartidaManager.Instance.CargarPartida(idPartida);
    }
}

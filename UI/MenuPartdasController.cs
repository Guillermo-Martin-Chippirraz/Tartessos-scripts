using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPartidasController : MonoBehaviour
{
    public RectTransform panelVistas;
    public float duracionDesliz = 0.3f;

    private float anchoPantalla;
    private Vector2 posMenu;
    private Vector2 posPartidas;
    private Vector2 posOpciones;
    private Coroutine animActual;

    private void Start()
    {
        // 🔥 1. Usar el ancho REAL de la pantalla
        anchoPantalla = Screen.width;

        // 🔥 2. Ajustar el tamaño del panel para que tenga EXACTAMENTE 3 pantallas de ancho
        panelVistas.sizeDelta = new Vector2(anchoPantalla * 3f, panelVistas.sizeDelta.y);

        // 🔥 3. Calcular posiciones correctas
        posMenu = Vector2.zero;
        posPartidas = new Vector2(-anchoPantalla, 0);
        posOpciones = new Vector2(-2f * anchoPantalla, 0);

        // 🔥 4. Asegurar que empezamos en la vista principal
        panelVistas.anchoredPosition = posMenu;
    }

    public void MostrarMenu() => MoverA(posMenu);
    public void MostrarPartidas() => MoverA(posPartidas);
    public void MostrarOpciones() => MoverA(posOpciones);

    private void MoverA(Vector2 destino)
    {
        if (animActual != null) StopCoroutine(animActual);
        animActual = StartCoroutine(Deslizar(destino));
    }

    private System.Collections.IEnumerator Deslizar(Vector2 destino)
    {
        Vector2 origen = panelVistas.anchoredPosition;
        float t = 0f;

        while (t < duracionDesliz)
        {
            t += Time.deltaTime;
            panelVistas.anchoredPosition = Vector2.Lerp(origen, destino, t / duracionDesliz);
            yield return null;
        }

        panelVistas.anchoredPosition = destino;
        animActual = null;
    }

    public void OnNuevaPartida()
    {
        PartidaManager.Instance.NuevaPartida();
    }

    public void OnVolverLogin()
    {
        SceneManager.LoadScene("Login");
    }
}

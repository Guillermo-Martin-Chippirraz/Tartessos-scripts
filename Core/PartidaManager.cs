using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PartidaManager : MonoBehaviour
{
    public static PartidaManager Instance;

    public string escenaIntro = "EscenaInicio";
    public string escenaJuego = "EscenaJuego";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void NuevaPartida()
    {
        StartCoroutine(CrearPartidaCoroutine());
    }

    private IEnumerator CrearPartidaCoroutine()
    {
        bool ok = false;
        ApiClient.CreatePartidaResponse resp = null;
        string err = null;

        yield return ApiClient.Instance.StartCoroutine(
            ApiClient.Instance.CreatePartida((success, r, e) =>
            {
                ok = success;
                resp = r;
                err = e;
            })
        );

        if (!ok)
        {
            Debug.LogError("Error creando partida: " + err);
            yield break;
        }

        PlayerPrefs.SetInt("id_partida", resp.id_partida);
        SceneManager.LoadScene(escenaIntro);
    }

    public void CargarPartida(int idPartida)
    {
        StartCoroutine(CargarPartidaCoroutine(idPartida));
    }

    private IEnumerator CargarPartidaCoroutine(int idPartida)
    {
        bool ok = false;
        GameStateResponse resp = null;
        string err = null;

        yield return ApiClient.Instance.StartCoroutine(
            ApiClient.Instance.CargarPartida(idPartida, (success, r, e) =>
            {
                ok = success;
                resp = r;
                err = e;
            })
        );

        if (!ok)
        {
            Debug.LogError("Error cargando partida: " + err);
            yield break;
        }

        GameState.Instance.CargarDesde(resp);

        PlayerPrefs.SetInt("id_partida", resp.partida.id_partida);
        SceneManager.LoadScene(escenaJuego);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BusSceneController : MonoBehaviour
{
    public DialogueSystem dialogo;
    public Transform cameraTransform;
    public Transform npcTransform;

    public DialogueLine lineaDespertar;
    public DialogueLine lineaFeo;
    public DialogueLine lineaFeoDisculpa;
    public DialogueLine lineaFeoContinuacion;
    public DialogueLine lineaSpeech;
    public DialogueLine lineaErnesto;

    public float giroDuracion = 1.2f;

    private void Start()
    {
        StartCoroutine(SecuenciaInicial());
    }

    private IEnumerator SecuenciaInicial()
    {
        yield return new WaitForSeconds(2f);

        yield return dialogo.ReproducirLinea(lineaDespertar);

        yield return StartCoroutine(GirarHaciaNPC());

        MostrarOpcionesIniciales();
    }

    private IEnumerator GirarHaciaNPC()
    {
        Quaternion inicio = cameraTransform.rotation;
        Quaternion destino = Quaternion.LookRotation(npcTransform.position - cameraTransform.position);

        float t = 0f;
        while (t < giroDuracion)
        {
            t += Time.deltaTime;
            cameraTransform.rotation = Quaternion.Slerp(inicio, destino, t / giroDuracion);
            yield return null;
        }
    }

    private void MostrarOpcionesIniciales()
    {
        dialogo.MostrarOpciones(new List<DialogueOption>
        {
            new DialogueOption("No te he oído bien, habla más alto, por favor.", () => StartCoroutine(RespuestaNormal())),
            new DialogueOption("(Ignorar)", () => StartCoroutine(RespuestaNormal())),
            new DialogueOption("Me cago en mi puta madre, ¡¿qué cojones es eso?!", () => StartCoroutine(RespuestaFeo())),
            new DialogueOption("Parece que el sueño se transformó en pesadilla...", () => StartCoroutine(RespuestaFeo()))
        });
    }

    private IEnumerator RespuestaFeo()
    {
        yield return dialogo.ReproducirLinea(lineaFeo);

        dialogo.MostrarOpciones(new List<DialogueOption>
        {
            new DialogueOption("No, para nada...", () => StartCoroutine(RespuestaFeoDisculpa())),
            new DialogueOption("Sí, demasiados.", () => StartCoroutine(RespuestaFeoContinuacion()))
        });
    }

    private IEnumerator RespuestaFeoDisculpa()
    {
        yield return dialogo.ReproducirLinea(lineaFeoDisculpa);
        yield return StartCoroutine(SpeechAcademia());
    }

    private IEnumerator RespuestaFeoContinuacion()
    {
        yield return dialogo.ReproducirLinea(lineaFeoContinuacion);
        yield return StartCoroutine(SpeechAcademia());
    }

    private IEnumerator RespuestaNormal()
    {
        yield return StartCoroutine(SpeechAcademia());
    }

    private IEnumerator SpeechAcademia()
    {
        yield return dialogo.ReproducirLinea(lineaSpeech);

        yield return new WaitForSeconds(1f);

        yield return dialogo.ReproducirLinea(lineaErnesto);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("EscenaEdicionPj");
    }

}

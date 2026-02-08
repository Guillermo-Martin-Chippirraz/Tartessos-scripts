using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public TMP_Text speakerName;
    public TMP_Text dialogueText;
    public Transform optionsContainer;
    public Button optionButtonPrefab;
    public AudioSource audioSource;

    public IEnumerator<WaitForSeconds> ReproducirLinea(DialogueLine linea)
    {
        speakerName.text = linea.speaker;
        dialogueText.text = linea.text;

        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);
        
        if (linea.audio != null)
        {
            audioSource.clip = linea.audio;
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
    }

    public void MostrarOpciones(List<DialogueOption> opciones)
    {

        optionsContainer.gameObject.SetActive(false);

        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);
        
        optionsContainer.gameObject.SetActive(true);

        foreach (var op in opciones)
        {
            var btn = Instantiate(optionButtonPrefab, optionsContainer);
            btn.GetComponentInChildren<TMP_Text>().text = op.texto;
            btn.onClick.AddListener(() =>
            {
                Debug.Log("BOTÓN PULSADO: " + op.texto);
                op.accion();
            });
        }
    }
}

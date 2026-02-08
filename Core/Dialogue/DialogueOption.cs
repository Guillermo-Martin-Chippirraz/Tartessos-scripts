using UnityEngine;

public class DialogueOption
{
    public string texto;
    public System.Action accion;

    public DialogueOption(string texto, System.Action accion)
    {
        this.texto = texto;
        this.accion = accion;
    }
}

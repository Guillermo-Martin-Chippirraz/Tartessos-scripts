using System;
using System.Collections.Generic;

[Serializable]
public class ObjetivoDTO
{
    public string id;
    public string descripcion;
    public bool completado;
}

[Serializable]
public class AceptarMisionDTO
{
    public string jugador_id;
    public string titulo;
    public List<ObjetivoDTO> objetivos;
}

[Serializable]
public class ObjetivoProgresoDTO
{
    public string id;
    public bool completado;
}

[Serializable]
public class ProgresoDTO
{
    public List<ObjetivoProgresoDTO> objetivos;
}

[Serializable]
public class MisionDTO
{
    public string _id;
    public string jugador_id;
    public string titulo;
    public string estado;
    public List<ObjetivoDTO> objetivos;
    public List<string> eventos_desbloqueados;
    public string fecha_aceptada;
}
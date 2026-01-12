using System;
using System.Collections.Generic;

[Serializable]
public class ExperienciaRequest
{
    public int id_usuario;
    public int xp_aventura;
    public int xp_personaje;
    public int id_personaje;
}

[Serializable]
public class ExperienciaAventuraResponse
{
    public int nivel_aventura;
    public int xp_aventura;
}

[Serializable]
public class ExperienciaPersonajeResponse
{
    public int nivel_personaje;
    public int xp_personaje;
}

[Serializable]
public class ExperienciaResponse
{
    public ExperienciaAventuraResponse aventura;
    public ExperienciaPersonajeResponse personaje;
}

[Serializable]
public class HabilidadRequest
{
    public int id_usuario;
    public int id_personaje;
    public string codigo_habilidad;
}

[Serializable]
public class LogroRequest
{
    public int id_usuario;
    public string codigo_logro;
    public int id_personaje;
}
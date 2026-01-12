using System;

[Serializable]
public class CrearSesionRequest {
    public int id_partida;
    public int jugadores_conectados;
}

[Serializable]
public class CrearSesionResponse
{
    public int id_sesion;
}

[Serializable]
public class ActualizarSesionRequest
{
    public int id_partida;
    public int jugadores_conectados;
}

[Serializable]
public class ChatRequest
{
    public int id_usuario;
    public string contenido;
    public string nombre_usuario;
}

[Serializable]
public class LogRedRequest
{
    public int id_sesion;
    public string evento;
}
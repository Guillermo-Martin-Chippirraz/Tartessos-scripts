using System;

[Serializable]
public class SlotPartida
{
    public int id_partida;
    public string personaje_principal;
    public string ultimo_logro;
    public string ultimo_guardado;
    public string snapshot_url;
}

[Serializable]
public class SlotResponse
{
    public SlotPartida[] partidas;
    public bool autoCreated;
}

[Serializable]
public class PartidaCargada
{
    public Partida partida;
    public PerfilData perfil;
    public PersonajeData[] personajes;
    public InventarioData inventario;
    public ItemData[] items;
    public MonederoData monedero;
    public MonedaData[] monedas;
}

[Serializable]
public class Partida
{
    public int id_partida;
    public string estado;
    public int nivel_aventura;
    public int nivel_mundo;
    public string fecha_inicio;
    public string ultimo_guardado;
    public string snapshot_url;
    public int id_monedero;
    public int id_inventario;
}

[Serializable]
public class PerfilData
{
    public int id_perfil;
    public string avatar;
    public string idioma;
    public bool modo_accesible;
    public int id_usuario;
    public int id_partida;
}

[Serializable]
public class PersonajeData
{
    public int id_personaje;
    public string nombre;
    public int nivel;
    public string raza;
    public string clase;
    public int experiencia;
    public bool activo_en_equipo;
}

[Serializable]
public class InventarioData
{
    public int id_inventario;
    public int cantidad;
}

[Serializable]
public class MonederoData
{
    public int id_monedero;
}

[Serializable]
public class MonedaData
{
    public int id_moneda;
    public string nombre;
    public int saldo;
}

[Serializable]
public class GameStateResponse
{
    public Partida partida;
    public PerfilData perfil;
    public PersonajeData[] personajes;
    public InventarioData inventario;
    public ItemData[] items;
    public MonederoData monedero;
    public MonedaData[] monedas;
}

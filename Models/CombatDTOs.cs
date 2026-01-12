using System;
using System.Collections.Generic;

[Serializable]
public class IniciarCombateRequest
{
    public string partida_id;
    public SnapshotEstadisticas snapshot_estadisticas;
    public List<EnemigoInicialDTO> enemigos;
}

[Serializable]
public class EnemigoInicialDTO
{
    public string id_enemigo;
    public string tipo;
    public int pv_max;
}

[Serializable]
public class SnapshotEstadisticas
{
    public List<PersonajeStatsDTO> personajes;
    public List<EnemigoStatsDTO> enemigos;
}

[Serializable]
public class PersonajeStatsDTO
{
    public string id_personaje;
    public string nombre;
    public int vida;
    public int mana;
    public int ataque;
    public int defensa;
    public int probCrit;
    public int damCrit;
}

[Serializable]
public class EnemigoStatsDTO
{
    public string id_enemigo;
    public int vida;
    public int fuerza;
    public int defensa;
}

[Serializable]
public class AccionRequest
{
    public string origen;
    public string tipo;
    public string nombre;
    public string objetivo;
    public string elemento;
    public PayloadDTO payload;
}

[Serializable]
public class PayloadDTO
{
    public bool trazo_correcto;
    public int daño;
}

[Serializable]
public class CombateDTO
{
    public string _id;
    public string partida_id;
    public string modo;
    public EstadoActualDTO estado_actual;
    public List<AccionLogDTO> acciones;
    public EventoFinalDTO evento_final;
}

[Serializable]
public class EstadoActualDTO
{
    public string fase;
    public bool agro;
    public string resultado;
    public List<PersonajeEstadoDTO> personajes;
    public List<EnemigoEstadoDTO> enemigos;
}

[Serializable]
public class PersonajeEstadoDTO
{
    public string id_personaje;
    public string nombre;
    public int pv_actual;
    public int pv_max;
    public int mana_actual;
    public int mana_max;
    public string estado;
    public List<string> buffs;
    public List<string> debuffs;
}

[Serializable]
public class EnemigoEstadoDTO
{
    public string id_enemigo;
    public string tipo;
    public int pv_actual;
    public int pv_max;
    public string estado;
    public bool agro;
    public List<string> buffs;
    public List<string> debuffs;
}

[Serializable]
public class AccionLogDTO
{
    public string timestamp;
    public string origen;
    public string tipo;
    public string nombre;
    public string objetivo;
    public ResultadoAccionDTO resultado;
}

[Serializable]
public class ResultadoAccionDTO
{
    public bool acierto;
    public int damage;
    public List<string> efectos;
}

[Serializable]
public class EventoFinalDTO
{
    public string tipo;
    public string motivo;
    public string timestamp;
}
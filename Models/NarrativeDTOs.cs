using System;
using System.Collections.Generic;

[Serializable]
public class EventoDTO {
    public string _id;
    public string nombre;
    public List<string> condiciones;
    public List<string> dialogo;
    public List<string> acciones;
}

[Serializable]
public class PreferenciasDTO
{
    public string _id;
    public UISettings ui;
    public SoundSettings sonido;
    public ControlSettings controles;
}

[Serializable]
public class UISettings
{
    public int brillo;
    public string tamaño_texto;
    public bool modo_daltonico;
}

[Serializable]
public class SoundSettings
{
    public int volumen_musica;
    public int volumen_efectos;
}

[Serializable]
public class ControlSettings
{
    public string tecla_accion;
    public string tecla_inventario;
}

[Serializable]
public class ConfiguracionSistemaDTO
{
    public int id_configuracion_sistema;
    public string graficos;
    public string idioma;
    public string controles_json;
}
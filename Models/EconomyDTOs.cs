using System;

[Serializable]
public class GanarRequest
{
    public int id_partida;
    public string moneda;
    public int cantidad;
}

[Serializable]
public class ComprarRequest
{
    public int id_partida;
    public int id_tienda;
    public int id_item;
    public int cantidad;
    public string moneda;
}

[Serializable]
public class IntercambiarRequest
{
    public int id_partida;
    public string origen;
    public string destino;
    public int cantidad;
    public float tasa;
}

[Serializable]
public class IntercambiarResponse
{
    public bool success;
    public int cantidadDestino;
}

[Serializable]
public class ActualizarSaldoRequest
{
    public int id_partida;
    public string moneda;
    public int saldo;
}

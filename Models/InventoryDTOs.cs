using System;
using System.Collections.Generic;

[Serializable]
public class InventoryResponse
{
    public Inventory inventario;
    public List<ItemData> items;
}

[Serializable]
public class Inventory
{
    public int id_inventario;
    public int cantidad;
    public int id_partida;
}

[Serializable]
public class ItemData
{
    public int id_item_inventario;
    public int id_item;
    public int cantidad;
    public bool equipado;

    public string nombre;
    public string descripcion;
    public string rareza;
    public string tipo_de_item;
    public float precio;
    public int precio_premium;
}

[Serializable]
public class UseItemResponse
{
    public string message;
    public ItemEffect efecto;
}

[Serializable]
public class ItemEffect
{
    public int PV;
    public int stamina;
    public int mana;
    public int atq;
    public int def;
}

[Serializable]
public class AddItemRequest
{
    public int id_item;
    public int cantidad;
}

[Serializable]
public class UseItemRequest
{
    public int id_item;
}

[Serializable]
public class EquipItemRequest
{
    public int id_item;
    public bool equipado;
}
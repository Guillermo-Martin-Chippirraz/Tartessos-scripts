using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public Partida partida;
    public PerfilData perfil;
    public PersonajeData[] personajes;
    public PersonajeData[] equipo;
    public InventarioData inventario;
    public ItemData[] items;
    public MonederoData monedero;
    public MonedaData[] monedas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void CargarDesde(GameStateResponse data)
    {
        partida = data.partida;
        perfil = data.perfil;
        personajes = data.personajes;
        inventario = data.inventario;
        items = data.items;
        monedero = data.monedero;
        monedas = data.monedas;

        equipo = System.Array.FindAll(personajes, p => p.activo_en_equipo);
    }
}

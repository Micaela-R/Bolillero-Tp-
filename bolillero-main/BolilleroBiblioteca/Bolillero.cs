namespace Biblioteca;
using System.Collections.Generic;

// Esta clase representa el bolillero del juego.
public class Bolillero : ICloneable
{
    public int Cantidad { get; private set; } // Guarda la cantidad total de bolillas, las bolillas que están dentro
    private List<int> _bolillas; // Lista que almacena las bolillas que actualmente están dentro del bolillero
    private List<int> _bolillasFuera; // Guarda la cantidad de bolillas que estan afuera qué bolilla sale. Además, puede clonarse para crear copias independientes.
    private IAzar _azar;

// Constructor para inicar el bolillero con cierta cantidad de bolillas y el tipo de azar que usará para sacar bolillas.
// Luego inicializa el bolillero dejando todo listo para hacer las listas 
    public Bolillero(int cantidad, IAzar azar)
    {
        Cantidad = cantidad; // Guarda en la propiedad Cantidad el número total de bolillas
        _azar = azar; // Guarda el tipo de azar que se usará para elegir qué bolilla sacar
        ReiniciarBolillero(); // Inicializa el bolillero colocando todas las bolillas dentro nuevamente
    }

    public int SacarBolilla()
    {
        int index = _azar.ObtenerIndice(_bolillas.Count);  // Elige qué bolilla va a salir
        int valor = _bolillas[index];  // Guarda el número de esa bolilla
        _bolillas.RemoveAt(index);  // La saca de las bolillas que todavía están adentro (nuk, sac)
        _bolillasFuera.Add(valor); // La manda a la lista de bolillas que ya salieron

        return valor; // Devuelve el numero que salio 
    }

    public void ReiniciarBolillero()
    {
        _bolillas = new List<int>();   // Crea de nuevo la lista de bolillas que van adentro
        _bolillasFuera = new List<int>();  // Vacía la lista de bolillas que ya habían salido

        for (int i = 0; i < Cantidad; i++)
        {
            _bolillas.Add(i); // Vuelve a meter todas las bolillas al bolillero (0,1,2,3...)

        }
    }

    public void ReingresarBolillas()
    {
        _bolillas.AddRange(_bolillasFuera); // Devuelve al bolillero todas las bolillas que habían salido (g, l, f)
        _bolillasFuera.Clear(); // Vacía la lista de bolillas que estaban afuera
        _bolillas.Sort(); 
    }

    public bool Jugar(List<int> jugada)  // (b, fs de partidas a)
    {
        ReingresarBolillas(); // Vuelve a meter todas las volillas al bolillero 
        foreach (var numero in jugada) // Recore cada numero que elijio el usuario 
        {
            if (SacarBolilla() != numero)
                return false;
        } // Si el numero que salio no coincide devuelve f y si concide devuekve v 
        return true;
    }

    public int JugarNVeces(List<int> jugada, int cantidad) // La cantidad de veces que se juega 
    {
        int aciertos = 0;
        for (int i = 0; i < cantidad; i++) // Si tenes un acierto suma uno 
        {
            if (Jugar(jugada)) aciertos++;
        }
        return aciertos; // Devuelve los acierto en total obtrnidos 
    }

    public int CantidadDentro() => _bolillas.Count;

    public int CantidadFuera() => _bolillasFuera.Count;
    public object Clone()
    {
        var clon = new Bolillero(Cantidad, _azar);    // Crea una copia nueva del bolillero con la misma cantidad y el mismo azar
        clon._bolillas = new List<int>(_bolillas); // copia bolillas que estan adentro 
        clon._bolillasFuera = new List<int>(_bolillasFuera); // copia las que ya salieron 
        return clon; 
    }
}
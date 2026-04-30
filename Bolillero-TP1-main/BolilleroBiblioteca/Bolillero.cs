namespace Biblioteca;
using System.Collections.Generic; // No te olvides de esto para las List

public class Bolillero
{
<<<<<<< HEAD
    public int Cantidad { get; private set; } //Te deja ver cuantas bolillas hay pero no deja cambiarlo porque es privado
    private List<int> bolillasDentro; //Se guardan las bolillas que estan adentro del bolillero
    private List<int> bolillasFuera; //Se guardan las bolillas que estan por fuera 
    private IAzar azar; // Elige a alguien para elegir una bolilla al azar 
=======
    public int Cantidad { get; private set; }
    private readonly int _cantidadInicial;
    private List<int> _bolillas = new();
    private List<int> bolillasFuera = new();
    private IAzar azar;
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4

    public Bolillero(int cantidad, IAzar azar) // Constructor
    {
<<<<<<< HEAD
        this.Cantidad = cantidad; // Guarda la cantidad de bolillas que hay 
        this.azar = azar; // Guarda quien va a elegir el azar 
        this.bolillasDentro = Enumerable.Range(0, cantidad).ToList(); // Guarda la cantidad de bolillas que hay dentro en una cadena  
        this.bolillasFuera = new List<int>(); // Se hace una lista vacia para poner las que ya salieron 
=======
        _cantidadInicial = cantidad;
        this.azar = azar; //  Usamos 'this' para asignar el atributo, sino no funciona porque el parámetro se llama igual que el atributo
        ReiniciarBolillero();
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4
    }

    public int SacarBolilla() // Devuelve numeros enteros
    {
<<<<<<< HEAD
        int index = azar.ObtenerIndice(bolillasDentro.Count); // Le pido al azar que me de una una posicion en una lista
        int valor = bolillasDentro[index]; // Se hace una lista donde se guarda la posicion en index y la guardo en valor 
        
        bolillasDentro.RemoveAt(index); //Elimina la bolilla que esta en esa posicion 
        bolillasFuera.Add(valor); // Se agrega la bolilla salida y se lo agrega en otra lista 
=======
        int index = azar.ObtenerIndice(_bolillas.Count);
        int valor = _bolillas[index];
        
        _bolillas.RemoveAt(index);
        bolillasFuera.Add(valor); 
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4
        
        return valor; //te devuelvo la bolilla que salio
    }

<<<<<<< HEAD
    public void ReingresarBolillas()// Se vuelven a meter todas las bolillas 
    {
        bolillasDentro.AddRange(bolillasFuera); // S e agarran todas de afuera y se meten todas juntas mas rapido
        bolillasFuera.Clear(); // Se vacia la lista de los que salieron
=======
    public void ReiniciarBolillero()
    {
        _bolillas = new List<int>();
        bolillasFuera = new List<int>(); 

        for (int i = 0; i < _cantidadInicial; i++)
        {
            _bolillas.Add(i);
        }
    }

    public void ReingresarBolillas()
    {
        _bolillas.AddRange(bolillasFuera);
        bolillasFuera.Clear();
        
        _bolillas.Sort();
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4
    }

    public bool Jugar(List<int> jugada) // Verdadero y falso 
    {
        ReingresarBolillas(); 
        foreach (var numero in jugada) // Se revisa uno por uno  los numeros que se jugo  
        {
            if (SacarBolilla() != numero) // Se saca una bolilla y se compara 
                return false; //Si perdiste devuelve esto 
        }
        return true; // Si ganaste devuelve esto
    }

    public int JugarNVeces(List<int> jugada, int cantidad) // La cantidad de veces que se hizo una jugada 
    {
        int aciertos = 0; // Se arranco con 0 victorias 
        for (int i = 0; i < cantidad; i++) // Se repite el juego la cantidad de veces que se dice 
        {
            if (this.Jugar(jugada)) aciertos++; // Si tenes aciertos se suma un punto 
        }
        return aciertos; // Devuelve cuantos aciertos tuviste
    }

<<<<<<< HEAD
    public int CantidadDentro() => bolillasDentro.Count;//Cantidad de bolillas que quedaron adentro 
=======
    

    public int CantidadDentro() => _bolillas.Count;
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4
    
    public int CantidadFuera() => bolillasFuera.Count;//Cantidad de bolillas que quedaron afuera
}
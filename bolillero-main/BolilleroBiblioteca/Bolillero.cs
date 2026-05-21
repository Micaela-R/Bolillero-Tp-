using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca
{
    public class Bolillero : ICloneable // Clase de bolillero para clonar 
    {
        private List<int> bolillasDentro; // Lista de bolillas que hay adentro
        private List<int> bolillasFuera; // Lista de bolillas que hay afuera
        private IAzar azar; // Se elije que azar se va a usar 
        private readonly object _lock = new object(); // Se usa para crear un objeto de bloqueo que sirve para sincronizar hilos

        public Bolillero(int cantidadBolillas, IAzar azar) // Constructor del bolillero
        {
            this.bolillasDentro = Enumerable.Range(0, cantidadBolillas).ToList();
            this.bolillasFuera = new List<int>();
            this.azar = azar;
        }

        private Bolillero(List<int> bolillasDentro, List<int> bolillasFuera, IAzar azar)
        {
            this.bolillasDentro = new List<int>(bolillasDentro); // Crea una copia de las bolillas de adentro en lista
            this.bolillasFuera = new List<int>(bolillasFuera); // Crea una copia de las bolillas que salieron
            this.azar = azar; // Guarda el objeto azar que salio 
        }

        public int SacarBolilla() // 
        {
            int indice = azar.ObtenerIndice(bolillasDentro.Count); // Pide un numero aleatorio de bolillas dentro
            int bolilla = bolillasDentro[indice]; // Busca el valor que esta en esa posicion

            bolillasDentro.RemoveAt(indice); // Elimina la bolilla de la lista de adentro
            bolillasFuera.Add(bolilla); // Para agregarla en la lista de afuera

            return bolilla; // Devuelve la bolilla
        }

        public void ReingresarBolillas() // Vuelve a meter todas las bolillas adentro nuevamente
        {
            bolillasDentro.AddRange(bolillasFuera); // Se agrega de una lista a la otra 
            bolillasFuera.Clear(); // Limpia las bolillas de afuera 
        }

        public int CantidadDentro() => bolillasDentro.Count; // Csntidad e bolillas adentro 

        public int CantidadFuera() => bolillasFuera.Count; // Cantidad de bolillas afuera

        public bool Jugar(List<int> jugada)
        {
            foreach (var numero in jugada) // Recorre la jugada 
            {
                if (SacarBolilla() != numero) // Saca una bolilla y la compara 
                    return false; // Si sale mal devuelve falso 
            }
            return true; // Verdadero 
        }

        
    
        public int JugarNVeces(List<int> jugada, int cantidad) // La cantidad de veces que se puede jugar
        {
            int victorias = 0;
            for (int i = 0; i < cantidad; i++)
            {
                lock (_lock)
                {
                    ReingresarBolillas();
                    if (Jugar(jugada))
                        victorias++;
                }
            }
            return victorias;
        }

        public object Clone()
        {
            return new Bolillero(this.bolillasDentro, this.bolillasFuera, this.azar); 
        }
    }
}

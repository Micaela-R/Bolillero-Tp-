using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca
{
    public class Bolillero : ICloneable
    {
        private List<int> bolillasDentro;
        private List<int> bolillasFuera;
        private IAzar azar;
        private readonly object _lock = new object();

        public Bolillero(int cantidadBolillas, IAzar azar)
        {
            this.bolillasDentro = Enumerable.Range(0, cantidadBolillas).ToList();
            this.bolillasFuera = new List<int>();
            this.azar = azar;
        }

        private Bolillero(List<int> bolillasDentro, List<int> bolillasFuera, IAzar azar)
        {
            this.bolillasDentro = new List<int>(bolillasDentro);
            this.bolillasFuera = new List<int>(bolillasFuera);
            this.azar = azar;
        }

        public int SacarBolilla()
        {
            int indice = azar.ObtenerIndice(bolillasDentro.Count);
            int bolilla = bolillasDentro[indice];

            bolillasDentro.RemoveAt(indice);
            bolillasFuera.Add(bolilla);

            return bolilla;
        }

        public void ReingresarBolillas()
        {
            bolillasDentro.AddRange(bolillasFuera);
            bolillasFuera.Clear();
        }

        public int CantidadDentro() => bolillasDentro.Count;

        public int CantidadFuera() => bolillasFuera.Count;

        public bool Jugar(List<int> jugada)
        {
            foreach (var numero in jugada)
            {
                if (SacarBolilla() != numero)
                    return false;
            }
            return true;
        }

        // El lock garantiza que cada hilo ejecuta su ronda completa (reingresar + jugar)
        // de forma atómica, sin que otro hilo interfiera con el estado del bolillero.
        public int JugarNVeces(List<int> jugada, int cantidad)
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

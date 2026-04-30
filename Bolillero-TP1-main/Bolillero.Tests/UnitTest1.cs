using Biblioteca;
using System.Collections.Generic;
using Xunit;

namespace TestBolilleros
{
    public class UnitTest1 
    {


        [Fact]
        public void Prueba_SacarBolilla_AzarFijo()
        {
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0)); //Crea el bolillero, con una instancia donde azar fijo siempre devuelve 0
            
            int resultado = miBolillero.SacarBolilla(); //Devuelve una bolilla que se guarda en resultado. Ya q es azar fijo, siempre devuelve 0
            
            Assert.Equal(0, resultado); // Verifica que el resultado de 0
        }

        [Fact]
        public void Prueba_Jugada_Ganadora()
        {
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0)); //Crea el obejto bolillero que se guarda en "miBolillero", devuelve indice 0 y tiene un azar controlado
            
            List<int> jugada = new List<int> { 0 }; //Hace una lista que representa la jugada del jugador

            bool gano = miBolillero.Jugar(jugada); //Devuelve V o F, compara la jugada con la del bolillero 

            Assert.True(gano); //Si coincide, gano la jugada 
        }

        [Fact]
        public void Prueba_Jugada_Perdedora()
        {
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0)); //Crea el obejto bolillero que se guarda en "miBolillero", devuelve indice 0 y tiene un azar controlado
            
            List<int> jugada = new List<int> { 5 }; //Define la jugada del jugador 

            bool gano = miBolillero.Jugar(jugada); //El bbolillero saca una bolilla, como es azar fijo sale 0, por lo que se compara con el 5 
        
            Assert.False(gano); //No coincide, por lo que verifica que el resultado es falso
        }

        [Fact]
        public void Prueba_VolverAColocarBolillas()
        {
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0)); 
            miBolillero.SacarBolilla(); //Elimina una bolilla, por lo que queda una menos 
            miBolillero.ReiniciarBolillero(); //Reinicia el bolillero, esto carga todas las bolillas dejandolas como en el inicio

            int bolilla = miBolillero.SacarBolilla(); //Vuelve a sacar una bolilla, si se reinicio correctamente, la bolilla 0 debe volver a existir
            Assert.Equal(0, bolilla); //Verifica el resultado
        }

        [Fact]
        public void Prueba_JugarNVeces_Bolillero()
        {
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0)); 
            List<int> jugada = new List<int> { 0 }; //Definbe la jugada, como el azar siempre es 0, entonces es una jugada ganadora 
            long aciertos = miBolillero.JugarNVeces(jugada, 5); //Guarda las cantidades de juego en "aciertos", esto devuelve cunatas veces ganaste 

            Assert.Equal(5, aciertos); //Verifica el resultado
        }
    }
}
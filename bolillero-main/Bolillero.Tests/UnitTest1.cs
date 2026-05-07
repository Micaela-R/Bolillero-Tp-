using Biblioteca;
using System.Collections.Generic;
using Xunit;

namespace TestBolilleros
{
    public class UnitTest1
    {

        [Fact]
        public void SacarBolilla_DevuelveBolillaCero_YActualizaCantidades() //Se crea el objeto tomando en cuenta la clase "Boliullero", esto verifica las bolillas fuera y dentro del bolillero
        {
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));

            int resultado = miBolillero.SacarBolilla();

            Assert.Equal(0, resultado);
            Assert.Equal(9, miBolillero.CantidadDentro());
            Assert.Equal(1, miBolillero.CantidadFuera());
        }

        [Fact]
        public void ReingresarBolillas_DespuesDeSacarUna_VuelveA10() 
        {
            //Se reingresan las bolillas, verificando que la cantidad dentro sea la inicial 
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));

            miBolillero.SacarBolilla();
            miBolillero.ReingresarBolillas();

            Assert.Equal(10, miBolillero.CantidadDentro());
            Assert.Equal(0, miBolillero.CantidadFuera());
        }

        [Fact]
        public void Jugar_JugadaGanadora_0123_Gana()
        {
            //Se verifica la jugada ganadora, verificando que la jugada seleccionada sea igual a las bolillas sacadas por el bolillero
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));
            List<int> jugada = new List<int> { 0, 1, 2, 3 };

            bool gano = miBolillero.Jugar(jugada);

            Assert.True(gano);
        }

        [Fact]
        public void Jugar_JugadaPerdedora_421_Pierde()
        {
            //Se verifica la jugada, en este caso; la jugada no coincide con las bolillas sacadas por el bolillero
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));
            List<int> jugada = new List<int> { 4, 2, 1 };

            bool gano = miBolillero.Jugar(jugada);

            Assert.False(gano);
        }

        [Fact]
        public void JugarNVeces_Jugada01_1Vez_Gana1Vez()
        {
            //Se verifican la cantidad de veces que se jugo en el bolillero
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));
            List<int> jugada = new List<int> { 0, 1 };

            int aciertos = miBolillero.JugarNVeces(jugada, 1);

            Assert.Equal(1, aciertos);
        }


        [Fact]
        public void SimularSinHilos_JugadaSegura_DevuelveNAciertos()
        {
            //Se ve la cantidad de veces que se simulo la jugada, en este caso son 100. Hace una simulacion por vez 
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));
            Simulacion sim = new Simulacion(miBolillero);
            List<int> jugada = new List<int> { 0, 1, 2 };

            long aciertos = sim.SimularSinHilos(jugada, 100);

            Assert.Equal(100, aciertos);
        }

        [Fact]
        public void SimularConHilos_JugadaSegura_DevuelveNAciertos()
        {
            //Se hace una simulacion con hilis, lo que permite hacer varias simulaciones al mismo tiempo. 
            Bolillero miBolillero = new Bolillero(10, new AzarFijo(0));
            Simulacion sim = new Simulacion(miBolillero);
            List<int> jugada = new List<int> { 0, 1, 2 };

            long aciertos = sim.SimularConHilos(jugada, 100, 4);

            Assert.Equal(100, aciertos);
        }

        [Fact]
        public void Clone_BolilleroClonado_EsIndependienteDelOriginal()
        {
            //Se hace un clon del bolillero, donde modifica este mismo pero el original queda igual 
            Bolillero original = new Bolillero(10, new AzarFijo(0));
            Bolillero clon = (Bolillero)original.Clone();

            clon.SacarBolilla();

            Assert.Equal(10, original.CantidadDentro());
            Assert.Equal(9, clon.CantidadDentro());
        }
    }
}
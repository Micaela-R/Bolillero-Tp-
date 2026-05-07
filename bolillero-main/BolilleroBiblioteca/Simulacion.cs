namespace Biblioteca;
// Ejecuta varias simulaciones de una jugada y devuelve la cantidad de aciertos.
public class Simulacion
{
    private Bolillero _bolillero;

    public Simulacion(Bolillero bolillero) => _bolillero = bolillero;
    public long SimularSinHilos(List<int> jugada, int cantidadVeces)
    {
        long aciertos = 0;
        for (int i = 0; i < cantidadVeces; i++) //buble de cantifdad de jugadas , simulacion extra
        {
            var clon = (Bolillero)_bolillero.Clone(); // Usarías siempre el mismo bolillero.
            if (clon.Jugar(jugada)) // Nunca verificaría si ganó o perdió. El método perdería sentido.
                aciertos++;
        }
        return aciertos;
    }

// Divide las simulaciones entre varios hilos para ejecutar partidas al mismo tiempo,
// cada hilo juega una cantidad de veces, cuenta sus aciertos
// y así el proceso termina más rápido.
    public long SimularConHilos(List<int> jugada, int cantidadVeces, int cantidadHilos)
    {
        int jugadasPorHilo = cantidadVeces / cantidadHilos;
        int resto = cantidadVeces % cantidadHilos;

        var tareas = new Task<long>[cantidadHilos]; // task para hacer las tareas en pararlelo 

        for (int i = 0; i < cantidadHilos; i++)
        {
            int jugadasDeEsteHilo = jugadasPorHilo + (i == cantidadHilos - 1 ? resto : 0);

            tareas[i] = Task.Run(() => // ya no abria paralelismo
            {
                long aciertosLocales = 0;
                for (int j = 0; j < jugadasDeEsteHilo; j++)
                {
                    var clon = (Bolillero)_bolillero.Clone(); //hilos bolilleros 
                    if (clon.Jugar(jugada))
                        aciertosLocales++;
                }
                return aciertosLocales;
            });
        }

        Task.WaitAll(tareas);

        long totalAciertos = 0;
        foreach (var tarea in tareas)
            totalAciertos += tarea.Result;

        return totalAciertos;
    }
}
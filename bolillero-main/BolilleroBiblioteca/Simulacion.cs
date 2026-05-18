namespace Biblioteca;

public class Simulacion
{
    private Bolillero _bolillero;

    public Simulacion(Bolillero bolillero) => _bolillero = bolillero;

    // Simula jugando n veces de forma secuencial, sin hilos.
    // Aprovecha JugarNVeces sobre un único clon para toda la simulación.
    public long SimularSinHilos(List<int> jugada, int cantidadVeces)
    {
        var clon = (Bolillero)_bolillero.Clone();
        return clon.JugarNVeces(jugada, cantidadVeces);
    }

    // Simula jugando n veces repartiendo el trabajo en varios hilos (síncrono).
    // Cada hilo recibe su propio clon y llama a JugarNVeces con su porción de trabajo.
    public long SimularConHilos(List<int> jugada, int cantidadVeces, int cantidadHilos)
    {
        int jugadasPorHilo = cantidadVeces / cantidadHilos;
        int resto = cantidadVeces % cantidadHilos;

        var tareas = new Task<long>[cantidadHilos];

        for (int i = 0; i < cantidadHilos; i++)
        {
            // El último hilo absorbe el resto si la división no es exacta
            int jugadasDeEsteHilo = jugadasPorHilo + (i == cantidadHilos - 1 ? resto : 0);

            // Capturamos el clon fuera del lambda para evitar closures problemáticas
            var clon = (Bolillero)_bolillero.Clone();
            tareas[i] = Task.Run(() => (long)clon.JugarNVeces(jugada, jugadasDeEsteHilo));
        }

        Task.WaitAll(tareas);

        return tareas.Sum(t => t.Result);
    }

    // Simula jugando n veces con async/await, repartiendo el trabajo en varios hilos.
    // Cada hilo recibe su propio clon y llama a JugarNVeces con su porción de trabajo.
    public async Task<long> SimularConHilosAsync(List<int> jugada, int cantidadVeces, int cantidadHilos)
    {
        int jugadasPorHilo = cantidadVeces / cantidadHilos;
        int resto = cantidadVeces % cantidadHilos;

        var tareas = new Task<long>[cantidadHilos];

        for (int i = 0; i < cantidadHilos; i++)
        {
            // El último hilo absorbe el resto si la división no es exacta
            int jugadasDeEsteHilo = jugadasPorHilo + (i == cantidadHilos - 1 ? resto : 0);

            // Cada hilo trabaja con su propio clon, sin concurrencia sobre el bolillero
            var clon = (Bolillero)_bolillero.Clone();
            tareas[i] = Task.Run(() => (long)clon.JugarNVeces(jugada, jugadasDeEsteHilo));
        }

        // Await asíncrono de todas las tareas y suma de resultados
        long[] resultados = await Task.WhenAll(tareas);

        return resultados.Sum();
    }
}

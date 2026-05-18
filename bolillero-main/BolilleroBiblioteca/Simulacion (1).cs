namespace Biblioteca;

public class Simulacion
{
    private Bolillero _bolillero;

    public Simulacion(Bolillero bolillero) => _bolillero = bolillero;

    // Simula jugando n veces de forma secuencial, sin hilos.
    public long SimularSinHilos(List<int> jugada, int cantidadVeces)
    {
        var clon = (Bolillero)_bolillero.Clone();
        return clon.JugarNVeces(jugada, cantidadVeces);
    }

    // Simula jugando n veces repartiendo el trabajo en varios hilos (síncrono).
    public long SimularConHilos(List<int> jugada, int cantidadVeces, int cantidadHilos)
    {
        int jugadasPorHilo = cantidadVeces / cantidadHilos;
        int resto = cantidadVeces % cantidadHilos;

        var tareas = new Task<long>[cantidadHilos];

        for (int i = 0; i < cantidadHilos; i++)
        {
            int jugadasDeEsteHilo = jugadasPorHilo + (i == cantidadHilos - 1 ? resto : 0);
            var clon = (Bolillero)_bolillero.Clone();
            tareas[i] = Task.Run(() => (long)clon.JugarNVeces(jugada, jugadasDeEsteHilo));
        }

        Task.WaitAll(tareas);
        return tareas.Sum(t => t.Result);
    }

    // Simula jugando n veces con async/await, llamando a JugarNVeces directamente
    // sobre el bolillero original. El lock dentro de JugarNVeces garantiza
    // que cada ronda (reingresar + jugar) sea atómica entre los hilos concurrentes.
    public async Task<long> SimularConHilosAsync(List<int> jugada, int cantidadVeces, int cantidadHilos)
    {
        int jugadasPorHilo = cantidadVeces / cantidadHilos;
        int resto = cantidadVeces % cantidadHilos;

        var tareas = new Task<long>[cantidadHilos];

        for (int i = 0; i < cantidadHilos; i++)
        {
            int jugadasDeEsteHilo = jugadasPorHilo + (i == cantidadHilos - 1 ? resto : 0);

            // Todos los hilos comparten _bolillero directamente (sin clonar).
            // El lock en JugarNVeces protege el estado compartido.
            tareas[i] = Task.Run(() => (long)_bolillero.JugarNVeces(jugada, jugadasDeEsteHilo));
        }

        long[] resultados = await Task.WhenAll(tareas);
        return resultados.Sum();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca;

public class Simulacion //Simula un bolillero,guarda,saca al azar, no los repite y permite ver si ganas
{
    private Bolillero bolillero; // Tengo un bolillero guardado para usarlo

    public Simulacion(Bolillero bolillero) 
    {
        this.bolillero = bolillero; //Cuando se crea me pasa un bolillero y lo guardo 
    }

    public int JugarNVeces(List<int> jugada, int cantidadVeces) //Se juega varias veces y dice el resultado 
    {
        int aciertos = 0; // Empieza con 0 aciertos 
        for (int i = 0; i < cantidadVeces; i++) //Repito el juego varias veces 
        {
            if (bolillero.Jugar(jugada)) // Si se gana una jugada se suna un acierto
            {
                aciertos++;

            }
        }
        return aciertos; //Devuelve cuantos aciertos tuve
    }
<<<<<<< HEAD
    public async Task<int> SimularAsincronico(List<int> jugada, int cantidadVeces) // Se simula muchas veces el juego mas rapido 
    {
    
        List<Task<int>> tareas = new List<Task<int>>(); // Una lista para guardar varias tareas y Task<int> trabaja en devolver el numero
=======

    public async Task<int> SimularAsincronico(List<int> jugada, int cantidadVeces)
    {
        // 1. Armamos una lista para ir guardando todas las simulaciones, 
        List<Task<int>> tareas = new List<Task<int>>();
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4

        for (int i = 0; i < cantidadVeces; i++) // Un bucle para repetir varias veces tal cosa
        {
            tareas.Add(Task.Run(() => // Se trabaja en paralelo y lo guarda despues
            {
<<<<<<< HEAD
        
                var bolilleroLocal = new Bolillero(bolillero.Cantidad, new AzarRandom());  // Cada tarea necesita su propio bolillero para no mezclar sus bolillas con las de otra tarea
=======
                var bolilleroLocal = new Bolillero(bolillero.Cantidad, new AzarRandom()); 
>>>>>>> e1b64a73119456d4030e7c905149bf970eed58a4

                if (bolilleroLocal.Jugar(jugada)) // Juego y si gano se suma 1 si no 0
                {
                    return 1; 
                }
                return 0; 
            }));
        }


        int[] resultados = await Task.WhenAll(tareas); // Se esperan a que terminen todas las simulaciones y devuelven todo junto 


        int totalAciertos = 0; // Empieza con 0 aciertos
        foreach (int resultado in resultados) // 
        {
            totalAciertos += resultado; // Seven todos los resultados y se suman 
        }

        return totalAciertos; // Cuantos aciertos en total tuve
    }
}

// Usamos la simulacion de un bolillero para jugar muchas veces, tienen una verson normal que jugo uno por uno y otra asincronica que juega todo el tiempo para ir mas rapido
// cada simulacion usa su propio bolillero para no omezclar datos y usamos async para ejecutar varias simulaciones al mismo tiempo y hacero mas rapido 
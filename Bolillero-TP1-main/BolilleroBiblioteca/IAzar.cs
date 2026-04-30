using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca;


namespace Biblioteca;

public interface IAzar //Genera una interfaz
{
    int ObtenerIndice(int max);//Metodo sin implementacion que recibe un parametro
}

public class AzarRandom : IAzar //Implementa la interfaz "IAzar"
{
    private Random random = new Random();//Declara la variable privada
    public int ObtenerIndice(int max) => random.Next(max);//Implementa la interfaz, ramdom next devuelve un numero 0 y max
}

public class AzarFijo : IAzar
{
    private int indiceFijo;
    public AzarFijo(int indice) => this.indiceFijo = indice;//Crea un obejeto, donde se le dara un valor que se guardara en "indice"
    public int ObtenerIndice(int max) => indiceFijo;//Siempre devuelve el mismo numero 
}
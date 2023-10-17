using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

enum Cellak
{
    Fukezdeny,
    ZsengeFu,
    KifejlettFucsomo,
    Nyul,
    Roka,
    Ures
}

class Entitas
{
    public int Jollakottsag { get; set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Entitas(int x, int y)
    {
        X = x;
        Y = y;
        Jollakottsag = 5;
    }

    public void Mozgas(int newX, int newY)
    {
        X = newX;
        Y = newY;
    }

    public void Eszik(int tapertek)
    {
        Jollakottsag += tapertek;
    }
}
class Program
{
    static int szelesseg = 7;
    static int magassag = 5;
    static Cellak[,] grid = new Cellak[szelesseg, magassag];
    static List<Entitas> nyulak = new List<Entitas>();
    static List<Entitas> rokak = new List<Entitas>();
    static Random random = new Random();

}
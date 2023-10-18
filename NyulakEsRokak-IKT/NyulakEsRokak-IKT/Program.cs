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

    static void Main()
    {
        Inicializalas();
        while (true)
        {
            Console.Clear();
            Racsrendszer();
            Frissites();
            Thread.Sleep(6000);
        }
    }

    //Entitások hozzáadása
    static void Inicializalas()
    {
        for (int x = 0; x < szelesseg; x++)
        {
            for (int y = 0; y < magassag; y++)
            {
                grid[x, y] = Cellak.Fukezdeny;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            nyulak.Add(new Entitas(random.Next(szelesseg), random.Next(magassag)));
        }

        for (int i = 0; i < 2; i++)
        {
            rokak.Add(new Entitas(random.Next(szelesseg), random.Next(magassag)));
        }
    }

    static void Racsrendszer()
    {
        for (int y = 0; y < magassag; y++)
        {
            for (int x = 0; x < szelesseg; x++)
            {
                char cellChar;
                switch (grid[x, y])
                {
                    case Cellak.Fukezdeny:
                        cellChar = 'F';
                        break;
                    case Cellak.ZsengeFu:
                        cellChar = 'Z';
                        break;
                    case Cellak.KifejlettFucsomo:
                        cellChar = 'K';
                        break;
                    case Cellak.Nyul:
                        cellChar = 'N';
                        break;
                    case Cellak.Roka:
                        cellChar = 'R';
                        break;
                    case Cellak.Ures:
                        cellChar = '-';
                        break;
                    default:
                        cellChar = '?';
                        break;
                }
                Console.Write(cellChar + "\t");
            }
            Console.WriteLine();
        }
    }

    static void Frissites()
    {
        List<Entitas> ujNyula = new List<Entitas>();
        List<Entitas> ujRoka = new List<Entitas>();

        foreach (var nyul in nyulak)
        {
            NyulLep(nyul, ujNyula);
        }

        foreach (var roka in rokak)
        {
            RokaLep(roka, ujRoka);
        }

        nyulak.AddRange(ujNyula);
        rokak.AddRange(ujRoka);

        // Az élőlények éheznek!
        foreach (var nyul in nyulak)
        {
            nyul.Jollakottsag--;
        }

        foreach (var roka in rokak)
        {
            roka.Jollakottsag--;
        }

        // Entitások irtása, ha a jóllakottsági szintjük 0 vagy annál kisebb
        nyulak.RemoveAll(n => n.Jollakottsag <= 0);
        rokak.RemoveAll(r => r.Jollakottsag <= 0);

        // Új entitások elhelyezése a rácsmezőben
        grid = new Cellak[szelesseg, magassag];
        foreach (var nyul in nyulak)
        {
            grid[nyul.X, nyul.Y] = Cellak.Nyul;
        }

        foreach (var roka in rokak)
        {
            grid[roka.X, roka.Y] = Cellak.Roka;
        }

        // Fű növekedése -- nem megfelelő működés, kifejlett fű nem lesz belőle (segítség kérés!)
        for (int x = 0; x < szelesseg; x++)
        {
            for (int y = 0; y < magassag; y++)
            {
                switch (grid[x, y])
                {
                    case Cellak.Fukezdeny:
                        grid[x, y] = Cellak.ZsengeFu;
                        break;
                    case Cellak.ZsengeFu:
                        grid[x, y] = Cellak.KifejlettFucsomo;
                        break;
                    case Cellak.KifejlettFucsomo:
                        grid[x, y] = Cellak.Fukezdeny;
                        break;
                }
            }
        }
    } 
}
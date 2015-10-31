using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace CommunityGaming
{
    class Program
    {
        
        static void Main(string[] args)
        {
            WarriorGame wariorGame;
            wariorGame = new WarriorGame();
            Thread thread = new Thread(new ThreadStart(() => wariorGame.startServer()));
                    thread.Start();
                    
           while ((true))
           {
                 Console.ReadLine();
                 Console.WriteLine("Juz wyswietlam...");
                 for (int i = 0; i < wariorGame.communicationUnits.Count(); i++)
                 {
                     Console.WriteLine("Client:{0}\nmovement{1}",i,wariorGame.communicationUnits[i].movement);
                 }
           }
            
            
        }
    }
}

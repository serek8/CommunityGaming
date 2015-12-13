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
                 //Console.WriteLine("Nacisnij ENTER zeby sprawdzic wariorGame.communicationUnits[i].movement");
                 Console.ReadLine();
                 wariorGame.communicationUnits[0].writeCodeToStream(3);
                //  for (int i = 0; i < wariorGame.communicationUnits.Count(); i++)
                //  {
                //      if (!wariorGame.communicationUnits[i].isConnected)
                //      {
                //          Console.WriteLine("Client:{0} is disconnected",i);
                //          continue;
                //      }
                     
                //      Console.WriteLine("Client:{0}\tmovement: {1}",i,wariorGame.communicationUnits[i].movement);
                //      Console.WriteLine("Client:{0}\trotation: {1}",i,wariorGame.communicationUnits[i].rotation);
                //      Console.WriteLine("Client:{0}\taction: {1}",i,wariorGame.communicationUnits[i].action);                         
                                              
                //  }
           }  
        }
    }
}

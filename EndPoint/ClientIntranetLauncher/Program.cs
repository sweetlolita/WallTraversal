using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;
using WallTraversal.EndPoint.ClientIntranet;

namespace WallTraversal.EndPoint.ClientIntranet.Launcher
{
    class Program : WallTraversalClientIntranetObserver
    {
        static void Main(string[] args)
        {
            Logger.enable();
            Bundle bundle = new Bundle(
                new Guid("CFF9B0CA-F2BA-4088-A326-702A6DF92B93"),
                "10.148.219.165", 0,
                "10.148.219.165", 8334,
                new Program());
            bundle.start();
            ConsoleKeyInfo ch;

            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    //case ConsoleKey.R:
                    //    {
                    //        bundle.register(guid);
                    //        break;
                    //    }
                    case ConsoleKey.C:
                        {
                            bundle.start();
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            bundle.stop();
                            break;
                        }
                    default:
                        break;
                }
            } while (ch.Key != ConsoleKey.Q);
            bundle.stop();
        }

        void WallTraversalClientIntranetObserver.onAppData(string appData)
        {
            Logger.debug("Program: on app data: {0}", appData);
        }

        void WallTraversalClientIntranetObserver.onStarted()
        {
            
        }

        void WallTraversalClientIntranetObserver.onStopped()
        {
            
        }
    }
}

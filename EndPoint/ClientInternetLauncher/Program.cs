using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;
using WallTraversal.EndPoint.ClientInternet;

namespace WallTraversal.EndPoint.ClientInternet.Launcher
{
    class Program : WallTraversalClientInternetObserver
    {
        static void Main(string[] args)
        {
            Logger.enable();
            Bundle bundle = new Bundle(
                new Guid("CFF9B0CA-F2BA-4088-A326-702A6DF92B93"),
                "10.31.31.31", 0,
                "10.0.0.2", 8334,
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
                    case ConsoleKey.S:
                        {
                            bundle.send("a test string.");
                            break;
                        }
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

        void WallTraversalClientInternetObserver.onStarted()
        {
            
        }

        void WallTraversalClientInternetObserver.onStopped()
        {
            
        }
    }
}

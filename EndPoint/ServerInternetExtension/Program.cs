using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;

namespace WallTraversal.EndPoint.ServerInternetExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            GprsTransmitter bundle = new GprsTransmitter(
                "10.31.31.31", 8332,
                "10.31.31.31", 8334);
            bundle.start();
            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.R:
                        {
                            bundle.register();
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
    }
}

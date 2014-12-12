using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;

namespace WallTraversal.EndPoint.ClientIntranet
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            Bundle bundle = new Bundle(
                "10.148.219.165", 8333,
                "10.148.219.165", 8334);
            bundle.start();
            Guid guid = new Guid("CFF9B0CA-F2BA-4088-A326-702A6DF92B93");
            ConsoleKeyInfo ch;

            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.R:
                        {
                            bundle.register(guid);
                            break;
                        }
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
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;

namespace WallTraversal.EndPoint.ServerIntranet
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.enable();
            Bundle bundle = new Bundle();
            bundle.start();

            ConsoleKeyInfo ch;
            do
            {
                ch = Console.ReadKey();
                switch (ch.Key)
                {
                    case ConsoleKey.S:
                        {
                            
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

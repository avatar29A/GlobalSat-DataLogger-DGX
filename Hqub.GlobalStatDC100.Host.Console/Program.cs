using System;
using System.Collections.Generic;
using System.Text;
using Hqub.GlobalSat;

namespace Hqub.GlobalStatDC100.Host.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var globalStatDc100 = new GlobalSat.GlobalSat("COM6", 230400);
            globalStatDc100.Logger.EventLog += (message, messageLevel) =>
                                                   {
                                                       switch (messageLevel)
                                                       {
                                                           case SimpleLogger.MessageLevel.None:
                                                               System.Console.WriteLine(message);
                                                               break;
                                                           case SimpleLogger.MessageLevel.Debug:
                                                               Print(message, ConsoleColor.Yellow);
                                                               break;

                                                           case SimpleLogger.MessageLevel.DebugError:
                                                               Print(message, ConsoleColor.Red);
                                                               break;
                                                           case SimpleLogger.MessageLevel.Information:
                                                               Print(message, ConsoleColor.Green);
                                                               break;

                                                           case SimpleLogger.MessageLevel.Error:
                                                               Print(message, ConsoleColor.Red);
                                                               break;
                                                       }
                                                   };

            try
            {
                globalStatDc100.ExportToGpx();
                
            }catch(ArgumentOutOfRangeException exception)
            {
                System.Console.WriteLine();
                Print(exception.Message, ConsoleColor.Red);
            }
            System.Console.ReadKey();
            globalStatDc100.Close();
        }

        static void Print(string message, ConsoleColor consoleColor)
        {
            System.Console.ForegroundColor = consoleColor;
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }
    }
}

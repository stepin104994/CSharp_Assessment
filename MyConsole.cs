 using System;
 namespace SampleConApp
{    
        class MyConsole
         {            
             internal static double getDouble(string message)
             {
                 Console.WriteLine(message);
                 return double.Parse(Console.ReadLine());
             }

            internal static string getString(string message)
             {
                 Console.WriteLine(message);
                 return Console.ReadLine();
             }

             internal static int getNumber(string message)
             {
                 return int.Parse(getString(message));
             }            

         }

 }
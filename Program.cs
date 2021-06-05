using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
namespace LeakedMSG
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = @"F:\alt cracking\combo things\+combo+100k.txt";
            string combo = load(source);
            string[] splitup = combo.Split('\n');
            accinfo info = new accinfo();
            foreach (string line in splitup)
            {
                try

                {
                    string[] account = line.Split(':');
                    info.user = account[0];
                    info.pass = account[1];
                    info.source = "NaN";

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"------- - ERROR: {line}");
                    Console.ResetColor();
                }
                sendmsg(info);
            }
            Console.WriteLine("\ndone");
            Console.ReadLine();
        }
        public static void sendmsg(accinfo info)
        {
            Console.WriteLine($"Hello, your account {info.user} was found in a database breach named: {info.source}\nThis was detected and you are being informed by a automated tool created by https:\\www.cybershare.tech.\n Heres some info that the database had:\nUsername:{info.user} Password:{info.pass},\nI recommend you change your passwords on everything and use a password manger, see ya!!\n\n");
        }
        public static string load(string location)
        {
            return File.ReadAllText(location);
        }
    }
}

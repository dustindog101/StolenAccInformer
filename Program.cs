using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace LeakedMSG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter username and password(user:pass)");
            string[] temp = Console.ReadLine().Split(':');
            creds.email = temp[0];
            creds.pass = temp[1];
            Console.Write("Folder path: ");
            string[] comboz = Directory.GetFiles(Console.ReadLine());
            stats.total = comboz.Length;
            foreach (string combo in comboz)
            {
                if (Path.GetExtension(combo) == ".txt")
                {
                    stats.TotalCombos++;
                    updateTitle(combo);
                    workWithCombo(combo);
                    stats.done++;
                }

            }
            
            
            Console.WriteLine("\ndone");
            Console.ReadLine();
        }
    public static void workWithCombo(string file)
    {
        string combo = load(file);
        string[] splitup = combo.Split('\n');
            stats.totalLines += splitup.Length;
        accinfo info = new accinfo();
            foreach (string line in splitup)
            {
                try

                {
                    string[] account = line.Split(':');
                    info.user = account[0];
                    info.pass = account[1];
                    info.source = "NaN";

                    sendmsg(info);
                    Console.ForegroundColor = ConsoleColor.Green;
                    stats.emailsSent++;
                    Console.WriteLine($"Sent: {info.user}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ERROR: {line}");
                    Console.ResetColor();
                }
            }
        }
        public static void updateTitle(string combo)
        {
            Console.Title = $"Informer | Total combos: {stats.TotalCombos} | current combo:{combo} |combos done:{stats.done} |Emails sent:{stats.emailsSent}|Total emails:{stats.totalLines}";
        }
        public static void sendmsg(accinfo info)
        {
            string subject = "Your account was detected in a database breach, please read this";
            string body = ($"Hello, your account {info.user} was found in a database breach named: {info.source}\nThis was detected and you are being informed by a automated tool created by https:\\www.cybershare.tech.\n Heres some info that the database had:\n Username:{info.user} Password:{info.pass},\nI recommend you change your passwords on everything and use a password manger, see ya!!\n\nthe sourcecode for this program is here: https://github.com/dustindog101/StolenAccInformer");
            //
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(creds.email, creds.pass),
                EnableSsl = true,
            };
            //
            var mailMessage = new MailMessage
            {
                From = new MailAddress(creds.email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(info.user);

            smtpClient.Send(mailMessage);

        }
        public static string load(string location)
        {
            return File.ReadAllText(location);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ATM_Console_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ATM.Connect();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            ATM.Disconnect();
        }
    }

    static class ATM
    {
        public static Thread outputThread; // Thread for receiving data from server
        public static TcpClient connection; // client to establish connection
        public static NetworkStream stream; // network data stream
        public static BinaryWriter writer; // facilitates writing to the stream
        public static BinaryReader reader; // facilitates reading from the stream

        public static void Connect()
        {
            // make connection to server and get the associated
            // network stream                                  
            connection = new TcpClient("127.0.0.1", 50000);
            stream = connection.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);

            // start a new thread for sending and receiving messages
            outputThread = new Thread(new ThreadStart(Run));
            outputThread.Start();
        }

        public static void Disconnect()
        {
            writer.Write("exiting");
            connection.Close();
            stream.Close();
            writer.Close();
            reader.Close();
        }

        public static void ProcessMessage(string message)
        {
            string[] command = message.Split(';');

            if (command[0] == "login")
            {
                Console.WriteLine("Welcome!");
                Console.Write("Please enter your account number: ");
                string number = Console.ReadLine();
                Console.Write("Enter your PIN: ");
                string pin = Console.ReadLine();

                writer.Write(number + ';' + pin);
            }
            else if (command[0] == "confirmed")
            {
                bool valid = false;
                while (!valid)
                {
                    Console.Clear();
                    Console.WriteLine("Main menu");
                    Console.WriteLine("\t1 - View my balance");
                    Console.WriteLine("\t2 - Withdraw cash");
                    Console.WriteLine("\t3 - Deposit funds");
                    Console.WriteLine("\t4 - Exit");
                    Console.Write("Enter a choice: ");
                    string choice = Console.ReadLine();

                    if (choice == "1" || choice == "2" || choice == "3")
                    {
                        valid = true;
                        writer.Write(choice);
                    }
                    else if (choice == "4")
                    {
                        Disconnect();
                        Environment.Exit(0);
                    }
                    else
                    {
                        valid = false;
                    }
                }
            }
            else if (command[0] == "balance")
            {
                Console.Clear();
                Console.WriteLine("Amount: {0:C}", Convert.ToDecimal(command[1]));
                Console.Read();
                writer.Write("confirmed");
            }
            else if (command[0] == "withdraw")
            {
                Console.Write("Amount: ");
                string input = Console.ReadLine();
                writer.Write("amount;" + input);
                string output = reader.ReadString();
                Console.WriteLine(output);
                writer.Write("confirmed");
            }
            else if (command[0] == "deposit")
            {
                Console.Write("Amount: ");
                string input = Console.ReadLine();
                writer.Write("amount;" + input);
                string output = reader.ReadString();
                Console.WriteLine(output);
                writer.Write("confirmed");
            }
            else
            {
                Console.WriteLine("wut");
            }
        }

        static void Run()
        {
            // process incoming messages
            try
            {
                // receive messages sent to client       
                while (true)
                    ProcessMessage(reader.ReadString());
            }
            catch (IOException)
            {
                Console.WriteLine("Server is down");
                Thread.Sleep(1000);
            }
        }
    }
}

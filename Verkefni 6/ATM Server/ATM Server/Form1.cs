using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Data.SQLite;

namespace ATM_Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        internal List<User> users; // two Player objects                      
        private List<Thread> userThreads; // Threads for client interaction   

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayerGetter.RunWorkerAsync();
            //users = new List<User>();
            userThreads = new List<Thread>();
            ATM.Connect_DB();
            //User test = new User("Viktor", "12345", "1234", 500000);
            //test.Save();
            //User test = new User("John", "54321", "4321", 500000);
            //test.Save();
        }

        public void Log(string message)
        {
            txt_log.Invoke((MethodInvoker)delegate
            {
                txt_log.AppendText(message + "\r\n");
            });
        }

        private void PlayerGetter_DoWork(object sender, DoWorkEventArgs e)
        {
            Log("Starting TCP listener...");
            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 50000);
            listener.Start();
            Log("TCP listener started");

            while (true)
            {
                var user = new UserConnection(listener.AcceptSocket(), this);
                //users.Add(user);
                //userThreads.Add(new Thread(new ThreadStart(user.Run)));
                Task.Factory.StartNew(user.Run);
                Log("Connection accepted");
            }
        }
    }

    internal static class ATM
    {
        public static SQLiteConnection DB = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");

        public static void Connect_DB()
        {
            SQLiteConnection.CreateFile("Users.sqlite");
            DB.Open();

            // Create a table for user
            try
            {
                string sql = "create table Users (name varchar(20), account_number varchar(5), pin varchar(4), balance decimal(19, 4))";
                SQLiteCommand command = new SQLiteCommand(sql, DB);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static User Login(string number, string pin)
        {
            string sql = string.Format("SELECT * FROM Users WHERE account_number LIKE {0} AND pin LIKE {1}", number, pin);
            SQLiteCommand command = new SQLiteCommand(sql, DB);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                return new User((string)reader["name"], (string)reader["account_number"], (string)reader["pin"], (decimal)reader["balance"]);
            return null;
        }
    }

    public class User
    {
        private string Name;
        private string AccountNumber;
        private string Pin;
        private decimal Balance;

        public User(string name, string number, string pin, decimal balance)
        {
            Name = name;
            AccountNumber = number;
            Pin = pin;
            Balance = balance;
        }

        public void Save()
        {
            string template = "INSERT OR IGNORE INTO Users (name, account_number, pin, balance) VALUES ('{0}', '{1}', '{2}', {3}); UPDATE Users SET name='{0}', pin='{2}', balance={3} WHERE account_number LIKE {1};";
            string sql = string.Format(template, Name, AccountNumber, Pin, Balance);
            SQLiteCommand command = new SQLiteCommand(sql, ATM.DB);
            command.ExecuteNonQuery();
        }

        public void Update()
        {
            string sql = string.Format("SELECT * FROM Users WHERE account_number LIKE {0} AND pin LIKE {1}", AccountNumber, Pin);

            SQLiteCommand command = new SQLiteCommand(sql, ATM.DB);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Balance = (decimal)reader["balance"];
                AccountNumber = (string)reader["account_number"];
                Pin = (string)reader["pin"];
                Balance = (decimal)reader["balance"];
            }
            Console.WriteLine("Update complete!");
        }

        public decimal ViewBalance()
        {
            Update();
            return Balance;
        }

        public string Withdraw_Cash(string str_amount)
        {
            decimal amount;
            try
            {
                amount = Convert.ToDecimal(str_amount);

                Update();
                if (Balance >= amount)
                {
                    Balance -= amount;
                    Save();
                    return "confirmed";
                }
                else
                {
                    return "nomoney";
                }
            }
            catch (Exception)
            {
                return "error";
            }
        }

        public string Deposit_Cash(string str_amount)
        {
            decimal amount;
            try
            {
                amount = Convert.ToDecimal(str_amount);

                Update();
                Balance += amount;
                Save();
                return "confirmed";
            }
            catch (Exception)
            {
               return "error";
            }
        }
    }

    public class UserConnection
    {
        internal Socket connection; // Socket for accepting a connection    
        private NetworkStream socketStream; // network data stream          
        private Form1 server; // reference to server
        internal BinaryWriter writer; // facilitates writing to the stream   
        private BinaryReader reader; // facilitates reading from the stream 
        private User user;
        private bool loggedIn = false;

        public UserConnection(Socket socket, Form1 serverValue)
        {
            connection = socket;
            server = serverValue;

            // create NetworkStream object for Socket      
            socketStream = new NetworkStream(connection);

            // create Streams for reading/writing bytes
            writer = new BinaryWriter(socketStream);
            reader = new BinaryReader(socketStream);
            server.Log("user setup");
        }

        public void Run()
        {
            using (connection)
            {
                server.Log("User connected");

                while (!loggedIn)
                {
                    writer.Write("login");
                    server.Log("Wrote login");

                    string login = reader.ReadString();
                    server.Log(login);
                    user = ATM.Login(login.Split(';')[0], login.Split(';')[1]);

                    if (user != null)
                    {
                        writer.Write("confirmed");
                        loggedIn = true;
                    }

                }

                while (loggedIn)
                {
                    string input = reader.ReadString();
                    server.Log(input);
                    ProcessMessage(input);
                }
            }
        }

        private void Disconnect()
        {
            loggedIn = false;
            connection.Close();
            socketStream.Close();
            writer.Close();
            reader.Close();
        }

        private void ProcessMessage(string message)
        {
            if (message == "disconnect")
            {
                server.Log("disconnect");
            }
            else if (message == "1")
            {
                writer.Write("balance;" + user.ViewBalance());
            }
            else if (message == "2")
            {
                writer.Write("withdraw");
                string input = reader.ReadString();
                string amount = input.Split(';')[1];
                writer.Write(user.Withdraw_Cash(amount));
            }
            else if (message == "3")
            {
                writer.Write("deposit");
                string input = reader.ReadString();
                string amount = input.Split(';')[1];
                writer.Write(user.Deposit_Cash(amount));
            }
            else if (message == "4")
            {
                Disconnect();
                return;
            }
            else if (message == "confirmed")
            {
                writer.Write("confirmed");
            }
        }
    }
}

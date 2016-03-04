// Exercise 23.6: HangmanClient.cs
// Hangman game that is played via a server.
using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;

public partial class HangmanClientForm : Form
{
    public HangmanClientForm()
    {
        InitializeComponent();
    } // end constructor

    BinaryWriter writer; // facilitates writing to the stream
    BinaryReader reader; // facilitates reading from the stream

    int numberWrong = 0;

    // initialize connection
    private void HangmanClientForm_Load(object sender, EventArgs e)
    {
        // connect to server
        TcpClient client = new TcpClient();
        client.Connect("localhost", 50000);

        // get NetworkStream
        NetworkStream stream = client.GetStream();
        writer = new BinaryWriter(stream);
        reader = new BinaryReader(stream);

        int wordLength = reader.ReadInt32();

        for (int i = 0; i < wordLength; i++)
        {
            guessLabel.Text += "-";
        }
    }

    // guess a letter
    private void guessButton_Click(object sender, System.EventArgs e)
    {
        if (guessTextBox.Text.Length != 1)
        {
            MessageBox.Show("Guess one letter at a time u fkn fag");
                return;
        }
        writer.Write(guessTextBox.Text[0]);
        int numRight = reader.ReadInt32();
        for (int i = 0; i < numRight; i++)
        {
            int replace = reader.ReadInt32();

            string replication = "";
            if (replace > 0)
            {
                replication += guessLabel.Text.Substring(0, replace);
            }
            else
                replication = "";
            
            replication += guessTextBox.Text + guessLabel.Text.Substring(replace + 1, guessLabel.Text.Length - (replace + 1));
            guessLabel.Text = replication;

        }
        if (numRight == 0)
        {
            numberWrong++;
        }
        countLabel.Text = numberWrong.ToString();
        if (numberWrong == 5)
        {
            MessageBox.Show("Game over\nThe correct answer is " + reader.ReadString());
            Application.Exit();
        }
        if (guessLabel.Text.IndexOf('-') < 0)
        {
            MessageBox.Show("You have correctly guessed the word");
        }
        guessTextBox.Clear();
        guessTextBox.Focus();

    } // end method guessButton_Click
} // end class HangmanClientForm
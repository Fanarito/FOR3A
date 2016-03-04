// Exercise 23.6: HangmanServer.cs
// Allows clients to play Hangman game.
using System;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

public partial class HangmanServerForm : Form
{
    public HangmanServerForm()
    {
        InitializeComponent();
    } // end constructor

    // initialize thread to allow client to connect
    private void HangmanServerForm_Load(object sender, EventArgs e)
    {
        Thread readThread = new Thread(new ThreadStart(GetGuesses));
        readThread.Start();
    } // end method HangmanServerForm_Load


    // skrifa texta
    private void DisplayMessage(string message)
    {
        // if modifying displayTextBox is not thread safe
        if (displayTextBox.InvokeRequired)
        {
            Invoke(new MethodInvoker(() => DisplayMessage(message)));
        } // end if
        else // OK to modify displayTextBox in current thread
            displayTextBox.Text += message;
    }

    // set up connection for client to play Hangman
    public void GetGuesses()
    {
        // start listening for connections
        IPAddress local = IPAddress.Parse( "127.0.0.1" );
        TcpListener listener = new TcpListener( local, 50000 );
        listener.Start();

        // accept client connection and get NetworkStream to communicate with client
        Socket connection = listener.AcceptSocket();
        DisplayMessage( "Connection received\r\n" );

        NetworkStream socketStream = new NetworkStream( connection );

        // create reader and writer for client
        BinaryWriter writer = new BinaryWriter( socketStream );
        BinaryReader reader = new BinaryReader( socketStream );

        Random randomWord = new Random();
        int wordNumber = randomWord.Next(297);
        string word = "";
        StreamReader wordReader = new StreamReader("words.txt");
        for (int i = 0; i <= wordNumber; i++)
            word = wordReader.ReadLine();

        DisplayMessage("The secret word is: " + word + "\r\n");
        writer.Write(word.Length);

        int numberWrong = 0;
        int numberLettersLeft = word.Length;
        int numberCharsInWord = 0;
        char guess;

        while (numberLettersLeft > 0 && numberWrong < 5)
        {
            numberCharsInWord = 0;
            guess = reader.ReadChar();
            DisplayMessage("The User guessed " + guess + "\r\n");

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == guess)
                {
                    numberCharsInWord++;
                }
            }
            writer.Write(numberCharsInWord);
            if (numberCharsInWord != 0)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == guess)
                    {
                        writer.Write(i);
                        numberLettersLeft--;
                    }
                }
            }
            else
	        {
                numberWrong++;
	        }
        }//endwhile
        if (numberLettersLeft != 0 && numberWrong == 5)
	    {
		    writer.Write(word);
	    }
        connection.Close();
        socketStream.Close();
        reader.Close();
        writer.Close();

    } // end method GetGuesses

    private void HangmanServerForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Environment.Exit(0);
    }
}// end class HangmanServerForm
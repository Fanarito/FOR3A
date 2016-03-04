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
   private void HangmanServerForm_Load( object sender, EventArgs e )
   {
      Thread readThread = new Thread( new ThreadStart( GetGuesses ) );
      readThread.Start();
   } // end method HangmanServerForm_Load

   // delegate that allows method DisplayMessage to be called
   // in the thread that creates and maintains the GUI       
   private delegate void DisplayDelegate( string message );  

   // method DisplayMessage sets displayTextBox's Text property
   // in a thread-safe manner
   private void DisplayMessage( string message )
   {
      // if modifying displayTextBox is not thread safe
      if ( displayTextBox.InvokeRequired )
      {
         // use inherited method Invoke to execute DisplayMessage
         // via a delegate                                       
         Invoke( new DisplayDelegate( DisplayMessage ),          
            new object[] { message } );                          
      } // end if
      else // OK to modify displayTextBox in current thread
         displayTextBox.Text += message;
   } // end method DisplayMessage

   // set up connection for client to play Hangman
   public void GetGuesses()
   {
      // start listening for connections
      IPAddress local = IPAddress.Parse( "127.0.0.1" );
      TcpListener listener = new TcpListener( local, 50000 );
      listener.Start();

      // accept client connection and get NetworkStream to
      // communicate with client
      Socket connection = listener.AcceptSocket();
      DisplayMessage( "Connection received\r\n" );
      NetworkStream socketStream = new NetworkStream( connection );

      // create reader and writer for client
      BinaryWriter writer = new BinaryWriter( socketStream );
      BinaryReader reader = new BinaryReader( socketStream );

      // choose random word
      Random randomWord = new Random();
      int wordNumber = randomWord.Next( 25 );
      string word = "";

      // open word file                                         
      StreamReader wordReader = new StreamReader( "words.txt" );

      // find word in file                   
      for ( int i = 0; i <= wordNumber; i++ )
         word = wordReader.ReadLine();       

      // display word and send length of word to client        
      DisplayMessage( "The secret word is: " + word + "\r\n" );
      writer.Write( word.Length );                             
      
      // initialize Hangman game variables
      int numberWrong = 0, numberLettersLeft = word.Length, 
         numberCharsInWord = 0; 
      char guess;

      // while entire word has not been guessed and
      // user has not made 5 mistakes, process user guesses
      while ( numberLettersLeft > 0 && numberWrong < 5 )
      {
         numberCharsInWord = 0;

         // get guess                                                
         guess = reader.ReadChar();                                  
         DisplayMessage( "The user guessed: " + guess + "\r\n" );    

         // find out how many occurrences of letter in word
         for ( int i = 0; i < word.Length; i++ )           
            if ( word[ i ] == guess )                      
               numberCharsInWord++;                        

         // send client number of occurrences
         writer.Write( numberCharsInWord );  

         // if the character is present, send index values
         // of each occurrence
         if ( numberCharsInWord != 0 )
         {
            for ( int i = 0; i < word.Length; i++ )
            {
               if ( word[ i ] == guess )                      
               {                                              
                  writer.Write( i );                          
                                                              
                  // decrement number of letters left to guess
                  numberLettersLeft--;                        
               } // end if                                    
            } // end for
         } // end if
         else
            numberWrong++; // user made mistake, add 1 to number wrong
      } // end while
      
      // if word not guessed and user made 5 mistakes,
      // tell user the word
      if ( numberLettersLeft != 0 && numberWrong == 5 )
         writer.Write( word );                         

      // close connections
      connection.Close();
      socketStream.Close();
      reader.Close();
      writer.Close();
   } // end method GetGuesses
} // end class HangmanServerForm

/**************************************************************************
 * (C) Copyright 1992-2006 by Deitel & Associates, Inc. and               *
 * Pearson Education, Inc. All Rights Reserved.                           *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 *************************************************************************/
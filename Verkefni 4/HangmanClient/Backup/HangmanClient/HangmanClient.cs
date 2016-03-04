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
   int numberWrong = 0; // count the number of wrong guesses

   // initialize connection
   private void HangmanClientForm_Load( object sender, EventArgs e )
   {
      // connect to server
      TcpClient client = new TcpClient();
      client.Connect( "localhost", 50000 );

      // get NetworkStream
      NetworkStream stream = client.GetStream();
      writer = new BinaryWriter( stream );
      reader = new BinaryReader( stream );

      // read the secret word's length from the server
      int wordLength = reader.ReadInt32();            

      for ( int i = 0; i < wordLength; i++ )
         guessLabel.Text += "-";            
   } // end method HangmanClientForm_Load

   // guess a letter
   private void guessButton_Click( object sender, System.EventArgs e )
   {
      // verify that the user guessed exactly one letter  
      if ( guessTextBox.Text.Length != 1 )                
      {                                                   
         MessageBox.Show( "Guess one letter at a time." );
         return;                                          
      } // end if                                         
      
      // send the server the letter guessed  
      writer.Write( guessTextBox.Text[ 0 ] );

      // get from server the number of occurrences of letter
      int numRight = reader.ReadInt32();                    

      // for every occurrence, get from server the location of letter
      // and show each occurrence on GUI
      for ( int i = 0; i < numRight; i++ )
      {
         int replace = reader.ReadInt32();

         // display word with this occurrence revealed                 
         guessLabel.Text =                                             
            ( replace > 0 ?                                            
               guessLabel.Text.Substring( 0, replace ) : "" ) +        
            guessTextBox.Text +                                        
            guessLabel.Text.Substring(                                 
               replace + 1, guessLabel.Text.Length - ( replace + 1 ) );
      } // end for

      // if the letter does not appear,        
      // add 1 to the count of wrong guesses   
      if ( numRight == 0 )                     
         numberWrong++;                        
      countLabel.Text = numberWrong.ToString();

      // if limit of wrong gusses reached, inform             
      // the user that the game is over and exit              
      if ( numberWrong == 5 )                                 
      {                                                       
         MessageBox.Show( "Game Over\nThe correct word is: " +
            reader.ReadString() );                            
         Application.Exit();                                  
      } // end if                                             

      // if no '-' characters, then the entire word has been       
      // guessed; congratulate user                                
      if ( guessLabel.Text.IndexOf( '-' ) < 0 )                    
         MessageBox.Show( "You have correctly guessed the word!" );

      guessTextBox.Clear();
      guessTextBox.Focus();
   } // end method guessButton_Click
} // end class HangmanClientForm

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
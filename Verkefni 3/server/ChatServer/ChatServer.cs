// Fig. 23.1: ChatServer.cs
// Set up a server that will receive a connection from a client, send a
// string to the client, chat with the client and close the connection.
using System;
using System.Windows.Forms;
using System.Threading;  
using System.Net;        
using System.Net.Sockets;
using System.IO;         

public partial class ChatServerForm : Form
{
   public ChatServerForm()
   {
      InitializeComponent();
   } // end constructor

   private Socket connection; // Socket for accepting a connection      
   private Thread readThread; // Thread for processing incoming messages
   private NetworkStream socketStream; // network data stream           
   private BinaryWriter writer; // facilitates writing to the stream    
   private BinaryReader reader; // facilitates reading from the stream  

   // initialize thread for reading
   private void ChatServerForm_Load( object sender, EventArgs e )
   {
      readThread = new Thread( new ThreadStart( RunServer ) );
      readThread.Start();                                     
   } // end method CharServerForm_Load

   // close all threads associated with this application
   private void ChatServerForm_FormClosing( object sender, 
      FormClosingEventArgs e )
   {         
      System.Environment.Exit( System.Environment.ExitCode );
   } // end method CharServerForm_FormClosing

   // delegate that allows method DisplayMessage to be called
   // in the thread that creates and maintains the GUI       
   private delegate void DisplayDelegate( string message );  

   // method DisplayMessage sets displayTextBox's Text property
   // in a thread-safe manner
   private void DisplayMessage( string message )
   {
       // if modifying displayTextBox is not thread safe
       if (displayTextBox.InvokeRequired)
       {
           Invoke(new MethodInvoker(() => DisplayMessage(message)));
       } // end if
      else // OK to modify displayTextBox in current thread
         displayTextBox.AppendText(message);
   } // end method DisplayMessage

   // delegate that allows method DisableInput to be called 
   // in the thread that creates and maintains the GUI
   private delegate void DisableInputDelegate( bool value );

   // method DisableInput sets inputTextBox's ReadOnly property
   // in a thread-safe manner
   private void DisableInput( bool value )
   {
      // if modifying inputTextBox is not thread safe
      if (inputTextBox.InvokeRequired)
      {
            Invoke(new MethodInvoker(() => DisableInput(value)));
      } // end if
      else
         inputTextBox.ReadOnly = value;
   } // end method DisableInput

   // send the text typed at the server to the client
   private void inputTextBox_KeyDown( object sender, KeyEventArgs e )
   {
      // send the text to the client
      try
      {         
         if ( e.KeyCode == Keys.Enter && inputTextBox.ReadOnly == false )
         {
            writer.Write( "SERVER>>> " + inputTextBox.Text );
            displayTextBox.AppendText("\r\nSERVER>>> " + inputTextBox.Text);

            // if the user at the server signaled termination
            // sever the connection to the client
            if ( inputTextBox.Text == "TERMINATE" )
               connection.Close();

            inputTextBox.Clear(); // clear the user’s input
         } // end if
      } // end try
      catch ( SocketException )
      {
         displayTextBox.Text += "\nError writing object";
      } // end catch
   } // end method inputTextBox_KeyDown

   // allows a client to connect; displays text the client sends
   public void RunServer()
   {
      TcpListener listener;
      int counter = 1;

      // wait for a client connection and display the text
      // that the client sends
      try
      {
         // Step 1: create TcpListener                    
         IPAddress local = IPAddress.Parse( "127.0.0.1" );
         listener = new TcpListener( local, 50000 );       

         // Step 2: TcpListener waits for connection request
         listener.Start();                                  

         // Step 3: establish connection upon client request
         while ( true )
         {
            DisplayMessage( "Waiting for connection\r\n" );

            // accept an incoming connection     
            connection = listener.AcceptSocket();

            // create NetworkStream object associated with socket
            socketStream = new NetworkStream( connection );      

            // create objects for transferring data across stream
            writer = new BinaryWriter( socketStream );           
            reader = new BinaryReader( socketStream );           

            DisplayMessage( "Connection " + counter + " received.\r\n" );

            // inform client that connection was successfull  
            writer.Write( "SERVER>>> Connection successful" );

            DisableInput( false ); // enable inputTextBox

            string theReply = "";

            // Step 4: read string data sent from client
            do
            {
               try
               {   
                  // read the string sent to the server
                  theReply = reader.ReadString();

                  // display the message
                  DisplayMessage( "\r\n" + theReply );
               } // end try
               catch ( Exception )
               {
                  // handle exception if error reading data
                  break;
               } // end catch
            } while ( theReply != "CLIENT>>> TERMINATE"  &&
               connection.Connected );

            DisplayMessage( "\r\nUser terminated connection\r\n" );

            // Step 5: close connection  
            writer.Close();              
            reader.Close();              
            socketStream.Close();        
            connection.Close();          

            DisableInput( true ); // disable InputTextBox
            counter++;
         } // end while
      } // end try
      catch ( Exception error )
      {
         MessageBox.Show( error.ToString() );
      } // end catch
   } // end method RunServer
} // end class ChatServerForm
partial class HangmanClientForm
{
   /// <summary>
   /// Required designer variable.
   /// </summary>
   private System.ComponentModel.IContainer components = null;

   /// <summary>
   /// Clean up any resources being used.
   /// </summary>
   /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
   protected override void Dispose( bool disposing )
   {
      if (disposing && (components != null))
      {
         components.Dispose();
      }
      base.Dispose( disposing );
   }

   #region Windows Form Designer generated code

   /// <summary>
   /// Required method for Designer support - do not modify
   /// the contents of this method with the code editor.
   /// </summary>
   private void InitializeComponent()
   {
      this.wordLabel = new System.Windows.Forms.Label();
      this.numberWrongLabel = new System.Windows.Forms.Label();
      this.guessTextBox = new System.Windows.Forms.TextBox();
      this.guessLabel = new System.Windows.Forms.Label();
      this.countLabel = new System.Windows.Forms.Label();
      this.guessButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // wordLabel
      // 
      this.wordLabel.AutoSize = true;
      this.wordLabel.Location = new System.Drawing.Point( 10, 13 );
      this.wordLabel.Name = "wordLabel";
      this.wordLabel.Size = new System.Drawing.Size( 40, 16 );
      this.wordLabel.TabIndex = 0;
      this.wordLabel.Text = "Word:";
      // 
      // numberWrongLabel
      // 
      this.numberWrongLabel.AutoSize = true;
      this.numberWrongLabel.Location = new System.Drawing.Point( 10, 33 );
      this.numberWrongLabel.Name = "numberWrongLabel";
      this.numberWrongLabel.Size = new System.Drawing.Size( 94, 16 );
      this.numberWrongLabel.TabIndex = 1;
      this.numberWrongLabel.Text = "Number wrong:";
      // 
      // guessTextBox
      // 
      this.guessTextBox.Location = new System.Drawing.Point( 12, 57 );
      this.guessTextBox.Name = "guessTextBox";
      this.guessTextBox.Size = new System.Drawing.Size( 91, 22 );
      this.guessTextBox.TabIndex = 2;
      // 
      // guessLabel
      // 
      this.guessLabel.Font = new System.Drawing.Font( "Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)) );
      this.guessLabel.Location = new System.Drawing.Point( 127, 13 );
      this.guessLabel.Name = "guessLabel";
      this.guessLabel.Size = new System.Drawing.Size( 109, 16 );
      this.guessLabel.TabIndex = 3;
      this.guessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // countLabel
      // 
      this.countLabel.AutoSize = true;
      this.countLabel.Location = new System.Drawing.Point( 127, 33 );
      this.countLabel.Name = "countLabel";
      this.countLabel.Size = new System.Drawing.Size( 11, 16 );
      this.countLabel.TabIndex = 4;
      this.countLabel.Text = "0";
      // 
      // guessButton
      // 
      this.guessButton.Location = new System.Drawing.Point( 129, 57 );
      this.guessButton.Name = "guessButton";
      this.guessButton.Size = new System.Drawing.Size( 75, 23 );
      this.guessButton.TabIndex = 5;
      this.guessButton.Text = "Guess";
      this.guessButton.Click += new System.EventHandler( this.guessButton_Click );
      // 
      // HangmanClientForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 8F, 16F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 248, 91 );
      this.Controls.Add( this.guessButton );
      this.Controls.Add( this.countLabel );
      this.Controls.Add( this.guessLabel );
      this.Controls.Add( this.guessTextBox );
      this.Controls.Add( this.numberWrongLabel );
      this.Controls.Add( this.wordLabel );
      this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 9.75F );
      this.Margin = new System.Windows.Forms.Padding( 4 );
      this.Name = "HangmanClientForm";
      this.Text = "Hangman Client";
      this.Load += new System.EventHandler( this.HangmanClientForm_Load );
      this.ResumeLayout( false );
      this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.Label wordLabel;
   private System.Windows.Forms.Label numberWrongLabel;
   private System.Windows.Forms.TextBox guessTextBox;
   private System.Windows.Forms.Label guessLabel;
   private System.Windows.Forms.Label countLabel;
   private System.Windows.Forms.Button guessButton;
}


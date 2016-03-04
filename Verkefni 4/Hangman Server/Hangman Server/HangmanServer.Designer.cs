partial class HangmanServerForm
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
            this.displayTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // displayTextBox
            // 
            this.displayTextBox.Location = new System.Drawing.Point(16, 15);
            this.displayTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.displayTextBox.Multiline = true;
            this.displayTextBox.Name = "displayTextBox";
            this.displayTextBox.ReadOnly = true;
            this.displayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayTextBox.Size = new System.Drawing.Size(356, 297);
            this.displayTextBox.TabIndex = 0;
            // 
            // HangmanServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 327);
            this.Controls.Add(this.displayTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HangmanServerForm";
            this.Text = "Hangman Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HangmanServerForm_FormClosing);
            this.Load += new System.EventHandler(this.HangmanServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

   }

   #endregion

   private System.Windows.Forms.TextBox displayTextBox;
}


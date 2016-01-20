namespace Verkefni_1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BallPanel = new System.Windows.Forms.Panel();
            this.ballCounter = new System.Windows.Forms.Label();
            this.nud_AmountOfBalls = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nud_AmountOfBalls)).BeginInit();
            this.SuspendLayout();
            // 
            // BallPanel
            // 
            this.BallPanel.Location = new System.Drawing.Point(12, 12);
            this.BallPanel.Name = "BallPanel";
            this.BallPanel.Size = new System.Drawing.Size(705, 469);
            this.BallPanel.TabIndex = 0;
            this.BallPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.BallPanel_Paint);
            this.BallPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BallPanel_MouseClick);
            // 
            // ballCounter
            // 
            this.ballCounter.AutoSize = true;
            this.ballCounter.Location = new System.Drawing.Point(724, 13);
            this.ballCounter.Name = "ballCounter";
            this.ballCounter.Size = new System.Drawing.Size(13, 13);
            this.ballCounter.TabIndex = 1;
            this.ballCounter.Text = "0";
            // 
            // nud_AmountOfBalls
            // 
            this.nud_AmountOfBalls.Location = new System.Drawing.Point(727, 29);
            this.nud_AmountOfBalls.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nud_AmountOfBalls.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_AmountOfBalls.Name = "nud_AmountOfBalls";
            this.nud_AmountOfBalls.Size = new System.Drawing.Size(43, 20);
            this.nud_AmountOfBalls.TabIndex = 2;
            this.nud_AmountOfBalls.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 493);
            this.Controls.Add(this.nud_AmountOfBalls);
            this.Controls.Add(this.ballCounter);
            this.Controls.Add(this.BallPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nud_AmountOfBalls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BallPanel;
        private System.Windows.Forms.Label ballCounter;
        private System.Windows.Forms.NumericUpDown nud_AmountOfBalls;
    }
}


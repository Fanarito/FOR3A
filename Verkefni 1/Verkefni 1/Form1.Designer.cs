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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_AmountOfBalls)).BeginInit();
            this.SuspendLayout();
            // 
            // BallPanel
            // 
            this.BallPanel.Location = new System.Drawing.Point(0, -1);
            this.BallPanel.Name = "BallPanel";
            this.BallPanel.Size = new System.Drawing.Size(730, 482);
            this.BallPanel.TabIndex = 0;
            this.BallPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.BallPanel_Paint);
            this.BallPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BallPanel_MouseClick);
            // 
            // ballCounter
            // 
            this.ballCounter.AutoSize = true;
            this.ballCounter.Location = new System.Drawing.Point(124, 515);
            this.ballCounter.Name = "ballCounter";
            this.ballCounter.Size = new System.Drawing.Size(13, 13);
            this.ballCounter.TabIndex = 1;
            this.ballCounter.Text = "0";
            // 
            // nud_AmountOfBalls
            // 
            this.nud_AmountOfBalls.Location = new System.Drawing.Point(127, 487);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 515);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fjöldi bolta : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 489);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fjöldi settir inn í einu : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 552);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}


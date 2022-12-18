namespace client_graphics
{
    partial class GameView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.ConsoleTextBox = new System.Windows.Forms.RichTextBox();
            this.Level1Button = new System.Windows.Forms.Button();
            this.Level2Button = new System.Windows.Forms.Button();
            this.Level3Button = new System.Windows.Forms.Button();
            this.StressTest = new System.Windows.Forms.Button();
            this.DrawTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 10;
            this.GameTimer.Tick += new System.EventHandler(this.OnTick);
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.BackColor = System.Drawing.SystemColors.Desktop;
            this.ConsoleTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ConsoleTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ConsoleTextBox.Location = new System.Drawing.Point(14, 895);
            this.ConsoleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ReadOnly = true;
            this.ConsoleTextBox.Size = new System.Drawing.Size(982, 143);
            this.ConsoleTextBox.TabIndex = 0;
            this.ConsoleTextBox.Text = "";
            // 
            // Level1Button
            // 
            this.Level1Button.Location = new System.Drawing.Point(726, 16);
            this.Level1Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Level1Button.Name = "Level1Button";
            this.Level1Button.Size = new System.Drawing.Size(86, 31);
            this.Level1Button.TabIndex = 1;
            this.Level1Button.Text = "Level 1";
            this.Level1Button.UseVisualStyleBackColor = true;
            this.Level1Button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Level1Button_MouseClick);
            // 
            // Level2Button
            // 
            this.Level2Button.Location = new System.Drawing.Point(818, 16);
            this.Level2Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Level2Button.Name = "Level2Button";
            this.Level2Button.Size = new System.Drawing.Size(86, 31);
            this.Level2Button.TabIndex = 2;
            this.Level2Button.Text = "Level 2";
            this.Level2Button.UseVisualStyleBackColor = true;
            this.Level2Button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Level2Button_MouseClick);
            // 
            // Level3Button
            // 
            this.Level3Button.Location = new System.Drawing.Point(911, 16);
            this.Level3Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Level3Button.Name = "Level3Button";
            this.Level3Button.Size = new System.Drawing.Size(86, 31);
            this.Level3Button.TabIndex = 3;
            this.Level3Button.Text = "Level 3";
            this.Level3Button.UseVisualStyleBackColor = true;
            this.Level3Button.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Level3Button_MouseClick);
            // 
            // StressTest
            // 
            this.StressTest.Location = new System.Drawing.Point(911, 54);
            this.StressTest.Name = "StressTest";
            this.StressTest.Size = new System.Drawing.Size(86, 29);
            this.StressTest.TabIndex = 4;
            this.StressTest.Text = "StressTest";
            this.StressTest.UseVisualStyleBackColor = true;
            this.StressTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // DrawTest
            // 
            this.DrawTest.Location = new System.Drawing.Point(911, 89);
            this.DrawTest.Name = "DrawTest";
            this.DrawTest.Size = new System.Drawing.Size(86, 29);
            this.DrawTest.TabIndex = 5;
            this.DrawTest.Text = "DrawTest";
            this.DrawTest.UseVisualStyleBackColor = true;
            this.DrawTest.Click += new System.EventHandler(this.DrawTest_Click);
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 1055);
            this.Controls.Add(this.DrawTest);
            this.Controls.Add(this.StressTest);
            this.Controls.Add(this.Level3Button);
            this.Controls.Add(this.Level2Button);
            this.Controls.Add(this.Level1Button);
            this.Controls.Add(this.ConsoleTextBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GameView";
            this.Text = "Bomberman";
            this.Load += new System.EventHandler(this.GameView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private RichTextBox ConsoleTextBox;
        private Button Level1Button;
        private Button Level2Button;
        private Button Level3Button;
        private Button StressTest;
        private Button DrawTest;
    }
}
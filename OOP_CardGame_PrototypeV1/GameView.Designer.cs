namespace OOP_CardGame_PrototypeV1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameView));
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.DebugTextbox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 10;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // DebugTextbox
            // 
            this.DebugTextbox.BackColor = System.Drawing.Color.Black;
            this.DebugTextbox.ForeColor = System.Drawing.Color.Chartreuse;
            this.DebugTextbox.Location = new System.Drawing.Point(12, 753);
            this.DebugTextbox.Name = "DebugTextbox";
            this.DebugTextbox.Size = new System.Drawing.Size(860, 96);
            this.DebugTextbox.TabIndex = 0;
            this.DebugTextbox.Text = "";
            this.DebugTextbox.Visible = false;
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 861);
            this.Controls.Add(this.DebugTextbox);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GameView";
            this.Text = "OOP_CardGame_PrototypeV1";
            this.Load += new System.EventHandler(this.GameView_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameView_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameView_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer GameTimer;
        private RichTextBox DebugTextbox;
    }
}
namespace EliteLogger {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.FileWatcher = new System.IO.FileSystemWatcher();
            this.Log = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.FileWatcher)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(13, 15);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(95, 15);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // FileWatcher
            // 
            this.FileWatcher.EnableRaisingEvents = true;
            this.FileWatcher.Filter = "netLog*.log";
            this.FileWatcher.NotifyFilter = System.IO.NotifyFilters.Size;
            this.FileWatcher.Path = "C:\\Users\\John Goode\\AppData\\Local\\Frontier_Developments\\Products\\FORC-FDEV-D-1003" +
    "\\Logs";
            this.FileWatcher.SynchronizingObject = this;
            this.FileWatcher.Changed += new System.IO.FileSystemEventHandler(this.FileWatcher_Changed);
            // 
            // Log
            // 
            this.Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Log.Location = new System.Drawing.Point(0, 0);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(600, 243);
            this.Log.TabIndex = 1;
            this.Log.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Log);
            this.panel1.Location = new System.Drawing.Point(13, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 243);
            this.panel1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 315);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Name = "Form1";
            this.Text = "Elite Logger";
            ((System.ComponentModel.ISupportInitialize)(this.FileWatcher)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.IO.FileSystemWatcher FileWatcher;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox Log;
    }
}


namespace Zealot_s_Dead_Space_Tools.File_Converter
{
    partial class FileConverter
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveForPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveForPS3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.severedgeoToPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meshVolatilePS3ToPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.convertToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(483, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveForPCToolStripMenuItem,
            this.saveForPS3ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveForPCToolStripMenuItem
            // 
            this.saveForPCToolStripMenuItem.Name = "saveForPCToolStripMenuItem";
            this.saveForPCToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveForPCToolStripMenuItem.Text = "Save for PC";
            // 
            // saveForPS3ToolStripMenuItem
            // 
            this.saveForPS3ToolStripMenuItem.Name = "saveForPS3ToolStripMenuItem";
            this.saveForPS3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveForPS3ToolStripMenuItem.Text = "Save for PS3";
            // 
            // convertToolStripMenuItem
            // 
            this.convertToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.severedgeoToPCToolStripMenuItem,
            this.meshVolatilePS3ToPCToolStripMenuItem,
            this.tToolStripMenuItem});
            this.convertToolStripMenuItem.Name = "convertToolStripMenuItem";
            this.convertToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.convertToolStripMenuItem.Text = "Convert";
            // 
            // severedgeoToPCToolStripMenuItem
            // 
            this.severedgeoToPCToolStripMenuItem.Name = "severedgeoToPCToolStripMenuItem";
            this.severedgeoToPCToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.severedgeoToPCToolStripMenuItem.Text = "Geo: PS3 to PC";
            this.severedgeoToPCToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // meshVolatilePS3ToPCToolStripMenuItem
            // 
            this.meshVolatilePS3ToPCToolStripMenuItem.Name = "meshVolatilePS3ToPCToolStripMenuItem";
            this.meshVolatilePS3ToPCToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.meshVolatilePS3ToPCToolStripMenuItem.Text = "MeshVolatile: PS3 to PC";
            this.meshVolatilePS3ToPCToolStripMenuItem.Click += new System.EventHandler(this.meshVolatilePS3ToPCToolStripMenuItem_Click);
            // 
            // tToolStripMenuItem
            // 
            this.tToolStripMenuItem.Name = "tToolStripMenuItem";
            this.tToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.tToolStripMenuItem.Text = "FaceList: PS3 to PC";
            this.tToolStripMenuItem.Click += new System.EventHandler(this.tToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(244, 85);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(227, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(227, 37);
            this.button2.TabIndex = 6;
            this.button2.Text = "Geo: PS3 to PC (Batch)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(460, 36);
            this.button1.TabIndex = 7;
            this.button1.Text = "MeshVolatile: PS3 to PC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.meshVolatilePS3ToPCToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 154);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(460, 36);
            this.button3.TabIndex = 8;
            this.button3.Text = "FaceList: PS3 to PC";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.tToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Texture hash to use for batch convert:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(460, 37);
            this.button4.TabIndex = 10;
            this.button4.Text = "Geo: PS3 to PC";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FileConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 200);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FileConverter";
            this.Text = "Dead Space File Converter";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveForPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveForPS3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem severedgeoToPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meshVolatilePS3ToPCToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
    }
}
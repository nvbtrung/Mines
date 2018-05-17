namespace Mines.OptionWindow
{
    partial class Option
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
            this.Row_label = new System.Windows.Forms.Label();
            this.row = new System.Windows.Forms.NumericUpDown();
            this.Col_label = new System.Windows.Forms.Label();
            this.col = new System.Windows.Forms.NumericUpDown();
            this.Bomb_label = new System.Windows.Forms.Label();
            this.bomb = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.row)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.col)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bomb)).BeginInit();
            this.SuspendLayout();
            // 
            // Row_label
            // 
            this.Row_label.AutoSize = true;
            this.Row_label.Location = new System.Drawing.Point(12, 15);
            this.Row_label.Name = "Row_label";
            this.Row_label.Size = new System.Drawing.Size(35, 13);
            this.Row_label.TabIndex = 0;
            this.Row_label.Text = "Row :";
            // 
            // row
            // 
            this.row.Location = new System.Drawing.Point(75, 12);
            this.row.Name = "row";
            this.row.Size = new System.Drawing.Size(120, 20);
            this.row.TabIndex = 1;
            // 
            // Col_label
            // 
            this.Col_label.AutoSize = true;
            this.Col_label.Location = new System.Drawing.Point(12, 47);
            this.Col_label.Name = "Col_label";
            this.Col_label.Size = new System.Drawing.Size(48, 13);
            this.Col_label.TabIndex = 2;
            this.Col_label.Text = "Column :";
            // 
            // col
            // 
            this.col.Location = new System.Drawing.Point(75, 45);
            this.col.Name = "col";
            this.col.Size = new System.Drawing.Size(120, 20);
            this.col.TabIndex = 3;
            // 
            // Bomb_label
            // 
            this.Bomb_label.AutoSize = true;
            this.Bomb_label.Location = new System.Drawing.Point(12, 79);
            this.Bomb_label.Name = "Bomb_label";
            this.Bomb_label.Size = new System.Drawing.Size(40, 13);
            this.Bomb_label.TabIndex = 4;
            this.Bomb_label.Text = "Bomb :";
            // 
            // bomb
            // 
            this.bomb.Location = new System.Drawing.Point(75, 77);
            this.bomb.Name = "bomb";
            this.bomb.Size = new System.Drawing.Size(120, 20);
            this.bomb.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(119, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Submit_Click);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 153);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bomb);
            this.Controls.Add(this.Bomb_label);
            this.Controls.Add(this.col);
            this.Controls.Add(this.Col_label);
            this.Controls.Add(this.row);
            this.Controls.Add(this.Row_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Option";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.row)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.col)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bomb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Row_label;
        private System.Windows.Forms.NumericUpDown row;
        private System.Windows.Forms.Label Col_label;
        private System.Windows.Forms.NumericUpDown col;
        private System.Windows.Forms.Label Bomb_label;
        private System.Windows.Forms.NumericUpDown bomb;
        private System.Windows.Forms.Button button1;
    }
}
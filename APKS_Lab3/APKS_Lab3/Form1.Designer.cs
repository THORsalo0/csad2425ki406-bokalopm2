namespace APKS_Lab3
{
    partial class main_Form
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
            this.mod_comboBox = new System.Windows.Forms.ComboBox();
            this.mode_label = new System.Windows.Forms.Label();
            this.results_textBox = new System.Windows.Forms.TextBox();
            this.rock_button = new System.Windows.Forms.Button();
            this.paper_button = new System.Windows.Forms.Button();
            this.scissors_button = new System.Windows.Forms.Button();
            this.play_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mod_comboBox
            // 
            this.mod_comboBox.FormattingEnabled = true;
            this.mod_comboBox.Location = new System.Drawing.Point(12, 29);
            this.mod_comboBox.Name = "mod_comboBox";
            this.mod_comboBox.Size = new System.Drawing.Size(121, 21);
            this.mod_comboBox.TabIndex = 0;
            // 
            // mode_label
            // 
            this.mode_label.AutoSize = true;
            this.mode_label.Location = new System.Drawing.Point(12, 9);
            this.mode_label.Name = "mode_label";
            this.mode_label.Size = new System.Drawing.Size(64, 13);
            this.mode_label.TabIndex = 1;
            this.mode_label.Text = "Game mode";
            // 
            // results_textBox
            // 
            this.results_textBox.Location = new System.Drawing.Point(27, 77);
            this.results_textBox.Multiline = true;
            this.results_textBox.Name = "results_textBox";
            this.results_textBox.ReadOnly = true;
            this.results_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.results_textBox.Size = new System.Drawing.Size(736, 282);
            this.results_textBox.TabIndex = 2;
            // 
            // rock_button
            // 
            this.rock_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rock_button.Location = new System.Drawing.Point(27, 377);
            this.rock_button.Name = "rock_button";
            this.rock_button.Size = new System.Drawing.Size(214, 46);
            this.rock_button.TabIndex = 3;
            this.rock_button.Text = "Rock";
            this.rock_button.UseVisualStyleBackColor = false;
            this.rock_button.Click += new System.EventHandler(this.rock_button_Click);
            // 
            // paper_button
            // 
            this.paper_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.paper_button.Location = new System.Drawing.Point(290, 377);
            this.paper_button.Name = "paper_button";
            this.paper_button.Size = new System.Drawing.Size(214, 46);
            this.paper_button.TabIndex = 4;
            this.paper_button.Text = "Paper";
            this.paper_button.UseVisualStyleBackColor = true;
            this.paper_button.Click += new System.EventHandler(this.paper_button_Click);
            // 
            // scissors_button
            // 
            this.scissors_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.scissors_button.Location = new System.Drawing.Point(549, 377);
            this.scissors_button.Name = "scissors_button";
            this.scissors_button.Size = new System.Drawing.Size(214, 46);
            this.scissors_button.TabIndex = 5;
            this.scissors_button.Text = "Scissors";
            this.scissors_button.UseVisualStyleBackColor = true;
            this.scissors_button.Click += new System.EventHandler(this.scissors_button_Click);
            // 
            // play_button
            // 
            this.play_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.play_button.Location = new System.Drawing.Point(153, 27);
            this.play_button.Name = "play_button";
            this.play_button.Size = new System.Drawing.Size(75, 23);
            this.play_button.TabIndex = 6;
            this.play_button.Text = "Play";
            this.play_button.UseVisualStyleBackColor = true;
            this.play_button.Click += new System.EventHandler(this.play_button_Click);
            // 
            // stop_button
            // 
            this.stop_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stop_button.Location = new System.Drawing.Point(234, 27);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(75, 23);
            this.stop_button.TabIndex = 7;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.play_button);
            this.Controls.Add(this.scissors_button);
            this.Controls.Add(this.paper_button);
            this.Controls.Add(this.rock_button);
            this.Controls.Add(this.results_textBox);
            this.Controls.Add(this.mode_label);
            this.Controls.Add(this.mod_comboBox);
            this.Name = "main_Form";
            this.Text = "Rock - Paper - Scissors";
            this.Load += new System.EventHandler(this.main_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox mod_comboBox;
        public System.Windows.Forms.Label mode_label;
        public System.Windows.Forms.TextBox results_textBox;
        public System.Windows.Forms.Button rock_button;
        public System.Windows.Forms.Button paper_button;
        public System.Windows.Forms.Button scissors_button;
        public System.Windows.Forms.Button play_button;
        public System.Windows.Forms.Button stop_button;
    }
}


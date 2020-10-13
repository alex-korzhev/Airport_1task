namespace Airport_1task
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
            this.btn_open_file = new System.Windows.Forms.Button();
            this.lv_opened_files = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_load_files = new System.Windows.Forms.Button();
            this.lbl_opened_files = new System.Windows.Forms.Label();
            this.tb_main_text = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_open_file
            // 
            this.btn_open_file.Location = new System.Drawing.Point(0, 0);
            this.btn_open_file.Name = "btn_open_file";
            this.btn_open_file.Size = new System.Drawing.Size(207, 50);
            this.btn_open_file.TabIndex = 0;
            this.btn_open_file.Text = "Открыть файл";
            this.btn_open_file.UseVisualStyleBackColor = true;
            this.btn_open_file.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // lv_opened_files
            // 
            this.lv_opened_files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lv_opened_files.FullRowSelect = true;
            this.lv_opened_files.HideSelection = false;
            this.lv_opened_files.Location = new System.Drawing.Point(0, 79);
            this.lv_opened_files.MultiSelect = false;
            this.lv_opened_files.Name = "lv_opened_files";
            this.lv_opened_files.Size = new System.Drawing.Size(207, 173);
            this.lv_opened_files.TabIndex = 1;
            this.lv_opened_files.UseCompatibleStateImageBehavior = false;
            this.lv_opened_files.View = System.Windows.Forms.View.List;
            this.lv_opened_files.Click += new System.EventHandler(this.lv_opened_files_Click);
            this.lv_opened_files.DoubleClick += new System.EventHandler(this.lv_opened_files_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = -2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_load_files);
            this.panel1.Controls.Add(this.lbl_opened_files);
            this.panel1.Controls.Add(this.btn_open_file);
            this.panel1.Controls.Add(this.lv_opened_files);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 296);
            this.panel1.TabIndex = 2;
            // 
            // btn_load_files
            // 
            this.btn_load_files.Location = new System.Drawing.Point(0, 254);
            this.btn_load_files.Name = "btn_load_files";
            this.btn_load_files.Size = new System.Drawing.Size(207, 38);
            this.btn_load_files.TabIndex = 3;
            this.btn_load_files.Text = "Загрузить ранее открытые файлы";
            this.btn_load_files.UseVisualStyleBackColor = true;
            this.btn_load_files.Click += new System.EventHandler(this.btn_load_files_Click);
            // 
            // lbl_opened_files
            // 
            this.lbl_opened_files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_opened_files.Location = new System.Drawing.Point(3, 53);
            this.lbl_opened_files.Name = "lbl_opened_files";
            this.lbl_opened_files.Size = new System.Drawing.Size(201, 23);
            this.lbl_opened_files.TabIndex = 2;
            this.lbl_opened_files.Text = "Список открытых фалов:";
            this.lbl_opened_files.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_main_text
            // 
            this.tb_main_text.Location = new System.Drawing.Point(226, 12);
            this.tb_main_text.Multiline = true;
            this.tb_main_text.Name = "tb_main_text";
            this.tb_main_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_main_text.Size = new System.Drawing.Size(509, 296);
            this.tb_main_text.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 316);
            this.Controls.Add(this.tb_main_text);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Текстовые файлы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_open_file;
        private System.Windows.Forms.ListView lv_opened_files;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_main_text;
        private System.Windows.Forms.Label lbl_opened_files;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btn_load_files;
    }
}


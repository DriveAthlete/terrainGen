namespace Project_sharp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_warp = new System.Windows.Forms.CheckBox();
            this.trackBar_detail = new System.Windows.Forms.TrackBar();
            this.textBox_detail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_seed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_size = new System.Windows.Forms.TrackBar();
            this.textBox_size = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Generate = new System.Windows.Forms.Button();
            this.button_ShowBMP = new System.Windows.Forms.Button();
            this.button_3D = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_size)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_warp);
            this.groupBox1.Controls.Add(this.trackBar_detail);
            this.groupBox1.Controls.Add(this.textBox_detail);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_seed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.trackBar_size);
            this.groupBox1.Controls.Add(this.textBox_size);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(369, 152);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // checkBox_warp
            // 
            this.checkBox_warp.AutoSize = true;
            this.checkBox_warp.Location = new System.Drawing.Point(161, 96);
            this.checkBox_warp.Name = "checkBox_warp";
            this.checkBox_warp.Size = new System.Drawing.Size(92, 17);
            this.checkBox_warp.TabIndex = 9;
            this.checkBox_warp.Text = "Цикличность";
            this.checkBox_warp.UseVisualStyleBackColor = true;
            this.checkBox_warp.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // trackBar_detail
            // 
            this.trackBar_detail.Location = new System.Drawing.Point(173, 49);
            this.trackBar_detail.Name = "trackBar_detail";
            this.trackBar_detail.Size = new System.Drawing.Size(137, 45);
            this.trackBar_detail.TabIndex = 7;
            this.trackBar_detail.Scroll += new System.EventHandler(this.trackBar_detail_Scroll);
            // 
            // textBox_detail
            // 
            this.textBox_detail.Location = new System.Drawing.Point(284, 23);
            this.textBox_detail.Name = "textBox_detail";
            this.textBox_detail.ReadOnly = true;
            this.textBox_detail.Size = new System.Drawing.Size(48, 20);
            this.textBox_detail.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(158, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Коэфициент плавности";
            // 
            // textBox_seed
            // 
            this.textBox_seed.Location = new System.Drawing.Point(10, 116);
            this.textBox_seed.Name = "textBox_seed";
            this.textBox_seed.Size = new System.Drawing.Size(133, 20);
            this.textBox_seed.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Зерно карты";
            // 
            // trackBar_size
            // 
            this.trackBar_size.Location = new System.Drawing.Point(6, 52);
            this.trackBar_size.Name = "trackBar_size";
            this.trackBar_size.Size = new System.Drawing.Size(137, 45);
            this.trackBar_size.TabIndex = 2;
            this.trackBar_size.Scroll += new System.EventHandler(this.trackBar_size_Scroll);
            // 
            // textBox_size
            // 
            this.textBox_size.Location = new System.Drawing.Point(95, 26);
            this.textBox_size.Name = "textBox_size";
            this.textBox_size.ReadOnly = true;
            this.textBox_size.Size = new System.Drawing.Size(48, 20);
            this.textBox_size.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Размер карты";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_Save);
            this.groupBox2.Controls.Add(this.button_3D);
            this.groupBox2.Controls.Add(this.button_ShowBMP);
            this.groupBox2.Controls.Add(this.button_Generate);
            this.groupBox2.Location = new System.Drawing.Point(379, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 151);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Действия";
            // 
            // button_Generate
            // 
            this.button_Generate.Location = new System.Drawing.Point(6, 19);
            this.button_Generate.Name = "button_Generate";
            this.button_Generate.Size = new System.Drawing.Size(148, 23);
            this.button_Generate.TabIndex = 0;
            this.button_Generate.Text = "Генерация";
            this.button_Generate.UseVisualStyleBackColor = true;
            this.button_Generate.Click += new System.EventHandler(this.button_Generate_Click);
            // 
            // button_ShowBMP
            // 
            this.button_ShowBMP.Location = new System.Drawing.Point(6, 48);
            this.button_ShowBMP.Name = "button_ShowBMP";
            this.button_ShowBMP.Size = new System.Drawing.Size(148, 23);
            this.button_ShowBMP.TabIndex = 1;
            this.button_ShowBMP.Text = "Показать BMP";
            this.button_ShowBMP.UseVisualStyleBackColor = true;
            this.button_ShowBMP.Click += new System.EventHandler(this.button_ShowBMP_Click);
            // 
            // button_3D
            // 
            this.button_3D.Location = new System.Drawing.Point(6, 77);
            this.button_3D.Name = "button_3D";
            this.button_3D.Size = new System.Drawing.Size(148, 23);
            this.button_3D.TabIndex = 2;
            this.button_3D.Text = "Визуализировать в 3д";
            this.button_3D.UseVisualStyleBackColor = true;
            this.button_3D.Click += new System.EventHandler(this.button_3D_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(7, 106);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(148, 39);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Сохранить карту";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(545, 156);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Генератор карты";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_size)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_size;
        private System.Windows.Forms.TextBox textBox_size;
        private System.Windows.Forms.TextBox textBox_seed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar_detail;
        private System.Windows.Forms.TextBox textBox_detail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_warp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_3D;
        private System.Windows.Forms.Button button_ShowBMP;
        private System.Windows.Forms.Button button_Generate;
        private System.Windows.Forms.Button button_Save;
    }
}


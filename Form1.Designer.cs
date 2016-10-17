namespace WFMyApp
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
            this.btnHigh = new System.Windows.Forms.Button();
            this.btnNormal = new System.Windows.Forms.Button();
            this.btnLow = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHigh
            // 
            this.btnHigh.Location = new System.Drawing.Point(12, 12);
            this.btnHigh.Name = "btnHigh";
            this.btnHigh.Size = new System.Drawing.Size(75, 23);
            this.btnHigh.TabIndex = 0;
            this.btnHigh.Text = "High";
            this.btnHigh.UseVisualStyleBackColor = true;
            // 
            // btnNormal
            // 
            this.btnNormal.Location = new System.Drawing.Point(12, 41);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(75, 23);
            this.btnNormal.TabIndex = 1;
            this.btnNormal.Text = "Normal";
            this.btnNormal.UseVisualStyleBackColor = true;
            // 
            // btnLow
            // 
            this.btnLow.Location = new System.Drawing.Point(12, 70);
            this.btnLow.Name = "btnLow";
            this.btnLow.Size = new System.Drawing.Size(75, 23);
            this.btnLow.TabIndex = 2;
            this.btnLow.Text = "Low";
            this.btnLow.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(177, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 117);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnLow);
            this.Controls.Add(this.btnNormal);
            this.Controls.Add(this.btnHigh);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHigh;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.Button btnLow;
        private System.Windows.Forms.Button btnStop;
    }
}


namespace Storage
{
    partial class ServerForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelPort;
        private TextBox textBoxPort;
        private Button buttonStart;
        private Button buttonStop;
        private TextBox textBoxLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelPort = new Label();
            textBoxPort = new TextBox();
            buttonStart = new Button();
            buttonStop = new Button();
            textBoxLog = new TextBox();
            SuspendLayout();
            // 
            // labelPort
            // 
            labelPort.AutoSize = true;
            labelPort.Location = new Point(12, 15);
            labelPort.Name = "labelPort";
            labelPort.Size = new Size(35, 15);
            labelPort.TabIndex = 0;
            labelPort.Text = "Порт";
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(55, 12);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(100, 23);
            textBoxPort.TabIndex = 1;
            textBoxPort.Text = "5000";
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(175, 11);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(140, 25);
            buttonStart.TabIndex = 2;
            buttonStart.Text = "Запустить сервер";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(330, 11);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(145, 25);
            buttonStop.TabIndex = 3;
            buttonStop.Text = "Остановить сервер";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // textBoxLog
            // 
            textBoxLog.Location = new Point(12, 50);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.ScrollBars = ScrollBars.Vertical;
            textBoxLog.Size = new Size(760, 388);
            textBoxLog.TabIndex = 4;
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(textBoxLog);
            Controls.Add(buttonStop);
            Controls.Add(buttonStart);
            Controls.Add(textBoxPort);
            Controls.Add(labelPort);
            Name = "ServerForm";
            Text = "TCP сервер";
            FormClosing += ServerForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
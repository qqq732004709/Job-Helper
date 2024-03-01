namespace Job_Helper;

partial class Form1
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
        ChooseResumeDialog = new OpenFileDialog();
        SelectFile = new Button();
        FilePath = new Label();
        SaveKey = new Button();
        ApiKey = new TextBox();
        label1 = new Label();
        label2 = new Label();
        JobDescription = new TextBox();
        label3 = new Label();
        AiResume = new TextBox();
        label4 = new Label();
        FetchResume = new Button();
        CopyResume = new Button();
        SuspendLayout();
        // 
        // ChooseResumeDialog
        // 
        ChooseResumeDialog.FileName = "openFileDialog1";
        ChooseResumeDialog.FileOk += ChooseResumeDialog_FileOk;
        // 
        // SelectFile
        // 
        SelectFile.Location = new Point(323, 58);
        SelectFile.Name = "SelectFile";
        SelectFile.Size = new Size(102, 25);
        SelectFile.TabIndex = 0;
        SelectFile.Text = "选择简历";
        SelectFile.UseVisualStyleBackColor = true;
        SelectFile.Click += SelectFile_Click;
        // 
        // FilePath
        // 
        FilePath.AutoSize = true;
        FilePath.Location = new Point(122, 58);
        FilePath.Name = "FilePath";
        FilePath.Size = new Size(0, 17);
        FilePath.TabIndex = 1;
        FilePath.Click += FilePath_Click;
        // 
        // SaveKey
        // 
        SaveKey.Location = new Point(323, 14);
        SaveKey.Name = "SaveKey";
        SaveKey.Size = new Size(102, 25);
        SaveKey.TabIndex = 0;
        SaveKey.Text = "保存";
        SaveKey.UseVisualStyleBackColor = true;
        SaveKey.Click += SaveKey_Click;
        // 
        // ApiKey
        // 
        ApiKey.Location = new Point(122, 15);
        ApiKey.Name = "ApiKey";
        ApiKey.Size = new Size(172, 23);
        ApiKey.TabIndex = 2;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 98);
        label1.Name = "label1";
        label1.Size = new Size(104, 17);
        label1.TabIndex = 3;
        label1.Text = "请输入岗位需求：";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(12, 18);
        label2.Name = "label2";
        label2.Size = new Size(100, 17);
        label2.TabIndex = 3;
        label2.Text = "Open API Key：";
        // 
        // JobDescription
        // 
        JobDescription.Location = new Point(122, 98);
        JobDescription.Multiline = true;
        JobDescription.Name = "JobDescription";
        JobDescription.Size = new Size(172, 54);
        JobDescription.TabIndex = 4;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(12, 58);
        label3.Name = "label3";
        label3.Size = new Size(71, 17);
        label3.TabIndex = 1;
        label3.Text = "请选择文件:";
        // 
        // AiResume
        // 
        AiResume.Location = new Point(122, 198);
        AiResume.Multiline = true;
        AiResume.Name = "AiResume";
        AiResume.Size = new Size(172, 193);
        AiResume.TabIndex = 5;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(12, 198);
        label4.Name = "label4";
        label4.Size = new Size(56, 17);
        label4.TabIndex = 3;
        label4.Text = "AI简历：";
        // 
        // FetchResume
        // 
        FetchResume.Location = new Point(323, 198);
        FetchResume.Name = "FetchResume";
        FetchResume.Size = new Size(102, 25);
        FetchResume.TabIndex = 0;
        FetchResume.Text = "生成AI简历";
        FetchResume.UseVisualStyleBackColor = true;
        FetchResume.Click += FetchResume_Click;
        // 
        // CopyResume
        // 
        CopyResume.Location = new Point(323, 229);
        CopyResume.Name = "CopyResume";
        CopyResume.Size = new Size(102, 25);
        CopyResume.TabIndex = 0;
        CopyResume.Text = "复制内容";
        CopyResume.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(446, 403);
        Controls.Add(AiResume);
        Controls.Add(JobDescription);
        Controls.Add(label2);
        Controls.Add(label4);
        Controls.Add(label1);
        Controls.Add(ApiKey);
        Controls.Add(label3);
        Controls.Add(FilePath);
        Controls.Add(SaveKey);
        Controls.Add(CopyResume);
        Controls.Add(FetchResume);
        Controls.Add(SelectFile);
        Name = "Form1";
        Text = "工作助手";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private OpenFileDialog ChooseResumeDialog;
    private Button SelectFile;
    private Label FilePath;
    private Button SaveKey;
    private TextBox ApiKey;
    private Label label1;
    private Label label2;
    private TextBox JobDescription;
    private Label label3;
    private TextBox AiResume;
    private Label label4;
    private Button FetchResume;
    private Button CopyResume;
}

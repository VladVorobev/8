namespace FileManagerApp
{
    partial class Form1
    {
        private System.Windows.Forms.Button buttonBack;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewFiles;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonViewFiles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            buttonBack = new Button();
            listViewFiles = new ListView();
            buttonBrowse = new Button();
            buttonCopy = new Button();
            buttonMove = new Button();
            buttonDelete = new Button();
            textBoxPath = new TextBox();
            labelPath = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            buttonViewFiles = new Button();
            buttonOpenFile = new Button();
            SuspendLayout();
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.Teal;
            buttonBack.Cursor = Cursors.Hand;
            buttonBack.FlatStyle = FlatStyle.Flat;
            buttonBack.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonBack.Location = new Point(458, 375);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(100, 30);
            buttonBack.TabIndex = 8;
            buttonBack.Text = "Назад";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // listViewFiles
            // 
            listViewFiles.Columns.Add("Имя", 300);
            listViewFiles.Columns.Add("Тип", 100);
            listViewFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewFiles.Location = new Point(12, 41);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(673, 320);
            listViewFiles.TabIndex = 0;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            // 
            // buttonBrowse
            // 
            buttonBrowse.BackColor = Color.Red;
            buttonBrowse.Cursor = Cursors.Hand;
            buttonBrowse.FlatStyle = FlatStyle.Flat;
            buttonBrowse.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonBrowse.Location = new Point(603, 10);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(82, 25);
            buttonBrowse.TabIndex = 1;
            buttonBrowse.Text = "Обзор";
            buttonBrowse.UseVisualStyleBackColor = false;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // buttonCopy
            // 
            buttonCopy.BackColor = Color.Gold;
            buttonCopy.Cursor = Cursors.Hand;
            buttonCopy.FlatStyle = FlatStyle.Flat;
            buttonCopy.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonCopy.Location = new Point(12, 375);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new Size(122, 30);
            buttonCopy.TabIndex = 2;
            buttonCopy.Text = "Копировать";
            buttonCopy.UseVisualStyleBackColor = false;
            buttonCopy.Click += buttonCopy_Click;
            // 
            // buttonMove
            // 
            buttonMove.BackColor = Color.Green;
            buttonMove.Cursor = Cursors.Hand;
            buttonMove.FlatStyle = FlatStyle.Flat;
            buttonMove.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonMove.Location = new Point(140, 375);
            buttonMove.Name = "buttonMove";
            buttonMove.Size = new Size(100, 30);
            buttonMove.TabIndex = 3;
            buttonMove.Text = "Переместить";
            buttonMove.UseVisualStyleBackColor = false;
            buttonMove.Click += buttonMove_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.BackColor = Color.DarkGray;
            buttonDelete.Cursor = Cursors.Hand;
            buttonDelete.FlatStyle = FlatStyle.Flat;
            buttonDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonDelete.Location = new Point(246, 375);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(100, 30);
            buttonDelete.TabIndex = 4;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxPath.Location = new Point(65, 12);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(532, 23);
            textBoxPath.TabIndex = 5;
            // 
            // labelPath
            // 
            labelPath.AutoSize = true;
            labelPath.BackColor = Color.Transparent;
            labelPath.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelPath.Location = new Point(17, 14);
            labelPath.Name = "labelPath";
            labelPath.Size = new Size(42, 17);
            labelPath.TabIndex = 6;
            labelPath.Text = "Путь:";
            // 
            // buttonViewFiles
            // 
            buttonViewFiles.BackColor = Color.Orange;
            buttonViewFiles.Cursor = Cursors.Hand;
            buttonViewFiles.FlatStyle = FlatStyle.Flat;
            buttonViewFiles.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonViewFiles.Location = new Point(352, 375);
            buttonViewFiles.Name = "buttonViewFiles";
            buttonViewFiles.Size = new Size(100, 30);
            buttonViewFiles.TabIndex = 7;
            buttonViewFiles.Text = "Просмотр";
            buttonViewFiles.UseVisualStyleBackColor = true;
            buttonViewFiles.Click += buttonViewFiles_Click;
            // 
            // buttonOpenFile
            // 
            buttonOpenFile.BackColor = Color.SteelBlue;
            buttonOpenFile.Cursor = Cursors.Hand;
            buttonOpenFile.FlatStyle = FlatStyle.Flat;
            buttonOpenFile.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonOpenFile.Location = new Point(564, 375);
            buttonOpenFile.Name = "buttonOpenFile";
            buttonOpenFile.Size = new Size(121, 30);
            buttonOpenFile.TabIndex = 9;
            buttonOpenFile.Text = "Открыть файл";
            buttonOpenFile.UseVisualStyleBackColor = true;
            buttonOpenFile.Click += buttonOpenFile_Click;
            // 
            // Form1
            // 
            BackgroundImage = Properties.Resources.фон;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(697, 421);
            Controls.Add(buttonOpenFile);
            Controls.Add(buttonBack);
            Controls.Add(buttonViewFiles);
            Controls.Add(labelPath);
            Controls.Add(textBoxPath);
            Controls.Add(buttonDelete);
            Controls.Add(buttonMove);
            Controls.Add(buttonCopy);
            Controls.Add(buttonBrowse);
            Controls.Add(listViewFiles);
            Name = "Form1";
            Text = "Файловый Менеджер";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOpenFile;
    }
}
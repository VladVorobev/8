using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileManagerApp
{
    public partial class Form1 : Form
    {
        // ���� ��� �������� ����� (��� ����������� �������� �����)
        private Stack<string> pathHistory = new Stack<string>();

        // ���������� ��� �������� �������� ����
        private string currentPath = string.Empty;

        public Form1()
        {
            InitializeComponent();  // ������������� ����������� �����
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // �����, ������� ���������� ��� �������� ����� (���� ������ �� ������)
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            // ������� ������ ��� ������ �����
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // ��������� ������� ���� � ����, ���� �� ��� ���������� (�� ������ �����)
                if (!string.IsNullOrEmpty(currentPath))
                {
                    pathHistory.Push(currentPath);  // ��������� ������� ���� � ����
                }

                // ������������� ����� ���� (��������� �������������)
                currentPath = folderBrowserDialog1.SelectedPath;
                textBoxPath.Text = currentPath;  // ��������� ��������� ���� ��� ����������� ����

                // ��������� ����� � ����� ��� �������� ����
                LoadFilesAndDirectories(currentPath);
            }
            else
            {
                // ��������� �� ������, ���� ����� �� ���� �������
                MessageBox.Show("�� ������� ������� �����.");
            }
        }

        // ����� ��� �������� ������ � ����� �� ���������� ����
        private void LoadFilesAndDirectories(string path)
        {
            try
            {
                // ������� �������� ������ ������ � ����� � ListView
                listViewFiles.Items.Clear();

                // �������� ������ ���� ���������� (�����) � ��������� ����
                string[] directories = Directory.GetDirectories(path);

                // ���������� ������ ����� � ��������� � � ListView
                foreach (string directory in directories)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(directory));  // ��������� ��� �����
                    item.SubItems.Add("�����");  // ��������� ��� �������� (�����)
                    listViewFiles.Items.Add(item);  // ��������� ������� � ListView
                }

                // �������� ������ ���� ������ � ��������� �����
                string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

                // ���������� ������ ���� � ��������� ��� � ListView
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);  // �������� ��� ����� � �����������
                    string fileExtension = Path.GetExtension(file);  // �������� ���������� �����

                    ListViewItem item = new ListViewItem(fileName);  // ������� ������� � ������ �����
                    item.SubItems.Add(fileExtension);  // ��������� ���������� ����� ��� ����������

                    listViewFiles.Items.Add(item);  // ��������� ������� � ListView
                }

                // �������������� ���������� ����������� ListView
                listViewFiles.Refresh();
            }
            catch (Exception ex)
            {
                // ��������� �� ������, ���� �� ������� ��������� ����� � �����
                MessageBox.Show("������ ��� �������� ������ � �����: " + ex.Message);
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            // ��������, ������ �� ���� ��� ����� ��� �����������
            if (listViewFiles.SelectedItems.Count == 0)
            {
                // ���� ��� ���������� ��������, ������� ���������
                MessageBox.Show("����������, �������� ���� ��� ����� ��� �����������.");
                return;  // ��������� ���������� ������
            }

            // �������� ��� ���������� ����� ��� �����
            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // ��������� ������ ���� � ���������� ��������
            string sourcePath = Path.Combine(textBoxPath.Text, selectedItem);

            // ��������� ���������� ���� ��� ������ ����� ����������
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // ��������� ������ ���� ���������� ��� �����������
                    string destinationPath = Path.Combine(folderDialog.SelectedPath, selectedItem);

                    try
                    {
                        // ���� ��������� ����� � ��� ���������� (� �� ����)
                        if (Directory.Exists(sourcePath))
                        {
                            // ��������, ���������� �� ��� ����� �� ���������� ����
                            if (!Directory.Exists(destinationPath))
                            {
                                // �������� ����� ���������� (������� ����������)
                                DirectoryCopy(sourcePath, destinationPath);
                                MessageBox.Show("����� �����������.");
                            }
                            else
                            {
                                // ��������, ��� ����� ��� ���������� � ������� ��������
                                MessageBox.Show("����� ��� ���������� � ������� ��������.");
                            }
                        }
                        // ���� ��������� ������� � ��� ����
                        else if (File.Exists(sourcePath))
                        {
                            // ��������, ���������� �� ��� ���� �� ���������� ����
                            if (!File.Exists(destinationPath))
                            {
                                // �������� ����
                                File.Copy(sourcePath, destinationPath);
                                MessageBox.Show("���� ����������.");
                            }
                            else
                            {
                                // ��������, ��� ���� ��� ���������� � ������� ��������
                                MessageBox.Show("���� ��� ���������� � ������� ��������.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // ��������� ������ �����������, ����� ��������� �� ������
                        MessageBox.Show("������ ��� �����������: " + ex.Message);
                    }
                }
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            // ��������, ������ �� ���� ��� ����� ��� �����������
            if (listViewFiles.SelectedItems.Count == 0)
            {
                // ���� ������ �� �������, ������� ���������
                MessageBox.Show("����������, �������� ���� ��� ����� ��� �����������.");
                return;  // ��������� ���������� ������
            }

            // �������� ��� ���������� ����� ��� �����
            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // ��������� �������� ���� � ���������� ��������
            string sourcePath = Path.Combine(textBoxPath.Text, selectedItem);

            // ��������� ������ ��� ������ ����� ����������
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // ��������� ���� � �����/����� � ��������� ������� ����������
                    string destinationPath = Path.Combine(folderDialog.SelectedPath, selectedItem);
                    try
                    {
                        // ���� ��������� ���������� � ��� �����
                        if (Directory.Exists(sourcePath))
                        {
                            // ��������, ���������� �� ��� ����� � ������� ����������
                            if (!Directory.Exists(destinationPath))
                            {
                                // ���������� �����
                                Directory.Move(sourcePath, destinationPath);
                                MessageBox.Show("����� ����������.");
                            }
                            else
                            {
                                MessageBox.Show("����� ��� ���������� � ������� ��������.");
                            }
                        }
                        // ���� ��������� ������� � ��� ����
                        else if (File.Exists(sourcePath))
                        {
                            // ��������, ���������� �� ��� ���� � ������� ����������
                            if (!File.Exists(destinationPath))
                            {
                                // ���������� ����
                                File.Move(sourcePath, destinationPath);
                                MessageBox.Show("���� ���������.");
                            }
                            else
                            {
                                MessageBox.Show("���� ��� ���������� � ������� ��������.");
                            }
                        }

                        // ��������� ���������� ������� ����������
                        LoadFilesAndDirectories(textBoxPath.Text);
                    }
                    catch (Exception ex)
                    {
                        // ��������� ������ �����������
                        MessageBox.Show("������ ��� �����������: " + ex.Message);
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // ��������, ������ �� ���� ��� ����� ��� ��������
            if (listViewFiles.SelectedItems.Count == 0)
            {
                // ���� ������ �� �������, ������� ���������
                MessageBox.Show("����������, �������� ���� ��� ����� ��� ��������.");
                return;  // ��������� ���������� ������
            }

            // �������� ��� ���������� ����� ��� �����
            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // ��������� �������� ���� � ���������� ��������
            string sourcePath = Path.Combine(textBoxPath.Text, selectedItem);

            try
            {
                // ���� ��� �����
                if (Directory.Exists(sourcePath))
                {
                    // ������� ����� � ��� � ����������
                    Directory.Delete(sourcePath, true);
                    MessageBox.Show("����� �������.");
                }
                // ���� ��� ����
                else if (File.Exists(sourcePath))
                {
                    // ������� ����
                    File.Delete(sourcePath);
                    MessageBox.Show("���� ������.");
                }

                // ��������� ���������� ������� ����������
                LoadFilesAndDirectories(textBoxPath.Text);
            }
            catch (Exception ex)
            {
                // ��������� ������ ��������
                MessageBox.Show("������ ��� ��������: " + ex.Message);
            }
        }

        // ����������� ����������� �����
        private void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // ������� ������ DirectoryInfo ��� �������� �����
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            // �������� ��� �������� � �������� �����
            DirectoryInfo[] dirs = dir.GetDirectories();

            // ���������, ���������� �� ����� ����������, ���� ��� - ������� �
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // �������� ����� �� �������� ����� � ����� ����������
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                // ��������� ���� ��� ����������� ������� �����
                string tempPath = Path.Combine(destDirName, file.Name);
                // �������� ���� �� ���������� ����
                file.CopyTo(tempPath, false);
            }

            // ���������� �������� ��� �������� � �� ����������
            foreach (DirectoryInfo subdir in dirs)
            {
                // ��������� ���� ��� ������ ��������
                string tempPath = Path.Combine(destDirName, subdir.Name);
                // ���������� �������� ����� ����������� ��� ������ ��������
                DirectoryCopy(subdir.FullName, tempPath);
            }
        }

        // ���������� ������� �� ������ "��������"
        private void buttonViewFiles_Click(object sender, EventArgs e)
        {
            // ���������, ������ �� ������� � ListView
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // �������� ��������� �������
                var selectedItem = listViewFiles.SelectedItems[0];

                // ��������� ������ ���� � ���������� ��������
                string selectedPath = Path.Combine(textBoxPath.Text, selectedItem.Text);

                // ���������, �������� �� ��������� ������� ������
                if (Directory.Exists(selectedPath))
                {
                    // ��������� ������� ���� � ����� ��� ����������� �������� �����
                    pathHistory.Push(currentPath);

                    // ��������� ������� ���� �� ��������� �����
                    currentPath = selectedPath;

                    // ���������� ����������� ���� � ��������� ����
                    textBoxPath.Text = currentPath;

                    // ������� ������ ListView ����� ����������� ����� ���������
                    listViewFiles.Items.Clear();

                    // �������� ����� � ����� � ��������� ����������
                    string[] files = Directory.GetFiles(selectedPath);
                    string[] directories = Directory.GetDirectories(selectedPath);

                    // ��������� ����� � ListView
                    foreach (string directory in directories)
                    {
                        // ��������� ������� ��� ������ ����� � ������ "�����"
                        ListViewItem item = new ListViewItem(new string[] { Path.GetFileName(directory), "�����" });
                        listViewFiles.Items.Add(item);
                    }

                    // ��������� ����� � ListView � ��������� �� ����������
                    foreach (string file in files)
                    {
                        // �������� ���������� �����
                        string fileExtension = Path.GetExtension(file);

                        // ��������� ������� ��� ������� ����� � ��������� ����������
                        ListViewItem item = new ListViewItem(new string[] { Path.GetFileName(file), fileExtension });
                        listViewFiles.Items.Add(item);
                    }
                }
                else
                {
                    // ���� ��������� ������� �� �������� ������, ������� ��������� �� ������
                    MessageBox.Show("��������� ������� �� �������� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // ���� ������ �� �������, ������� ��������������
                MessageBox.Show("����������, �������� ����� ��� ���������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            // ���������, ���� �� ���� � ����� (���������� ���� ��������� � ����� pathHistory)
            if (pathHistory.Count > 0)
            {
                // ��������� ���������� ���� �� ����� (������������ � ���������� ����������)
                currentPath = pathHistory.Pop();

                // ��������� ��������� ���� � ������� �����
                textBoxPath.Text = currentPath;

                // ��������� ����� � ����� ��� ����������� ����
                LoadFilesAndDirectories(currentPath);
            }
            else
            {
                // ���� � ����� ��� ���������� �����, ������� ��������������
                MessageBox.Show("��� ���������� �����.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            // ���������, ������ �� ������� � ListView
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // ��������� ������ ���� � ���������� �����
                string selectedFile = Path.Combine(textBoxPath.Text, listViewFiles.SelectedItems[0].Text);

                // ���������, ���������� �� ����
                if (File.Exists(selectedFile))
                {
                    try
                    {
                        // ������� ������ ProcessStartInfo ��� �������� �����
                        ProcessStartInfo processStartInfo = new ProcessStartInfo
                        {
                            FileName = selectedFile,  // ���� � �����
                            UseShellExecute = true,   // ���������� �������� Windows ��� �������� �����
                            Verb = "open"             // ������� ���� � ������� ��������� �� ���������
                        };

                        // ��������� ������� ��� �������� �����
                        Process.Start(processStartInfo);
                    }
                    catch (Exception ex)
                    {
                        // ��������� ������ ��� �������� �����
                        MessageBox.Show("�� ������� ������� ����: " + ex.Message);
                    }
                }
                else
                {
                    // ���� ���� �� ������, ������� ���������
                    MessageBox.Show("���� �� ����������: " + selectedFile);
                }
            }
            else
            {
                // ���� ���� �� ������, ������� ��������������
                MessageBox.Show("����������, �������� ���� ��� ��������.");
            }
        }
    }
}

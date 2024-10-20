using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FileManagerApp
{
    public partial class Form1 : Form
    {
        // Стек для хранения путей (для возможности возврата назад)
        private Stack<string> pathHistory = new Stack<string>();

        // Переменная для хранения текущего пути
        private string currentPath = string.Empty;

        public Form1()
        {
            InitializeComponent();  // Инициализация компонентов формы
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Метод, который вызывается при загрузке формы (пока ничего не делает)
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            // Открыть диалог для выбора папки
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                // Сохраняем текущий путь в стек, если он уже установлен (не первый выбор)
                if (!string.IsNullOrEmpty(currentPath))
                {
                    pathHistory.Push(currentPath);  // Добавляем текущий путь в стек
                }

                // Устанавливаем новый путь (выбранный пользователем)
                currentPath = folderBrowserDialog1.SelectedPath;
                textBoxPath.Text = currentPath;  // Обновляем текстовое поле для отображения пути

                // Загружаем файлы и папки для текущего пути
                LoadFilesAndDirectories(currentPath);
            }
            else
            {
                // Сообщение об ошибке, если папка не была выбрана
                MessageBox.Show("Не удалось выбрать папку.");
            }
        }

        // Метод для загрузки файлов и папок по указанному пути
        private void LoadFilesAndDirectories(string path)
        {
            try
            {
                // Очистка текущего списка файлов и папок в ListView
                listViewFiles.Items.Clear();

                // Получаем список всех директорий (папок) в указанном пути
                string[] directories = Directory.GetDirectories(path);

                // Перебираем каждую папку и добавляем её в ListView
                foreach (string directory in directories)
                {
                    ListViewItem item = new ListViewItem(Path.GetFileName(directory));  // Добавляем имя папки
                    item.SubItems.Add("Папка");  // Добавляем тип элемента (Папка)
                    listViewFiles.Items.Add(item);  // Добавляем элемент в ListView
                }

                // Получаем список всех файлов в указанной папке
                string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

                // Перебираем каждый файл и добавляем его в ListView
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);  // Получаем имя файла с расширением
                    string fileExtension = Path.GetExtension(file);  // Получаем расширение файла

                    ListViewItem item = new ListViewItem(fileName);  // Создаем элемент с именем файла
                    item.SubItems.Add(fileExtension);  // Добавляем расширение файла как подэлемент

                    listViewFiles.Items.Add(item);  // Добавляем элемент в ListView
                }

                // Принудительное обновление отображения ListView
                listViewFiles.Refresh();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке, если не удалось загрузить файлы и папки
                MessageBox.Show("Ошибка при загрузке файлов и папок: " + ex.Message);
            }
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            // Проверка, выбран ли файл или папка для копирования
            if (listViewFiles.SelectedItems.Count == 0)
            {
                // Если нет выбранного элемента, выводим сообщение
                MessageBox.Show("Пожалуйста, выберите файл или папку для копирования.");
                return;  // Прерываем выполнение метода
            }

            // Получаем имя выбранного файла или папки
            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // Формируем полный путь к выбранному элементу
            string sourcePath = Path.Combine(textBoxPath.Text, selectedItem);

            // Открываем диалоговое окно для выбора папки назначения
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Формируем полный путь назначения для копирования
                    string destinationPath = Path.Combine(folderDialog.SelectedPath, selectedItem);

                    try
                    {
                        // Если выбранная папка — это директория (а не файл)
                        if (Directory.Exists(sourcePath))
                        {
                            // Проверка, существует ли уже папка по указанному пути
                            if (!Directory.Exists(destinationPath))
                            {
                                // Копируем папку рекурсивно (включая содержимое)
                                DirectoryCopy(sourcePath, destinationPath);
                                MessageBox.Show("Папка скопирована.");
                            }
                            else
                            {
                                // Сообщаем, что папка уже существует в целевом каталоге
                                MessageBox.Show("Папка уже существует в целевом каталоге.");
                            }
                        }
                        // Если выбранный элемент — это файл
                        else if (File.Exists(sourcePath))
                        {
                            // Проверка, существует ли уже файл по указанному пути
                            if (!File.Exists(destinationPath))
                            {
                                // Копируем файл
                                File.Copy(sourcePath, destinationPath);
                                MessageBox.Show("Файл скопирован.");
                            }
                            else
                            {
                                // Сообщаем, что файл уже существует в целевом каталоге
                                MessageBox.Show("Файл уже существует в целевом каталоге.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок копирования, вывод сообщения об ошибке
                        MessageBox.Show("Ошибка при копировании: " + ex.Message);
                    }
                }
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            // Проверка, выбран ли файл или папка для перемещения
            if (listViewFiles.SelectedItems.Count == 0)
            {
                // Если ничего не выбрано, выводим сообщение
                MessageBox.Show("Пожалуйста, выберите файл или папку для перемещения.");
                return;  // Прерываем выполнение метода
            }

            // Получаем имя выбранного файла или папки
            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // Формируем исходный путь к выбранному элементу
            string sourcePath = Path.Combine(textBoxPath.Text, selectedItem);

            // Открываем диалог для выбора папки назначения
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Формируем путь к файлу/папке в выбранной целевой директории
                    string destinationPath = Path.Combine(folderDialog.SelectedPath, selectedItem);
                    try
                    {
                        // Если выбранная директория — это папка
                        if (Directory.Exists(sourcePath))
                        {
                            // Проверка, существует ли уже папка в целевой директории
                            if (!Directory.Exists(destinationPath))
                            {
                                // Перемещаем папку
                                Directory.Move(sourcePath, destinationPath);
                                MessageBox.Show("Папка перемещена.");
                            }
                            else
                            {
                                MessageBox.Show("Папка уже существует в целевом каталоге.");
                            }
                        }
                        // Если выбранный элемент — это файл
                        else if (File.Exists(sourcePath))
                        {
                            // Проверка, существует ли уже файл в целевой директории
                            if (!File.Exists(destinationPath))
                            {
                                // Перемещаем файл
                                File.Move(sourcePath, destinationPath);
                                MessageBox.Show("Файл перемещен.");
                            }
                            else
                            {
                                MessageBox.Show("Файл уже существует в целевом каталоге.");
                            }
                        }

                        // Обновляем содержимое текущей директории
                        LoadFilesAndDirectories(textBoxPath.Text);
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибки перемещения
                        MessageBox.Show("Ошибка при перемещении: " + ex.Message);
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            // Проверка, выбран ли файл или папка для удаления
            if (listViewFiles.SelectedItems.Count == 0)
            {
                // Если ничего не выбрано, выводим сообщение
                MessageBox.Show("Пожалуйста, выберите файл или папку для удаления.");
                return;  // Прерываем выполнение метода
            }

            // Получаем имя выбранного файла или папки
            string selectedItem = listViewFiles.SelectedItems[0].Text;

            // Формируем исходный путь к выбранному элементу
            string sourcePath = Path.Combine(textBoxPath.Text, selectedItem);

            try
            {
                // Если это папка
                if (Directory.Exists(sourcePath))
                {
                    // Удаляем папку и все её содержимое
                    Directory.Delete(sourcePath, true);
                    MessageBox.Show("Папка удалена.");
                }
                // Если это файл
                else if (File.Exists(sourcePath))
                {
                    // Удаляем файл
                    File.Delete(sourcePath);
                    MessageBox.Show("Файл удален.");
                }

                // Обновляем содержимое текущей директории
                LoadFilesAndDirectories(textBoxPath.Text);
            }
            catch (Exception ex)
            {
                // Обработка ошибок удаления
                MessageBox.Show("Ошибка при удалении: " + ex.Message);
            }
        }

        // Рекурсивное копирование папки
        private void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // Создаем объект DirectoryInfo для исходной папки
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            // Получаем все подпапки в исходной папке
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Проверяем, существует ли папка назначения, если нет - создаем её
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Копируем файлы из исходной папки в папку назначения
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                // Формируем путь для копирования каждого файла
                string tempPath = Path.Combine(destDirName, file.Name);
                // Копируем файл по указанному пути
                file.CopyTo(tempPath, false);
            }

            // Рекурсивно копируем все подпапки и их содержимое
            foreach (DirectoryInfo subdir in dirs)
            {
                // Формируем путь для каждой подпапки
                string tempPath = Path.Combine(destDirName, subdir.Name);
                // Рекурсивно вызываем метод копирования для каждой подпапки
                DirectoryCopy(subdir.FullName, tempPath);
            }
        }

        // Обработчик нажатия на кнопку "Просмотр"
        private void buttonViewFiles_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли элемент в ListView
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // Получаем выбранный элемент
                var selectedItem = listViewFiles.SelectedItems[0];

                // Формируем полный путь к выбранному элементу
                string selectedPath = Path.Combine(textBoxPath.Text, selectedItem.Text);

                // Проверяем, является ли выбранный элемент папкой
                if (Directory.Exists(selectedPath))
                {
                    // Сохраняем текущий путь в стеке для возможности возврата назад
                    pathHistory.Push(currentPath);

                    // Обновляем текущий путь до выбранной папки
                    currentPath = selectedPath;

                    // Отображаем обновленный путь в текстовом поле
                    textBoxPath.Text = currentPath;

                    // Очищаем список ListView перед добавлением новых элементов
                    listViewFiles.Items.Clear();

                    // Получаем файлы и папки в выбранной директории
                    string[] files = Directory.GetFiles(selectedPath);
                    string[] directories = Directory.GetDirectories(selectedPath);

                    // Добавляем папки в ListView
                    foreach (string directory in directories)
                    {
                        // Добавляем элемент для каждой папки с меткой "Папка"
                        ListViewItem item = new ListViewItem(new string[] { Path.GetFileName(directory), "Папка" });
                        listViewFiles.Items.Add(item);
                    }

                    // Добавляем файлы в ListView с указанием их расширений
                    foreach (string file in files)
                    {
                        // Получаем расширение файла
                        string fileExtension = Path.GetExtension(file);

                        // Добавляем элемент для каждого файла с указанием расширения
                        ListViewItem item = new ListViewItem(new string[] { Path.GetFileName(file), fileExtension });
                        listViewFiles.Items.Add(item);
                    }
                }
                else
                {
                    // Если выбранный элемент не является папкой, выводим сообщение об ошибке
                    MessageBox.Show("Выбранный элемент не является папкой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Если ничего не выбрано, выводим предупреждение
                MessageBox.Show("Пожалуйста, выберите папку для просмотра.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            // Проверяем, есть ли пути в стеке (предыдущие пути сохранены в стеке pathHistory)
            if (pathHistory.Count > 0)
            {
                // Извлекаем предыдущий путь из стека (возвращаемся к предыдущей директории)
                currentPath = pathHistory.Pop();

                // Обновляем текстовое поле с текущим путем
                textBoxPath.Text = currentPath;

                // Загружаем файлы и папки для предыдущего пути
                LoadFilesAndDirectories(currentPath);
            }
            else
            {
                // Если в стеке нет предыдущих путей, выводим предупреждение
                MessageBox.Show("Нет предыдущей папки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            // Проверяем, выбран ли элемент в ListView
            if (listViewFiles.SelectedItems.Count > 0)
            {
                // Формируем полный путь к выбранному файлу
                string selectedFile = Path.Combine(textBoxPath.Text, listViewFiles.SelectedItems[0].Text);

                // Проверяем, существует ли файл
                if (File.Exists(selectedFile))
                {
                    try
                    {
                        // Создаем объект ProcessStartInfo для открытия файла
                        ProcessStartInfo processStartInfo = new ProcessStartInfo
                        {
                            FileName = selectedFile,  // Путь к файлу
                            UseShellExecute = true,   // Используем оболочку Windows для открытия файла
                            Verb = "open"             // Открыть файл с помощью программы по умолчанию
                        };

                        // Запускаем процесс для открытия файла
                        Process.Start(processStartInfo);
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок при открытии файла
                        MessageBox.Show("Не удалось открыть файл: " + ex.Message);
                    }
                }
                else
                {
                    // Если файл не найден, выводим сообщение
                    MessageBox.Show("Файл не существует: " + selectedFile);
                }
            }
            else
            {
                // Если файл не выбран, выводим предупреждение
                MessageBox.Show("Пожалуйста, выберите файл для открытия.");
            }
        }
    }
}

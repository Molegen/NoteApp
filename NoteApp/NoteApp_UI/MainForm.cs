using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using NoteApp;

namespace NoteApp_UI
{
    public partial class MainForm : Form
    {
        ///<summary>
        ///Список заметок
        /// </summary>
        private readonly NoteList _noteList = new NoteList();

        private readonly Serialization _serialization = new Serialization();

        public MainForm()
        {
            InitializeComponent();
            notesCategory.SelectedItem = Category.All;
            foreach (Category category in Enum.GetValues(typeof(Category)))
            {
                notesCategory.Items.Add(category);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save changes?",
                "Exit",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveNotes(@"d:\notes.json"); // Сохраняем данные при закрытии формы
            }
            else if (result == DialogResult.Cancel) // Обработка Cancel
            {
                e.Cancel = true; // Отменяем закрытие формы
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNotes(@"d:\notes.json"); // Загружаем данные при загрузке формы
            notesCategory.SelectedIndex = 0;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNote();
            SaveNotes(@"d:\notes.json");
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNote();
            SaveNotes(@"d:\notes.json");
        }
        
        private void removeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutForm(); //Создаем форму About
            about.ShowDialog();
        }

        // Действие при смене категории
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryChange();
        }

        /// <summary>
        /// Создать заметку
        /// </summary>
        public void AddNote()
        {
            Note newNote = new Note();
            newNote.Id = Guid.NewGuid();
            newNote.Name = "Новая заметка";
            newNote.Category = "Other";
            newNote.LastUpdate = DateTime.Now;
            newNote.CreateTime = DateTime.Now;
            var edit = new EditForm(); //Создаем форму 
            edit.Note = newNote; //Передаем форме данные
            edit.ShowDialog(); //Отображаем форму для редактирования
            if (edit.DialogResult == DialogResult.OK) //При нажатии ок на форме Edit создаем новую заметку
            {
                var updatedNote = edit.Note;
                _noteList.Notes.Add(updatedNote);
                UpdateNotesListBox();
                CategoryChange();
                notesListBox.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// Редактировать заметку
        /// </summary>
        public void EditNote()
        {
            if (notesListBox.SelectedIndex == -1)
            {
                // Если ничего не выбрано, выводим предупреждение
                MessageBox.Show("Select note before editing");
                return;
            }
            //Получаем текущую выбранную заметку
            var selectedIndex = notesListBox.SelectedIndex;
            var selectedNote = _noteList.FilteredNotes[selectedIndex];
            var edit = new EditForm(); //Создаем форму 
            edit.Note = selectedNote; //Передаем форме данные
            edit.ShowDialog(); //Отображаем форму для редактирования
            if (edit.DialogResult == DialogResult.OK) //При нажатии ок на форме Edit обновляем список
            {
                var updatedNote = edit.Note; //Забираем измененные данные
                //Осталось удалить старые данные по выбранному индексу
                // и заменить их на обновленные
                int originalIndex = _noteList.Notes.IndexOf(selectedNote);
                _noteList.Notes.RemoveAt(originalIndex);
                _noteList.Notes.Insert(originalIndex, updatedNote);
                UpdateNotesListBox();
                CategoryChange();
                notesListBox.SelectedIndex = 0;
            }
            else notesListBox.SelectedIndex = selectedIndex;
        }
        /// <summary>
        /// Удалить заметку
        /// </summary>
        public void RemoveNote()
        {
            // Получаем индексы выбранных заметок
            List<int> selectedIndices = notesListBox.SelectedIndices.Cast<int>().ToList();

            if (!selectedIndices.Any())
            {
                return; // Ничего не выбрано, выход из функции
            }

            // Формируем сообщение в зависимости от количества выбранных заметок
            string message = selectedIndices.Count == 1
                ? $"Do you really want to remove this note: {_noteList.FilteredNotes[selectedIndices[0]].Name}?"
                : $"Are you sure you want to delete {selectedIndices.Count} notes?";

            DialogResult result = MessageBox.Show(message, "Delete Notes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // Сортируем индексы в обратном порядке, чтобы удаление не влияло на последующие индексы
                selectedIndices.Sort((a, b) => b.CompareTo(a));

                // Проходимся по всем выбранным заметкам и удаляем их из обоих списков
                foreach (int index in selectedIndices)
                {
                    Note note = _noteList.FilteredNotes[index]; // Получаем заметку по индексу из FilteredNotes

                    // Находим заметку по Id в основном списке _note
                    int noteIndex = _noteList.Notes.FindIndex(n => n.Id == note.Id);

                    if (noteIndex != -1)
                    {
                        _noteList.Notes.RemoveAt(noteIndex); // Удаляем заметку из основного списка _note
                    }

                    // Удаляем заметку из _noteList.FilteredNotes
                    _noteList.FilteredNotes.RemoveAt(index);
                }

                // Обновляем список заметок
                UpdateNotesListBox();

                // Если остались заметки, выбираем первую
                if (_noteList.FilteredNotes.Count > 0)
                {
                    notesListBox.SelectedIndex = 0;
                }
                else
                {
                    ClearTextForms();
                }
            }
        }

        /// <summary>
        /// Изменить категорию
        /// </summary>
        public void CategoryChange()
        {
            if (notesCategory.SelectedItem != null)
            {
                Category selectedCategory = (Category)notesCategory.SelectedItem;

                // Очищаем _noteList.FilteredNotes
                _noteList.FilteredNotes.Clear();

                // Фильтруем заметки
                switch (selectedCategory)
                {
                    case Category.All:
                        // Заполняем _noteList.FilteredNotes всеми заметками
                        _noteList.FilteredNotes.AddRange(_noteList.Notes); // Важно!
                        break;
                    default:
                        // Фильтруем заметки по категории
                        _noteList.FilteredNotes.AddRange(_noteList.Notes.Where(n =>
                        {
                            Category category;
                            if (Enum.TryParse<Category>(n.Category, out category))
                            {
                                return category == selectedCategory;
                            }
                            else
                            {
                                // Обработка некорректного значения
                                return false;
                            }
                        })); // Важно!
                        break;
                }

                // Обновляем список заметок в notesListBox
                UpdateNotesListBox();
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void notesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, что элемент выбран
            if (notesListBox.SelectedIndex != -1)
            {
                selectedTitleTextBox.Text = _noteList.FilteredNotes[notesListBox.SelectedIndex].Name;
                noteTextBox.Text = _noteList.FilteredNotes[notesListBox.SelectedIndex].Text;
                selectedCategoryNameTextBox.Text = _noteList.FilteredNotes[notesListBox.SelectedIndex].Category;
                lastUpdateSelectedTextBox.Text = _noteList.FilteredNotes[notesListBox.SelectedIndex].LastUpdate.ToShortDateString();
                createSelectedTextBox.Text = _noteList.FilteredNotes[notesListBox.SelectedIndex].CreateTime.ToShortDateString();
            }
        }

        private void addPictureBox_Click(object sender, EventArgs e)
        {
            AddNote();
            SaveNotes(@"d:\notes.json");
        }
        private void addPictureBox_MouseHover(object sender, EventArgs e)
        {
            // Создайте объект ToolTip
            ToolTip toolTip = new ToolTip();
            // Задайте текст подсказки
            toolTip.SetToolTip(addPictureBox, "Добавить заметку");
        }
        private void editPictureBox_Click(object sender, EventArgs e)
        {
            EditNote();
            SaveNotes(@"d:\notes.json");
        }
        private void editPictureBox_MouseHover(object sender, EventArgs e)
        {
            // Создайте объект ToolTip
            ToolTip toolTip = new ToolTip();
            // Задайте текст подсказки
            toolTip.SetToolTip(editPictureBox, "Изменить заметку");
        }
        private void removePictureBox_Click(object sender, EventArgs e)
        {
            RemoveNote();
        }
        private void removePictureBox_MouseHover(object sender, EventArgs e)
        {
            // Создайте объект ToolTip
            ToolTip toolTip = new ToolTip();
            // Задайте текст подсказки
            toolTip.SetToolTip(removePictureBox, "Удалить заметку");
        }
        private void noteTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Метод для очистки текстовых форм
        /// </summary>
        private void ClearTextForms()
        {
            selectedTitleTextBox.Clear();
            noteTextBox.Clear();
            selectedCategoryNameTextBox.Clear();
            lastUpdateSelectedTextBox.Clear();
            createSelectedTextBox.Clear();
        }
        /// <summary>
        /// Метод для обновления отоброжаемого списка заметок
        /// </summary>
        public void UpdateNotesListBox()
        {
            notesListBox.Items.Clear(); // Очищаем ListBox
            // Сортируем заметки по дате изменения (новые сверху)
            _noteList.FilteredNotes.Sort((x, y) => y.LastUpdate.CompareTo(x.LastUpdate));
            foreach (var note in _noteList.FilteredNotes)
            {
                string name = note.Name;
                string lastUpdateD = note.LastUpdate.ToString("dd.MM.yyyy");
                string lastUpdateH = note.LastUpdate.ToString("HH:mm");
                notesListBox.Items.Add(name);
            }
        }

        /// <summary>
        /// Сериализовать заметки
        /// </summary>
        private void SaveNotes(string filePath)
        {
            _serialization.SerializeNotesToFile(filePath, _noteList.Notes);
        }
        /// <summary>
        /// Десериализовать заметки
        /// </summary>
        private void LoadNotes(string filePath)
        {
            var notes = _serialization.DeserializeNotesFromFile(filePath);
            _noteList.SetNotes(notes);
            UpdateNotesListBox();
        }

        private void selectedCategoryNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectedCategoryTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastUpdateSelectedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectedTitleNameTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void selectedTitleTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusGroupBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void createTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void createSelectedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

    }
    
}

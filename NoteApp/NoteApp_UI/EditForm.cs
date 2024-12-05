﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp;

namespace NoteApp_UI
{
    public partial class EditForm : Form
    {
        private Note _note;
        public Note Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                if (_note != null)
                {
                    noteNameTextBox.Text = _note.Name;
                    NoteTextBox.Text = _note.Text;
                    editNotesCategory.Text = _note.Category;
                    createSelectedTextBox.Text = _note.CreateTime.ToShortDateString();
                    lastUpdateSelectedTextBox.Text = _note.LastUpdate.ToShortDateString();
                    _originalText = NoteTextBox.Text; // Сохраняем исходный текст при загрузке формы
                    _originalCategory = editNotesCategory.Text; // Сохраняем исходную категорию при загрузке формы
                }
            }
        }
        public EditForm()
        {
            InitializeComponent();
            this.Text = "Редактор";
            foreach (Category category in Enum.GetValues(typeof(Category))
                .Cast<Category>() // Преобразуем Enum в коллекцию Category
                .Where(c => c != Category.All)) // Фильтруем, чтобы удалить Category.All
            {
                editNotesCategory.Items.Add(category);
            }
            editNotesCategory.SelectedIndex = 0; // Установка начального значения
        }

        private string _originalText; // Добавлено поле для хранения исходного текста
        private string _originalCategory; // Добавлено поле для хранения исходного текста

        private void NoteTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            try
            {
                _note.Name = noteNameTextBox.Text;
                _note.Text = NoteTextBox.Text;
                _note.LastUpdate = DateTime.Now;
                _note.Category = editNotesCategory.Text;

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (NoteTextBox.Text != _originalText || editNotesCategory.Text != _originalCategory) // Условие: были ли текст или категория изменены
            {
                DialogResult result = MessageBox.Show("Close without saving?",
                    "Close",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    DialogResult = DialogResult.Cancel; // Закрытие формы
                    this.Close(); // Закрытие формы
                }
                else if (result == DialogResult.Cancel) // Обработка Cancel
                {
                    return; // Выходим из обработчика, чтобы не закрывать форму
                }
            }

            DialogResult = DialogResult.Cancel; // Закрытие формы
            this.Close(); // Закрытие формы
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
            NoteTextBox.SelectionStart = NoteTextBox.Text.Length;
        }

        private void noteNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void enterNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void selectCategoryTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void editNotesCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void createTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateTimeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void createSelectedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastUpdateSelectedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
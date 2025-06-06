using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laba_9.Models;



namespace Laba_9.Views
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public string Directory1
        {
            get => txtDir1.Text;
            set => txtDir1.Text = value;
        }

        public string Directory2
        {
            get => txtDir2.Text;
            set => txtDir2.Text = value;
        }

        public void SetDifferences(List<FileDifference> differences)
        {
            lstDifferences.Items.Clear();
            foreach (var diff in differences)
            {
                lstDifferences.Items.Add(diff.ToString());
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowLog(List<LogEntry> entries)
        {
            var logForm = new Form();
            var listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                FormattingEnabled = true
            };

            foreach (var entry in entries)
            {
                listBox.Items.Add($"[{entry.Timestamp}] {entry.FileName} - {entry.ChangeType}");
            }

            logForm.Controls.Add(listBox);
            logForm.Size = new System.Drawing.Size(600, 400);
            logForm.Text = "Журнал синхронизации";
            logForm.ShowDialog();
        }

        private void btnBrowseDir1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Directory1 = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnBrowseDir2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Directory2 = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Directory1) || string.IsNullOrEmpty(Directory2))
            {
                ShowMessage("Пожалуйста, выберите обе директории");
                return;
            }

            var presenter = this.Tag as Presenters.MainPresenter;
            presenter?.CompareDirectories();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Directory1) || string.IsNullOrEmpty(Directory2))
            {
                ShowMessage("Пожалуйста, выберите обе директории");
                return;
            }

            var presenter = this.Tag as Presenters.MainPresenter;
            presenter?.SynchronizeDirectories();
        }

        private void btnViewXmlLog_Click(object sender, EventArgs e)
        {
            var presenter = this.Tag as Presenters.MainPresenter;
            presenter?.ViewXmlLog();
        }

        private void btnViewJsonLog_Click(object sender, EventArgs e)
        {
            var presenter = this.Tag as Presenters.MainPresenter;
            presenter?.ViewJsonLog();
        }
    }
}

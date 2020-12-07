using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WordpadKiller
{
    public partial class Form1 : Form
    {
        bool isBold = false;
        bool isItalic = false;
        bool isUnderline = false;

        bool isChanged = false;

        Color foreColor = Color.Black;
        Color backColor = Color.White;

        string filename = "";

        public Form1()
        {
            InitializeComponent();

            foreach (var item in System.Drawing.FontFamily.Families)
            {
                tsComboBox_Font.Items.Add(item.Name);
            }
            NewIboWordPad();
        }

        private void NewIboWordPad()
        {
            filename = "";

            richTextBox1.ForeColor = foreColor = Color.Black;
            richTextBox1.BackColor = backColor = Color.White;
            
            tsComboBox_Font.SelectedIndex = 25;
            tsComboBox_FontSize.SelectedIndex = 6;

            richTextBox1.Text = "";
            richTextBox1.SelectionFont = new System.Drawing.Font(
                tsComboBox_Font.SelectedItem.ToString(),
                float.Parse(tsComboBox_FontSize.SelectedItem.ToString()),
                System.Drawing.FontStyle.Regular);

            isChanged = false;
        }

        private void ChangeRichTextBoxProperty()
        {
            if (isBold == true)
                richTextBox1.SelectionFont = new System.Drawing.Font(
                tsComboBox_Font.SelectedItem.ToString(),
                float.Parse(tsComboBox_FontSize.SelectedItem.ToString()),
                System.Drawing.FontStyle.Bold);
            else if (isItalic == true)
                richTextBox1.SelectionFont = new System.Drawing.Font(
                tsComboBox_Font.SelectedItem.ToString(),
                float.Parse(tsComboBox_FontSize.SelectedItem.ToString()),
                System.Drawing.FontStyle.Italic);
            else if (isUnderline == true)
                richTextBox1.SelectionFont = new System.Drawing.Font(
                tsComboBox_Font.SelectedItem.ToString(),
                float.Parse(tsComboBox_FontSize.SelectedItem.ToString()),
                System.Drawing.FontStyle.Underline);
            else
                richTextBox1.SelectionFont = new System.Drawing.Font(
                tsComboBox_Font.SelectedItem.ToString(),
                float.Parse(tsComboBox_FontSize.SelectedItem.ToString()),
                System.Drawing.FontStyle.Regular);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isChanged = true;
            MatchCollection wordColl = Regex.Matches(richTextBox1.Text, @"[\W]+");
            toolStripStatusLabel1.Text = $"{wordColl.Count} words";
            MatchCollection symbolColl = Regex.Matches(richTextBox1.Text, @".");
            toolStripStatusLabel2.Text = $"{symbolColl.Count} symbols";            
        }

        private void tsButton_AlignLeft_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tsButton_AlignCenter_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tsButton_AlignRight_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void tsButton_Bold_Click(object sender, EventArgs e)
        {
            if (isBold == true)
            {
                isBold = false;
                tsButton_Bold.BackColor = Color.FromName("ActiveBorder");
            }
            else
            {
                isBold = true;
                tsButton_Bold.BackColor = Color.FromName("ControlDarkDark");
            }
            isItalic = false;
            isUnderline = false;
            tsButton_Italic.BackColor = Color.FromName("ActiveBorder");
            tsButton_Underline.BackColor = Color.FromName("ActiveBorder");

            ChangeRichTextBoxProperty();
        }

        private void tsButton_Italic_Click(object sender, EventArgs e)
        {
            if (isItalic == true)
            {
                isItalic = false;
                tsButton_Italic.BackColor = Color.FromName("ActiveBorder");
            }
            else
            {
                isItalic = true;
                tsButton_Italic.BackColor = Color.FromName("ControlDarkDark");
            }
            isBold = false;
            isUnderline = false;
            tsButton_Bold.BackColor = Color.FromName("ActiveBorder");
            tsButton_Underline.BackColor = Color.FromName("ActiveBorder");

            ChangeRichTextBoxProperty();
        }

        private void tsButton_Underline_Click(object sender, EventArgs e)
        {
            if (isUnderline == true)
            {
                isUnderline = false;
                tsButton_Underline.BackColor = Color.FromName("ActiveBorder");
            }
            else
            {
                isUnderline = true;
                tsButton_Underline.BackColor = Color.FromName("ControlDarkDark");
            }
            isBold = false;
            isItalic = false;
            tsButton_Bold.BackColor = Color.FromName("ActiveBorder");
            tsButton_Italic.BackColor = Color.FromName("ActiveBorder");

            ChangeRichTextBoxProperty();
        }

        private void tsButton_Shrink_Click(object sender, EventArgs e)
        {
            if (tsComboBox_FontSize.SelectedIndex > 0)
                tsComboBox_FontSize.SelectedIndex--;

            ChangeRichTextBoxProperty();
        }

        private void tsButton_Grow_Click(object sender, EventArgs e)
        {
            if (tsComboBox_FontSize.SelectedIndex < tsComboBox_FontSize.Items.Count - 1)
                tsComboBox_FontSize.SelectedIndex++;

            ChangeRichTextBoxProperty();
        }

        private void tsButton_Paste_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void tsButton_Copy_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void tsButton_Cut_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void tsComboBox_Font_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = int.TryParse(tsComboBox_FontSize.SelectedIndex.ToString(), out a) ? a : -1;
            if (a != -1)
                ChangeRichTextBoxProperty();
        }

        private void tsComboBox_FontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = int.TryParse(tsComboBox_Font.SelectedIndex.ToString(), out a) ? a : -1;
            if (a != -1)
                ChangeRichTextBoxProperty();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToLongTimeString();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e) // Font 15.75?
        {
            if (richTextBox1.SelectionFont != null)
            {
                tsComboBox_Font.SelectedItem = richTextBox1.SelectionFont.Name;
                tsComboBox_FontSize.SelectedItem = richTextBox1.SelectionFont.Size.ToString();                
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Product: IboWordPad (.rtf)\nMade by:  Ibrahim Huseynzade\nContact us:  IboWordPad@gmail.com", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isChanged == false)
            {
                NewIboWordPad();
                return;
            }
            DialogResult result = MessageBox.Show("Do you want to save your changes to Document?", "IboWordPad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
                NewIboWordPad();                
            }
            else if (result == DialogResult.No)
            {
                NewIboWordPad();
            }
            else if (result == DialogResult.Cancel)
                return;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isChanged == true)
            {
                DialogResult result = MessageBox.Show("Do you want to save your changes to Document?", "IboWordPad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (result == DialogResult.Cancel)
                    return;
            }
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "IboWordPad Files |*.rtf|Text Files (*.txt)|*.txt";

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile.FileName.Length >= 0)
            {
                if (openFile.FilterIndex == 1)
                    richTextBox1.LoadFile(openFile.FileName);
                else
                    richTextBox1.Text = File.ReadAllText(openFile.FileName);

                filename = openFile.FileName;
            }
            isChanged = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename != "")
                richTextBox1.SaveFile(filename);
            else
                saveAsToolStripMenuItem_Click(sender, e);
            isChanged = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog openFile = new SaveFileDialog();

            openFile.Filter = "IboWordPad Files |*.rtf";

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
               openFile.FileName.Length >= 0)
            {
                richTextBox1.SaveFile(openFile.FileName, RichTextBoxStreamType.RichText);
                filename = openFile.FileName;
            }
            isChanged = false;
        }

        private void tsButton_ForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                foreColor = colorDialog.Color;
                pictureBox_ForeColor.BackColor = foreColor;
            }

            richTextBox1.SelectionColor = foreColor;
        }

        private void tsButton_BackColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                backColor = colorDialog.Color;
                pictureBox_BackColor.BackColor = backColor;
            }

            richTextBox1.SelectionBackColor = backColor;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isChanged == false)
                Environment.Exit(0);

            DialogResult result = MessageBox.Show("Do you want to save your changes to Document?", "IboWordPad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);                
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }
    }
}

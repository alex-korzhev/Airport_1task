using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airport_1task
{
    public partial class Form1 : Form
    {

        List<TextFile> textFiles;
        private OpenFileDialog ofd;

        public Form1()
        {
            InitializeComponent();
            textFiles = new List<TextFile>();
        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {
            TextFile tf = openFile();
            if (tf != null)
            {
                addTFToForm(tf);
            }
        }

        private void addTFToForm(TextFile tf)
        {
            textFiles.Add(tf);
            tb_main_text.Text = tf.Text;
            ListViewItem lvi = new ListViewItem();
            lvi.Text = tf.Name;
            lvi.Tag = textFiles.IndexOf(tf);
            lv_opened_files.Items.Add(lvi);
        }

        private TextFile openFile()
        {
            ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TextFile temptf = textFiles.Find(tf => tf.Path == ofd.FileName);
                if (temptf != null)
                {
                    showFile(temptf);
                    return null;
                }
                else
                {
                    try
                    {
                        var sr = new StreamReader(ofd.FileName);
                        return new TextFile(ofd.SafeFileName, sr.ReadToEnd(), ofd.FileName);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                        $"Details:\n\n{ex.StackTrace}");
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private void lv_opened_files_Click(object sender, EventArgs e)
        {
            ListViewItem selectedFile = lv_opened_files.SelectedItems[0];
            showFile(textFiles[Int32.Parse(selectedFile.Tag.ToString())]);
        }

        private void showFile(TextFile tf)
        {
            tb_main_text.Text = tf.Text;
            lv_opened_files.Items[textFiles.IndexOf(tf)].Selected = true;
            lv_opened_files.Select();
        }
    }

    public class TextFile
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }

        public TextFile(string name, string text, string path)
        {
            Name = name;
            Text = text;
            Path = path;
        }
        public override string ToString()
        {
            return Text;
        }


    }

}

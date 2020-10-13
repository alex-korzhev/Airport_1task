using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

namespace Airport_1task
{
    public partial class Form1 : Form
    {

        public FileHandler fh;
        public Form1()
        {
            InitializeComponent();
            lv_opened_files.Columns[0].Width = lv_opened_files.Width;
            fh = new FileHandler(lv_opened_files);
        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {
            TextFile tf = fh.LoadFile();
            fh.showFile(tf, tb_main_text);
            fh.selectFile(tf);
        }

        private void lv_opened_files_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem selectedFile = lv_opened_files.SelectedItems[0];
                fh.showFile(fh.FindTFByPath(selectedFile.Tag.ToString()), tb_main_text);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        private void btn_load_files_Click(object sender, EventArgs e)
        {
            fh.LoadFileList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fh.SaveFileList();
        }

        private void lv_opened_files_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem selectedFile = lv_opened_files.SelectedItems[0];
                System.Diagnostics.Process.Start(selectedFile.Tag.ToString());
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }
    }

    public class FileHandler
    {
        readonly string fileListPath = Application.StartupPath + "\\filelist.xml";
        private OpenFileDialog ofd;

        public List<TextFile> TextFiles { get; set; }
        public ListView SyncedLV { get; set; }

        public FileHandler(ListView lv)
        {
            TextFiles = new List<TextFile>();
            SyncedLV = lv;
        }

        public void RedrawListView()
        {
            SyncedLV.Clear();
            foreach (TextFile tf in TextFiles)
            {
                AddToListView(tf);
            }
        }

        public TextFile FindTFByPath(String path)
        {
            return TextFiles.Find(tf => tf.Path == path);
        }

        public void AddToListView(TextFile tf)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = tf.Name;
            lvi.Tag = tf.Path;
            SyncedLV.Items.Add(lvi);
        }

        public void selectFile(TextFile tf)
        {
            if (tf != null)
            {
                SyncedLV.Items[TextFiles.IndexOf(tf)].Selected = true;
                SyncedLV.Select();
            }
        }

        public void showFile(TextFile tf, TextBox tb)
        {
            if (tf != null)
            {
                tb.Text = tf.Text;
            }
            else
            {
                tb.Clear();
            }
        }

        public TextFile LoadFile()
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                TextFile temptf = TextFiles.Find(tf => tf.Path == ofd.FileName);
                if (temptf != null)
                {
                    return temptf;
                }
                else
                {
                    try
                    {
                        var sr = new StreamReader(ofd.FileName);
                        TextFiles.Add(new TextFile(ofd.SafeFileName, sr.ReadToEnd(), ofd.FileName));
                        AddToListView(TextFiles.Last());
                        return TextFiles.Last();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при открытии файла: \r\n" + ex.StackTrace);
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public void LoadFileList()
        {
            if (File.Exists(fileListPath))
            {
                XmlSerializer reader = new XmlSerializer(TextFiles.GetType());
                StreamReader file = new StreamReader(fileListPath);
                TextFiles = new List<TextFile>();
                TextFiles = (List<TextFile>)reader.Deserialize(file);
                file.Close();
                RedrawListView();
            }
        }

        public void SaveFileList()
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.NewLineHandling = NewLineHandling.Entitize;
            XmlSerializer ser = new XmlSerializer(TextFiles.GetType());
            using (XmlWriter wr = XmlWriter.Create(fileListPath, ws))
            {
                ser.Serialize(wr, TextFiles);
            }
        }

    }
        public class TextFile
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }

        public TextFile()
        {

        }
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

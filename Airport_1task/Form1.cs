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
            //Show full file name in file list (filena... otherwise)
            lv_opened_files.Columns[0].Width = lv_opened_files.Width;
            fh = new FileHandler(lv_opened_files);
        }

        private void btn_open_file_Click(object sender, EventArgs e)
        {
            //Load file to the list (handler)
            TextFile tf = fh.LoadFile();
            //Show file in textbox
            fh.showFile(tf, tb_main_text);
            //Select the filename in listview
            fh.selectFile(tf);
        }

        private void lv_opened_files_Click(object sender, EventArgs e)
        {
            /* Workaround for an exception,
             * when user clicks outside of the text in a row (slightly to the right)
             * FullRowSelect doesn't work here.
             */
            try
            {
                ListViewItem selectedFile = lv_opened_files.SelectedItems[0];
                fh.showFile(fh.FindTFByPath(selectedFile.Tag.ToString()), tb_main_text);
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void lv_opened_files_DoubleClick(object sender, EventArgs e)
        {
            //Same as above
            try
            {
                ListViewItem selectedFile = lv_opened_files.SelectedItems[0];
                System.Diagnostics.Process.Start(selectedFile.Tag.ToString());
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void btn_load_files_Click(object sender, EventArgs e)
        {
            fh.LoadFileList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fh.SaveFileList();
        }

        
    }

    public class FileHandler
    {
        //Places the filelist in the same folder as the .exe
        readonly string fileListPath = Application.StartupPath + "\\filelist.xml";
        private OpenFileDialog ofd;
        public List<TextFile> TextFiles { get; set; }
        //ListView associated with the file handler
        public ListView SyncedLV { get; set; }

        public FileHandler(ListView lv)
        {
            //Initialize the list
            TextFiles = new List<TextFile>();
            //Associate the ListView with the List<TextFiles>
            SyncedLV = lv;
        }

        //Repopulate ListView completely from the List
        public void RedrawListView()
        {
            SyncedLV.Clear();
            foreach (TextFile tf in TextFiles)
            {
                AddToListView(tf);
            }
        }

        //Returns instanceof TextFile from it's path (used in ListView Tag)
        public TextFile FindTFByPath(String path)
        {
            return TextFiles.Find(tf => tf.Path == path);
        }

        //Add one TextFile to ListView
        public void AddToListView(TextFile tf)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = tf.Name;
            lvi.Tag = tf.Path;
            SyncedLV.Items.Add(lvi);
        }

        //Highlight the TetFile name in the ListView
        public void selectFile(TextFile tf)
        {
            if (tf != null)
            {
                SyncedLV.Items[TextFiles.IndexOf(tf)].Selected = true;
                SyncedLV.Select();
            }
        }

        //Show the TextFile's text in the Textbox
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

        //Load file into the TextFiles List (and return it to the caller).
        public TextFile LoadFile()
        {
            ofd = new OpenFileDialog();
            //Filter .txt files only
            //ofd.Filter = "txt files (*.txt)|*.txt";
            //ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //If text file was already opened (decided by file path) - return the existing TF.
                TextFile temptf = TextFiles.Find(tf => tf.Path == ofd.FileName);
                if (temptf != null)
                {
                    return temptf;
                }
                //If file is not in the list - load and return
                else
                {
                    try
                    {
                        var sr = new StreamReader(ofd.FileName);
                        //Add to the TF List
                        TextFiles.Add(new TextFile(ofd.SafeFileName, sr.ReadToEnd(), ofd.FileName));
                        //Add to ListView
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
            //If user clicked "Cancel" on the dialog box
            else
            {
                return null;
            }
        }

        //Loads the insides of the Text Files back into the program (from XML)
        //NOT loading the actual files.
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

        //Save the file list and the text inside into XML
        public void SaveFileList()
        {
            XmlWriterSettings ws = new XmlWriterSettings();
            // Workaround for a bug, when text saved in xml loses all the line breaks
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

        //Blank constructor for XML serialization
        public TextFile()
        {

        }

        //Actual constructor
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

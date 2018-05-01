using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace FileConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog mydialog = new OpenFileDialog();
            string textPath;
            mydialog.Filter = "PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";

            if (mydialog.ShowDialog() == DialogResult.OK)
            {
                textPath = mydialog.FileName.ToString();
                //MessageBox.Show(textPath.ToString());

                string strText = string.Empty;
                try
                {

                    PdfReader pdfReader = new PdfReader(textPath);
                    for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();
                        string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                        currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.UTF8.GetBytes(currentText)));
                        //text.Append(currentText);
                        strText = strText + currentText;
                        richTextBox1.Text = strText;
                       
                    }
                    pdfReader.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = @"C:\";

            saveFileDialog1.Title = "Save text Files";

            //saveFileDialog1.CheckFileExists = true;

            //saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.FileName = "*.txt";

            saveFileDialog1.DefaultExt = "txt";

            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            saveFileDialog1.FilterIndex = 2;

            saveFileDialog1.RestoreDirectory = true;



            if (saveFileDialog1.ShowDialog() == DialogResult.OK)

            {

                Stream fileStream = saveFileDialog1.OpenFile();
                StreamWriter sw = new StreamWriter(fileStream);
                sw.Write(richTextBox1.Text);
                sw.Close();
                fileStream.Close();
                richTextBox1.Clear();
                //richTextBox1.Text = saveFileDialog1.FileName;

            }
        }
    }
}

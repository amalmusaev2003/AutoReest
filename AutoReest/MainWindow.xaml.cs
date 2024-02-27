using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;



namespace AutoReest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessPdf();
        }


        private void ProcessPdf()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            StringBuilder sb = new StringBuilder();

            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                using (PdfReader reader = new PdfReader(filePath))
                {
                    for (int pNum = 1; pNum < reader.NumberOfPages; pNum++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(reader, pNum, strategy);
                        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                        sb.Append(text);
                    }
                }

            }

            //Testing how our lib exrtract info to the txt file

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:/Users/amusaev/Desktop/task", "File3.txt")))
            {
                outputFile.WriteLine(sb);
            }

            // Extract code
            MessageBox.Show(GetCode(sb.ToString()));


            //Extract last number of changes
            MessageBox.Show(GetRevisionNumber(sb.ToString()));


        }
        private string GetCode(string text)
        {
            /*TODO:
             [1] - разобраться почему анализ текста начинается не сначала файла и исправить это
             [2] - Дать возможность пользователю добавить свой паттерн
            */
            string codePattern = @"^.+-[^-]+-[^-]+$";
            string code = "";
            string[] words = Regex.Split(text, " ");

            foreach (string word in words)
            {
                if (Regex.IsMatch(word, codePattern))
                {
                    code = word;
                }

            }
            return code;
        }
        private string GetRevisionNumber(string text)
        {
            /*TODO:
             [1] - Протестировать текущий паттерн для первого файла
             [2] - Реализовать возможность вариировать шаблоны ???
            */
            string tablePattern = @"^(\d)\s.+(\d{1,2}\.\d{1,2}\.\d{2,4})";
            string revision = "";
            string[] lines = Regex.Split(text, "\n");
            Array.Reverse(lines);

            foreach (string line in lines)
            {
                if (Regex.IsMatch(line, tablePattern))
                {
                    Match match = Regex.Match(text, tablePattern);
                    revision = match.Groups[1].Value;
                }

            }
            return Convert.ToString(revision);
        }

    }
}

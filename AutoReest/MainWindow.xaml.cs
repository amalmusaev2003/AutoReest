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
            /*TODO:
             [1] - Разобраться как выбирать папку с файлами
             */

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

            // Отображение диалогового окна
            if (dialog.ShowDialog() == true)
            {
                string selectedFolderPath = dialog.FileName;

                ProcessPdf(selectedFolderPath);
            }
        }


        private void ProcessPdf(string selectedFolderPath)
        {
            StringBuilder sb = new StringBuilder();
            using (PdfReader reader = new PdfReader(selectedFolderPath))
            {
                for (int pNum = 1; pNum < reader.NumberOfPages; pNum++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader, pNum, strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    sb.Append(text);
                }
            }
            MessageBox.Show(GetCode(sb.ToString()));
            MessageBox.Show(GetRevisionNumber(sb.ToString()));
            /*string[] files = Directory.GetFiles(selectedFolderPath, "*.pdf");
            int fileCount = files.Length;
            StringBuilder[] sbs = new StringBuilder[fileCount];

            for (int f = 0; f < fileCount; f++) 
            {
                using (PdfReader reader = new PdfReader(files[f]))
                {
                    for (int pNum = 1; pNum < reader.NumberOfPages; pNum++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(reader, pNum, strategy);
                        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                        sbs[f].Append(text);
                    }
                }
            }

            for (int t = 0; t < sbs.Length; t++)
            {
                using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:/Users/amusaev/Desktop/task/txtoutput",  $"File{t+1}.txt")))
                {
                    outputFile.WriteLine(sbs[t]);
                }
            }*/

            /*Testing how our lib exrtract info to the txt file

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:/Users/amusaev/Desktop/task", "File3.txt")))
            {
                outputFile.WriteLine(sb);
            }*/
        }

        private string GetCode(string text)
        {
            string codePattern = @"^\d+-\d+/\d+-\d+-\d+-.+$";
            string[] words = Regex.Split(text, " ");

            foreach (string word in words)
            {
                if (Regex.IsMatch(word, codePattern))
                {
                    return word;
                }

            }
            return "не найден";
        }
        private string GetRevisionNumber(string text)
        {
            /*TODO:
             [1] - Реализовать возможность вариировать шаблоны ???
            */
            string tablePattern = @"(?<num>\d+) \d+-\d+ (?<date>\d{1,2}.\d{1,2}.\d{1,2})";
            var rg = new Regex(tablePattern, RegexOptions.RightToLeft);

            Match matchedChange = rg.Match(text);
            if (matchedChange.Success)
               return matchedChange.Groups["num"].Value;
            return "не менялся";
        }

    }
}

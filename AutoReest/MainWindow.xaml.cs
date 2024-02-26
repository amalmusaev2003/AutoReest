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
            
            string codePattern = @"^11-10/.*$";
            string changesPattern = @"^Изм";
            string sep = "[\n ]";
            openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                using(PdfReader reader = new PdfReader(filePath))
                {
                    for(int pNum = 1; pNum < reader.NumberOfPages; pNum++)
                    {
                        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                        string text = PdfTextExtractor.GetTextFromPage(reader, pNum, strategy);
                        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                        sb.Append(text);   
                    }
                }

            }

            /* 
            Testing how our lib exrtract info to the txt file
            
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine("C:/Users/amusaev/Desktop/task", "WriteFile.txt")))
            {
                outputFile.WriteLine(sb);
            }*/

            // Extract code 
            string[] ourText = sb.ToString().Split('\n');
            string code = "";
            int changes = 0;
            string[] words = Regex.Split(sb.ToString(), sep);

            

            foreach (string line in words)
            {
                if (Regex.IsMatch(line, codePattern))
                {
                    code = line;
                }

                string[] parts = line.Split(' ');

                if (parts.Length == 4)
                {
                    int changeNumber = int.Parse(parts[0]);

                    if (changeNumber > changes)
                    {
                        changes = changeNumber;
                    }
                }
            }


            MessageBox.Show(code);
            MessageBox.Show(Convert.ToString(changes));
        }

    }
}

using System.Collections.Generic;
using System;
using System.Windows;
using System.IO;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using AutoReest.Services.RegexPatterns;
using System.Text;

namespace AutoReest.Services.Workers
{
    public partial class PdfWorker
    {
        private PdfReader _pdfReader;
        private List<string> _pageContents;

        public PdfWorker(string pathToFile)
        {
            _pdfReader = new PdfReader(pathToFile);
            _pageContents = new List<string>();

            InitPageContents();
        }

        /// <summary>
        /// Находит обозначение документа при помощи Regex
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public string FindNotation(int pageNum) => PdfTextPatterns.NotationRegex.Match(_pageContents[pageNum]).Value;

        /// <summary>
        /// Находит таблицу в документе при помощи Regex
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public string[] FindTable(int pageNum) => PdfTextPatterns.TableRegex.Matches(_pageContents[pageNum]).ToStrings();

        public int GetPageNumbers() => _pdfReader.NumberOfPages;

        private void InitPageContents()
        {
            try
            {
                for (int i = 1; i < _pdfReader.NumberOfPages; i++)
                    _pageContents.Add(PdfTextExtractor.GetTextFromPage(_pdfReader, i));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить текст страницы...");
            }
        }
        
        public void PdfToTxt()
        {
            string filePath = "C:/Users/amusaev/Desktop/task/File3.txt";
            File.WriteAllLines(filePath, _pageContents);
        }
    }
}


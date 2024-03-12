using Microsoft.Win32;
using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.Input;
using AutoReest.Services.Workers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AutoReest.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public ICommand SelectFileCommand { get; }
        public MainViewModel()
        {
            SelectFileCommand = new RelayCommand(SelectFile);
        }

        private void SelectFile()
        {
            /* Extract data from one file only
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                // Здесь можно обработать выбранный файл, например, передать его в метод парсера
                var pdfWorker = new PdfWorker(/*PATH TO PDF FILEselectedFilePath);
                var data = DataBuilder.RegistryDataBuild(pdfWorker);
                MessageBox.Show(data.Notation);
                MessageBox.Show(data.Data.NumberOfColumn);
            }*/
            string lastChange;

            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    foreach (string file in files)
                    {
                        var pdfWorker = new PdfWorker(file);
                        //pdfWorker.PdfToTxt();
                        var data = DataBuilder.RegistryDataBuild(pdfWorker);
                        if (data.Data.NumberOfColumn == null) lastChange = "не менялся";
                        else lastChange = data.Data.NumberOfColumn;
                        
                        System.Windows.Forms.MessageBox.Show(data.Notation);
                        System.Windows.Forms.MessageBox.Show(lastChange);

                    }
                }
            }
        }
    }
}

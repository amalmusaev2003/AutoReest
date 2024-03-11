using Microsoft.Win32;
using System;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using AutoReest.Services.Workers;

namespace AutoReest.ViewModel
{
    internal class MainViewModel
    {
        public ICommand SelectFileCommand { get; }
        public MainViewModel()
        {
            SelectFileCommand = new RelayCommand(SelectFile);
        }
        // TODO
        // [1] - Надо обработать случай когда программа не находит таблицы

        private void SelectFile()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                // Здесь можно обработать выбранный файл, например, передать его в метод парсера
                var pdfWorker = new PdfWorker(/*PATH TO PDF FILE*/selectedFilePath);
                var data = DataBuilder.RegistryDataBuild(pdfWorker);
                MessageBox.Show(data.Notation);
                MessageBox.Show(data.Data.NumberOfColumn);
            }
        }
    }
}

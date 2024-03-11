using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using AutoReest.ViewModel;

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
            DataContext = new MainViewModel();
        }
    }
}

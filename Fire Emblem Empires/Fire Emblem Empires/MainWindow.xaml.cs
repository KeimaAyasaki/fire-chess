﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Fire_Emblem_Empires.File_Management;
using Fire_Emblem_Empires.Board_Creation;
using Fire_Emblem_Empires.Unit_Creation;
using System.Drawing;
using System.Reflection;

namespace Fire_Emblem_Empires
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileReader fReader = new FileReader();
        Board map;

        public MainWindow()
        {
            InitializeComponent();
            this.Width = menuScreen.Width;
            this.Height = menuScreen.Height;
            menuScreen.MouseUp += new MouseButtonEventHandler(ChangeScreenClick);
            fReader.Initialize(out map);
            GameGrid grid = new GameGrid(map);
            grid.Show();
            //this.Close();
        }

        private void ChangeScreenClick(Object sender, RoutedEventArgs e)
        {
            string imageSource = menuScreen.Source.ToString();
            //if (imageSource.Equals("file:///D:/fire-chess/fire-chess/Fire Emblem Empires/Data/Menus/TitleScreen.jpg"))
            //{

            BitmapImage nextScreen = new BitmapImage();
            nextScreen.BeginInit();
            nextScreen.UriSource = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name  +";component/Resources/OfficialMenuScreen.png");
            nextScreen.EndInit();
            menuScreen.Source = nextScreen;
            


            //}
        }
    }
}

using System;
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
        }

        private void fileLoader_Click(object sender, RoutedEventArgs e)
        {
            fReader.Initialize("\\Data\\MapFiles\\Chapter1T1.fes", out map);
            GameGrid grid = new GameGrid(map);
            grid.Show();
        }

        private void fileSaver_Click(object sender, RoutedEventArgs e)
        {
            fReader.CreateFile(map);
        }

        private void OnMouseClick(Object sender, MouseEventArgs e)
        {
            if (menuScreen.Source.ToString().Equals("D:\\fire-chess\\fire-chess\\Fire Emblem Empires\\Data\\Menus\\TitleScreen.jpg"))
            {

                //Image newImage = new Image();
                //BitmapImage newimage = new BitmapImage();
                //newimage.BeginInit();
                //menuScreen.Source = new BitmapImage(new Uri(@"D:\\fire-chess\\fire-chess\\Fire Emblem Empires\\Data\\Menus\\OfficialMenuScreen.jpg", UriKind.Relative));
                menuScreen.BeginInit();
                menuScreen.Source = new BitmapImage(new Uri("D:\\fire-chess\\fire-chess\\Fire Emblem Empires\\Data\\Menus\\OfficialMenuScreen.jpg", UriKind.Relative));
                menuScreen.EndInit();
                //menuScreen.Source = new Image("D:\\fire-chess\\fire-chess\\Fire Emblem Empires\\Data\\Menus\\OfficialMenuScreen.jpg");


            }
        }
    }
}

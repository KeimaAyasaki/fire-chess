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
using Fire_Emblem_Empires.Unit_Management;

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
            fReader.Initialize("Chapter1T1.txt", out map);
            GameGrid grid = new GameGrid(map);
            grid.Show();
            this.Close();
        }
    }
}

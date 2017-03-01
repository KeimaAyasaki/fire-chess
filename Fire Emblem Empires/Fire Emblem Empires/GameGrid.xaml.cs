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
using System.Windows.Shapes;
using Fire_Emblem_Empires.Board_Creation;

namespace Fire_Emblem_Empires
{
    /// <summary>
    /// Interaction logic for GameGrid.xaml
    /// </summary>
    public partial class GameGrid : Window
    {
        public GameGrid(Board map)
        {
            InitializeComponent();

            int tileSize = 100;

            Grid dynamicGrid = new Grid();
            
            dynamicGrid.VerticalAlignment = VerticalAlignment.Top;
            dynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            dynamicGrid.ShowGridLines = true;
            int numRows = map.numRows;
            int numColumns = map.numColumns;
            for(int i = 0; i < numRows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(tileSize);
                dynamicGrid.RowDefinitions.Add(row);
            }
            for(int i = 0; i < numColumns; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(tileSize);
                dynamicGrid.ColumnDefinitions.Add(column);
            }
            this.Content = dynamicGrid;
            this.Width = tileSize * numColumns + 17;
            this.Height = tileSize * numRows + 40;
        }
    }
}

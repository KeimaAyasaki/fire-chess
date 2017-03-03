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
using Fire_Emblem_Empires.Unit_Creation;

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

            for(int i = 0; i < numRows; i++)
            {
                for(int j = 0; j < numColumns; j++)
                {
                    TextBlock tile = new TextBlock();
                    if(map.spaces[i, j].occupiedBy != null)
                    {
                        tile.Text = map.spaces[i, j].occupiedBy.GetJob().ToString().First().ToString();
                        tile.FontSize = 40;
                        tile.FontWeight = FontWeights.Bold;
                        tile.TextAlignment = TextAlignment.Center;
                        switch(map.spaces[i, j].occupiedBy.GetTeamColor())
                        {
                            case Team.BLUE:
                                tile.Foreground = new SolidColorBrush(Colors.Aqua);
                                break;
                            case Team.GREEN:
                                tile.Foreground = new SolidColorBrush(Colors.LightGreen);
                                break;
                            case Team.RED:
                                tile.Foreground = new SolidColorBrush(Colors.Red);
                                break;
                        }
                    }
                    SolidColorBrush backgroundColor = new SolidColorBrush();
                    switch(map.spaces[i, j].terrainType)
                    {
                        case TileEnumeration.PLAIN:
                            backgroundColor = new SolidColorBrush(Colors.Green);
                            break;
                        case TileEnumeration.FOREST:
                            backgroundColor = new SolidColorBrush(Colors.DarkGreen);
                            break;
                        case TileEnumeration.WATER:
                            backgroundColor = new SolidColorBrush(Colors.Blue);
                            break;
                        case TileEnumeration.MOUNTAIN:
                            backgroundColor = new SolidColorBrush(Colors.Gray);
                            break;
                        case TileEnumeration.TOWN:
                            backgroundColor = new SolidColorBrush(Colors.Gold);
                            break;
                    }
                    tile.Background = backgroundColor;
                    Grid.SetRow(tile, i);
                    Grid.SetColumn(tile, j);
                    dynamicGrid.Children.Add(tile);
                }
            }
        }
    }
}

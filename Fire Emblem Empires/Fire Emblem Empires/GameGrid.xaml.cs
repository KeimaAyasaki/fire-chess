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
using Fire_Emblem_Empires.File_Management;

namespace Fire_Emblem_Empires
{
    /// <summary>
    /// Interaction logic for GameGrid.xaml
    /// </summary>
    public partial class GameGrid : Window
    {
        Board map;
        public GameGrid(Board map)
        {
            InitializeComponent();

            this.map = map;

            int tileSize = 75;

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
            dynamicGrid.Width = tileSize * numColumns;
            dynamicGrid.Height = tileSize * numRows;
            dynamicGrid.Margin = new Thickness(-2);
            this.View.Items.Add(dynamicGrid);
            this.Width = dynamicGrid.Width + 18;
            this.Height = tileSize * (numRows) + (tileSize * 2);
            this.View.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.View.VerticalAlignment = VerticalAlignment.Stretch;
            this.View.Padding = new Thickness(-3);
            this.View.BorderThickness = new Thickness(0);

            for (int i = 0; i < numRows; i++)
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
                    tile.MouseEnter += new MouseEventHandler(Mouse_Enter_Event);
                    tile.MouseLeave += new MouseEventHandler(Mouse_Exit_Event);
                    tile.MouseLeftButtonUp += new MouseButtonEventHandler(Mouse_Left_Click);
                    tile.Padding = new Thickness(0);
                    tile.Margin = new Thickness(0);
                    Grid.SetRow(tile, i);
                    Grid.SetColumn(tile, j);
                    dynamicGrid.Children.Add(tile);
                }
            }
            TextBlock unitInfo = new TextBlock();
            unitInfo.Background = new SolidColorBrush(Colors.Gray);
            unitInfo.Width = dynamicGrid.Width;
            unitInfo.Height = tileSize * 1.5;
            unitInfo.Margin = new Thickness(-2);
            this.View.Items.Add(unitInfo);
        }

        private void Mouse_Enter_Event(object sender, MouseEventArgs e)
        {
            TextBlock tile = (TextBlock) sender;
            SolidColorBrush background = (SolidColorBrush)tile.Background;
            Color newColor = background.Color + Color.FromArgb(0, 80, 80, 80);
            tile.Background = new SolidColorBrush(newColor);
            Unit targetUnit = GetUnitOnTile(tile);
            TextBlock unitInfo = (TextBlock)this.View.Items.GetItemAt(1);
            if (targetUnit != null)
            {
                unitInfo.Text = targetUnit.ToString();
                switch (targetUnit.GetTeamColor())
                {
                    case Team.BLUE:
                        unitInfo.Background = new SolidColorBrush(Colors.Aqua);
                        break;
                    case Team.GREEN:
                        unitInfo.Background = new SolidColorBrush(Colors.LightGreen);
                        break;
                    case Team.RED:
                        unitInfo.Background = new SolidColorBrush(Colors.Red);
                        break;
                }
            }
        }

        private void Mouse_Exit_Event(object sender, MouseEventArgs e)
        {
            TextBlock tile = (TextBlock)sender;
            SolidColorBrush background = (SolidColorBrush)tile.Background;
            Color newColor = background.Color - Color.FromArgb(0, 80, 80, 80);
            tile.Background = new SolidColorBrush(newColor);
            TextBlock unitInfo = (TextBlock)this.View.Items.GetItemAt(1);
            unitInfo.Text = "";
            unitInfo.Background = new SolidColorBrush(Colors.Gray);
        }

        private void Mouse_Left_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock tile = (TextBlock)sender;
            Unit selectedUnit = GetUnitOnTile(tile);
            FileReader reader = new FileReader();
            if(Grid.GetRow(tile)==0 && Grid.GetColumn(tile)==0)
            {
                reader.CreateFile(map);
            }
            else if(Grid.GetRow(tile)==0 && Grid.GetColumn(tile)==1)
            {
                map = new Board(9, 11);
                reader.Initialize(out map, "Chapter1T2");
                GameGrid newMap = new GameGrid(map);
                newMap.Show();
            }
        }
        private Unit GetUnitOnTile(TextBlock sender)
        {
            int rowNumber = Grid.GetRow(sender);
            int columnNumber = Grid.GetColumn(sender);
            return map.spaces[rowNumber, columnNumber].occupiedBy;
        }
    }

}

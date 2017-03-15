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
        bool unitSelected;
        int previousRow;
        int previousColumn;
        MainWindow menu;
        GameLogic logic;
        public GameGrid(Board map, MainWindow menu)
        {
            InitializeComponent();

            this.map = map;
            this.menu = menu;
            logic = new GameLogic(ref map);

            this.KeyDown += new KeyEventHandler(EscapePressed);

            unitSelected = false;

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
            this.Height = tileSize * (numRows) + (tileSize * 2.2);
            this.View.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.View.VerticalAlignment = VerticalAlignment.Stretch;
            this.View.Padding = new Thickness(-3);
            this.View.BorderThickness = new Thickness(0);

            for (int i = 0; i < numRows; i++)
            {
                for(int j = 0; j < numColumns; j++)
                {
                    TextBlock tile = new TextBlock();
                    tile.FontSize = 40;
                    tile.FontWeight = FontWeights.Bold;
                    tile.TextAlignment = TextAlignment.Center;
                    //if (map.spaces[i, j].m_unit != null)
                    //{
                    //    tile.Text = ConvertJobToString(map.spaces[i, j].m_unit);
                    //    if (map.spaces[i, j].m_unit.GetTeamColor() == Team.BLUE)
                    //    {
                    //        tile.Foreground = new SolidColorBrush(Colors.Aqua);
                    //    }
                    //    else
                    //    {
                    //        tile.Foreground = new SolidColorBrush(Colors.Gray);
                    //    }
                    //}
                    SolidColorBrush backgroundColor = new SolidColorBrush();
                    switch(map.spaces[i, j].m_terrainType)
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
                            backgroundColor = new SolidColorBrush(Colors.SaddleBrown);
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
            unitInfo.Height = tileSize * 1.7;
            unitInfo.Margin = new Thickness(-2);
            this.View.Items.Add(unitInfo);
            UpdateGrid();
        }



        private void Mouse_Enter_Event(object sender, MouseEventArgs e)
        {
            TextBlock tile = (TextBlock) sender;
            SolidColorBrush background = (SolidColorBrush)tile.Background;
            Color newColor = background.Color + Color.FromArgb(0, 80, 80, 80);
            tile.Background = new SolidColorBrush(newColor);
            Unit targetUnit = GetUnitOnTile(tile);
            TextBlock unitInfo = (TextBlock)this.View.Items.GetItemAt(1);
            unitInfo.Background = background;
            unitInfo.Text = map.spaces[Grid.GetRow(tile), Grid.GetColumn(tile)].m_terrainType.ToString() + "\n";
            if (targetUnit != null)
            {
                unitInfo.Text += targetUnit.ToString();
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
            //Unit selectedUnit = GetUnitOnTile(tile);
            int row = Grid.GetRow(tile);
            int column = Grid.GetColumn(tile);

            if (!unitSelected)
            {
                previousRow = row;
                previousColumn = column;
                Tile currTile = map.spaces[row, column];
                currTile.m_Location = new Location((byte)row, (byte)column);
                logic.SetCurrTile(currTile);
                unitSelected = true;
            }
            else
            {
                Tile destTile = map.spaces[row, column];
                destTile.m_Location = new Location((byte)row, (byte)column);
                logic.SetDestTile(destTile);
                if (logic.TakeTurn())
                {
                    tile.Text = ConvertJobToString(map.spaces[row, column].m_unit);
                    UpdateGrid();
                }
                unitSelected = false;
            }
        }
        private Unit GetUnitOnTile(TextBlock sender)
        {
            int rowNumber = Grid.GetRow(sender);
            int columnNumber = Grid.GetColumn(sender);
            return map.spaces[rowNumber, columnNumber].m_unit;
        }

        private string ConvertJobToString(Unit currJob)
        {
            string job = "";
            if (currJob != null)
            {
                switch (currJob.GetJob())
                {
                    case Job.MERCENARY:
                        job = "M";
                        break;
                    case Job.SOLDIER:
                        job = "S";
                        break;
                    case Job.FIGHTER:
                        job = "F";
                        break;
                    case Job.MAGE:
                        job = "C";
                        break;
                    case Job.HEALER:
                        job = "H";
                        break;
                }
            }
            return job;
        }

        private void EscapePressed (object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                menu.Visibility = Visibility.Visible;
                menu.map = this.map;
            }
        }

        private void UpdateGrid()
        {
            for(int i = 0; i < map.numRows; ++i)
            {
                for (int j = 0; j < map.numColumns; ++j)
                {
                    Grid temp = (Grid)this.View.Items.GetItemAt(0);
                    TextBlock space = (TextBlock)temp.Children.Cast<UIElement>().First(k => Grid.GetRow(k) == i && Grid.GetColumn(k) == j);
                    space.Text = ConvertJobToString(map.spaces[i, j].m_unit);
                    if (map.spaces[i, j].m_unit != null && map.spaces[i, j].m_unit.CanTakeAction())
                    {
                        switch (map.spaces[i, j].m_unit.GetTeamColor())
                        {
                            case Team.BLUE:
                                space.Foreground = new SolidColorBrush(Colors.Aqua);
                                break;
                            case Team.GREEN:
                                space.Foreground = new SolidColorBrush(Colors.LightGreen);
                                break;
                            case Team.RED:
                                space.Foreground = new SolidColorBrush(Colors.Red);
                                break;
                        }
                    }
                    else
                    {
                        space.Foreground = new SolidColorBrush(Colors.Gray);
                    }
                }
            }
        }
    }

}

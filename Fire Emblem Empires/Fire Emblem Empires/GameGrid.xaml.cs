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
        Unit selectedUnit;
        int previousRow;
        int previousColumn;
        public GameGrid(Board map)
        {
            InitializeComponent();

            this.map = map;

            selectedUnit = null;

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
                    if(map.spaces[i, j].m_unit != null)
                    {
                        tile.Text = ConvertJobToString(map.spaces[i, j].m_unit.GetJob());
                        tile.FontSize = 40;
                        tile.FontWeight = FontWeights.Bold;
                        tile.TextAlignment = TextAlignment.Center;
                        switch(map.spaces[i, j].m_unit.GetTeamColor())
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
            unitInfo.Height = tileSize * 1.7;
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

            if (selectedUnit == null)
            {
                selectedUnit = GetUnitOnTile(tile);
                if (selectedUnit != null && selectedUnit.CanTakeAction())
                {
                    previousRow = Grid.GetRow(tile);
                    previousColumn = Grid.GetColumn(tile);
                }
                else
                {
                    selectedUnit = null;
                }
            }
            else
            {
                if (map.MoveUnitFromSpaceToSpace(new Location((byte)previousRow, (byte)previousColumn), new Location((byte)Grid.GetRow(tile), (byte)Grid.GetColumn(tile))))
                {
                    tile.Text = ConvertJobToString(selectedUnit.GetJob());
                    tile.FontSize = 40;
                    tile.FontWeight = FontWeights.Bold;
                    tile.TextAlignment = TextAlignment.Center;
                    tile.Foreground = new SolidColorBrush(Colors.Gray);
                    Grid temp = (Grid)this.View.Items.GetItemAt(0);
                    tile = (TextBlock)temp.Children.Cast<UIElement>().First(i => Grid.GetRow(i) == previousRow && Grid.GetColumn(i) == previousColumn);
                    tile.Text = "";
                }
                selectedUnit = null;
            }
        }
        private Unit GetUnitOnTile(TextBlock sender)
        {
            int rowNumber = Grid.GetRow(sender);
            int columnNumber = Grid.GetColumn(sender);
            return map.spaces[rowNumber, columnNumber].m_unit;
        }

        private string ConvertJobToString(Job currJob)
        {
            string job = "";
            switch(currJob)
            {
                case Job.MERCENARY:
                    job = "M";
                    break;           
                case Job.SOLDIER:
                    job = "S";
                    break;
                case Job.FIGHTER:
                    job =  "F";
                    break;
                case Job.MAGE:
                    job = "C";
                    break;
                case Job.HEALER:
                    job = "H";
                    break;
            }
            return job;
        }
    }

}

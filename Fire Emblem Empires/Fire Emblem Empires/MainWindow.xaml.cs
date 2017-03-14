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
using System.Reflection;
using System.Resources;
using System.Collections;
using Fire_Emblem_Empires.Time_Management;

namespace Fire_Emblem_Empires
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileReader fReader = new FileReader();
        public Board map;
        GameGrid boardScreen;
        private Button resume, newGame, saveGame, exitGame, saveData1, saveData2, saveData3, saveData4, loadData1, loadData2, loadData3, loadData4;

        public MainWindow()
        {
            InitializeComponent();
            startButton.Width = menuScreen.Width;
            this.Width = menuScreen.Width;
            this.Height = menuScreen.Height;
            TimeManager.Start();
            menuScreen.MouseUp += new MouseButtonEventHandler(ChangeScreenClick);
        }

        private void ChangeScreenClick(Object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(EscapePressed);
            if (sender == startButton)
            {
                ChangeToMenuScreen();
            }
        }

        private void MouseEnters(object sender, MouseEventArgs e)
        {
            Button temp = (Button)sender;
            temp.Opacity = 0.2;
        }

        private void MouseExits(object sender, MouseEventArgs e)
        {
            Button temp = (Button)sender;
            temp.Opacity = 1;
            temp.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            if (boardScreen != null)
            {
                boardScreen.Close();
            }
            this.Close();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            fReader.Initialize(out map);
            boardScreen = new GameGrid(map, this);
            boardScreen.Show();
            this.Visibility = Visibility.Collapsed;
        }
        private void SaveGame(object sender, RoutedEventArgs e)
        {
            fReader.CreateFile(map, TimeManager.TimeElapsedSinceStart());
            BitmapImage nextScreen = new BitmapImage();
            nextScreen.BeginInit();
            nextScreen.UriSource = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/FE_Empires_SaveScreen.png");
            nextScreen.EndInit();
            menuScreen.Source = nextScreen;
            this.grid.Children.Clear();
            Image newScreen = new Image();
            newScreen.Source = nextScreen;
            this.grid.Children.Add(newScreen);
            saveData1 = new Button();
            saveData2 = new Button();
            saveData3 = new Button();
            saveData4 = new Button();
            double buttonWidth = this.Width * 0.63;
            double buttonHeight = this.Height * 0.12;
            int heightOffset = (int)(this.Height * 0.2);
            int centerOffset = (int)(this.Width * 0.02);
            int buttonDistance = (int)(this.Height * 0.17);
            saveData1.VerticalAlignment = VerticalAlignment.Top;
            saveData1.Margin = new Thickness(-centerOffset, heightOffset, 0, 0);
            saveData1.Width = buttonWidth;
            saveData1.Height = buttonHeight;
            saveData1.MouseEnter += new MouseEventHandler(MouseEnters);
            saveData1.MouseLeave += new MouseEventHandler(MouseExits);
            saveData1.Opacity = 0;
            this.grid.Children.Add(saveData1);
            saveData2.VerticalAlignment = VerticalAlignment.Top;
            saveData2.Margin = new Thickness(-centerOffset, heightOffset += buttonDistance, 0, 0);
            saveData2.Width = buttonWidth;
            saveData2.Height = buttonHeight;
            saveData2.MouseEnter += new MouseEventHandler(MouseEnters);
            saveData2.MouseLeave += new MouseEventHandler(MouseExits);
            saveData2.Opacity = 0;
            this.grid.Children.Add(saveData2);
            saveData3.VerticalAlignment = VerticalAlignment.Top;
            saveData3.Margin = new Thickness(-centerOffset, heightOffset += buttonDistance, 0, 0);
            saveData3.Width = buttonWidth;
            saveData3.Height = buttonHeight;
            saveData3.MouseEnter += new MouseEventHandler(MouseEnters);
            saveData3.MouseLeave += new MouseEventHandler(MouseExits);
            saveData3.Opacity = 0;
            this.grid.Children.Add(saveData3);
            saveData4.VerticalAlignment = VerticalAlignment.Top;
            saveData4.Margin = new Thickness(-centerOffset, heightOffset += (int)(buttonDistance * 0.85), 0, 0);
            saveData4.Width = buttonWidth;
            saveData4.Height = buttonHeight;
            saveData4.MouseEnter += new MouseEventHandler(MouseEnters);
            saveData4.MouseLeave += new MouseEventHandler(MouseExits);
            saveData4.Opacity = 0;
            this.grid.Children.Add(saveData4);
        }
        private void ResumeGame(object sender, RoutedEventArgs e)
        {
            BitmapImage nextScreen = new BitmapImage();
            nextScreen.BeginInit();
            nextScreen.UriSource = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/FE_Empires_LoadScreen.png");
            nextScreen.EndInit();
            List<String> fileNames = new List<String>();
            ResourceSet saves = new ResourceSet("MapFiles.resources");
            foreach(DictionaryEntry file in saves)
            {
                fileNames.Add(file.Key.ToString());
            }
            menuScreen.Source = nextScreen;
            this.grid.Children.Clear();
            Image newScreen = new Image();
            newScreen.Source = nextScreen;
            this.grid.Children.Add(newScreen);
            Button loadData1 = new Button(), loadData2 = new Button(), loadData3 = new Button(), loadData4 = new Button();
            double buttonWidth = this.Width * 0.63;
            double buttonHeight = this.Height * 0.12;
            int heightOffset = (int)(this.Height * 0.2);
            int centerOffset = (int)(this.Width * 0.02);
            int buttonDistance = (int)(this.Height * 0.17);
            loadData1.VerticalAlignment = VerticalAlignment.Top;
            loadData1.Margin = new Thickness(-centerOffset, heightOffset, 0, 0);
            loadData1.Width = buttonWidth;
            loadData1.Height = buttonHeight;
            loadData1.MouseEnter += new MouseEventHandler(MouseEnters);
            loadData1.MouseLeave += new MouseEventHandler(MouseExits);
            loadData1.Background = new SolidColorBrush(Colors.Transparent);
            if(fileNames.Count > 0)
            {
                string saveName = fileNames[0].Substring(0, fileNames[0].IndexOf('_'));
                loadData1.Tag = fileNames[0];
                loadData1.Content = saveName;
                loadData1.FontSize = 25;
                loadData1.Click += new RoutedEventHandler(LoadFile);
            }
            this.grid.Children.Add(loadData1);
            loadData2.VerticalAlignment = VerticalAlignment.Top;
            loadData2.Margin = new Thickness(-centerOffset, heightOffset += buttonDistance, 0, 0);
            loadData2.Width = buttonWidth;
            loadData2.Height = buttonHeight;
            loadData2.MouseEnter += new MouseEventHandler(MouseEnters);
            loadData2.MouseLeave += new MouseEventHandler(MouseExits);
            loadData2.Background = new SolidColorBrush(Colors.Transparent);
            if (fileNames.Count > 1)
            {
                string saveName = fileNames[1].Substring(0, fileNames[1].IndexOf('_'));
                loadData2.Tag = fileNames[1];
                loadData2.Content = saveName;
                loadData2.Click += new RoutedEventHandler(LoadFile);
                loadData2.FontSize = 25;
            }
            this.grid.Children.Add(loadData2);
            loadData3.VerticalAlignment = VerticalAlignment.Top;
            loadData3.Margin = new Thickness(-centerOffset, heightOffset += buttonDistance, 0, 0);
            loadData3.Width = buttonWidth;
            loadData3.Height = buttonHeight;
            loadData3.MouseEnter += new MouseEventHandler(MouseEnters);
            loadData3.MouseLeave += new MouseEventHandler(MouseExits);
            loadData3.Background = new SolidColorBrush(Colors.Transparent);
            if (fileNames.Count > 2)
            {
                string saveName = fileNames[2].Substring(0, fileNames[2].IndexOf('_'));
                loadData3.Tag = fileNames[2];
                loadData3.Content = saveName;
                loadData3.Click += new RoutedEventHandler(LoadFile);
                loadData3.FontSize = 25;
            }
            this.grid.Children.Add(loadData3);
            loadData4.VerticalAlignment = VerticalAlignment.Top;
            loadData4.Margin = new Thickness(-centerOffset, heightOffset += (int)(buttonDistance * 0.85), 0, 0);
            loadData4.Width = buttonWidth;
            loadData4.Height = buttonHeight;
            loadData4.MouseEnter += new MouseEventHandler(MouseEnters);
            loadData4.MouseLeave += new MouseEventHandler(MouseExits);
            loadData4.Background = new SolidColorBrush(Colors.Transparent);
            if (fileNames.Count > 3)
            {
                string saveName = fileNames[3].Substring(0, fileNames[3].IndexOf('_'));
                loadData4.Tag = fileNames[3];
                loadData4.Content = saveName;
                loadData4.Click += new RoutedEventHandler(LoadFile);
                loadData4.FontSize = 25;
            }
            this.grid.Children.Add(loadData4);
        }

        private void ChangeToMenuScreen()
        {

            BitmapImage nextScreen = new BitmapImage();
            nextScreen.BeginInit();
            nextScreen.UriSource = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name + ";component/Resources/OfficialMenuScreen.png");
            nextScreen.EndInit();
            menuScreen.Source = nextScreen;
            this.grid.Children.Clear();
            Image newScreen = new Image();
            newScreen.Source = nextScreen;
            this.grid.Children.Add(newScreen);
            resume = new Button();
            double buttonWidth = this.Width * 0.43;
            double buttonHeight = this.Height * 0.14;
            int heightOffset = (int)(this.Height * 0.1);
            int centerOffset = (int)(this.Width * 0.028);
            int buttonDistance = (int)(this.Height * 0.22);
            resume.VerticalAlignment = VerticalAlignment.Top;
            resume.Margin = new Thickness(centerOffset, heightOffset, 0, 0);
            resume.Width = buttonWidth;
            resume.Height = buttonHeight;
            resume.MouseEnter += new MouseEventHandler(MouseEnters);
            resume.MouseLeave += new MouseEventHandler(MouseExits);
            resume.Opacity = 0;
            resume.Click += new RoutedEventHandler(ResumeGame);
            this.grid.Children.Add(resume);
            newGame = new Button();
            newGame.VerticalAlignment = VerticalAlignment.Top;
            newGame.Margin = new Thickness(centerOffset, heightOffset += buttonDistance, 0, 0);
            newGame.Width = buttonWidth;
            newGame.Height = buttonHeight;
            newGame.MouseEnter += new MouseEventHandler(MouseEnters);
            newGame.MouseLeave += new MouseEventHandler(MouseExits);
            newGame.Click += new RoutedEventHandler(NewGame);
            newGame.Opacity = 0;
            this.grid.Children.Add(newGame);
            saveGame = new Button();
            saveGame.VerticalAlignment = VerticalAlignment.Top;
            saveGame.Margin = new Thickness(centerOffset, heightOffset += buttonDistance, 0, 0);
            saveGame.Width = buttonWidth;
            saveGame.Height = buttonHeight;
            saveGame.MouseEnter += new MouseEventHandler(MouseEnters);
            saveGame.MouseLeave += new MouseEventHandler(MouseExits);
            saveGame.Opacity = 0;
            saveGame.Click += new RoutedEventHandler(SaveGame);
            this.grid.Children.Add(saveGame);
            exitGame = new Button();
            exitGame.VerticalAlignment = VerticalAlignment.Top;
            exitGame.Margin = new Thickness(centerOffset, heightOffset += (int)(buttonDistance * 0.85), 0, 0);
            exitGame.Width = buttonWidth;
            exitGame.Height = buttonHeight;
            exitGame.MouseEnter += new MouseEventHandler(MouseEnters);
            exitGame.MouseLeave += new MouseEventHandler(MouseExits);
            exitGame.Click += new RoutedEventHandler(ExitGame);
            exitGame.Opacity = 0;
            this.grid.Children.Add(exitGame);
        }

        private void EscapePressed (object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                ChangeToMenuScreen();
            }
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            Button fileButton = (Button)sender;
            fReader.Initialize(out map, fileButton.Tag.ToString());
            if (boardScreen != null)
            {
                boardScreen.Close();
            }
            boardScreen = new GameGrid(map, this);
            boardScreen.Show();
            this.Visibility = Visibility.Collapsed;
            ChangeToMenuScreen();
        }
    }
}

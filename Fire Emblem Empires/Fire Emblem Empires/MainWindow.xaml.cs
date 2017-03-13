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
            startButton.Width = menuScreen.Width;
            this.Width = menuScreen.Width;
            this.Height = menuScreen.Height;
            menuScreen.MouseUp += new MouseButtonEventHandler(ChangeScreenClick);
        }

        private void ChangeScreenClick(Object sender, RoutedEventArgs e)
        {
            string imageSource = menuScreen.Source.ToString();
            if (sender == startButton)
            {

                BitmapImage nextScreen = new BitmapImage();
                nextScreen.BeginInit();
                nextScreen.UriSource = new Uri(@"pack://application:,,,/" + Assembly.GetExecutingAssembly().GetName().Name  +";component/Resources/OfficialMenuScreen.png");
                nextScreen.EndInit();
                menuScreen.Source = nextScreen;
                this.grid.Children.Remove(startButton);
                Image newScreen = new Image();
                newScreen.Source = nextScreen;
                this.grid.Children.Add(newScreen);
                Button resume = new Button();
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
                //this.grid.Children.Add(resume);
                Button newGame = new Button();
                newGame.VerticalAlignment = VerticalAlignment.Top;
                newGame.Margin = new Thickness(centerOffset, heightOffset += buttonDistance, 0, 0);
                newGame.Width = buttonWidth;
                newGame.Height = buttonHeight;
                newGame.MouseEnter += new MouseEventHandler(MouseEnters);
                newGame.MouseLeave += new MouseEventHandler(MouseExits);
                newGame.Click += new RoutedEventHandler(NewGame);
                newGame.Opacity = 0;
                this.grid.Children.Add(newGame);
                Button saveGame = new Button();
                saveGame.VerticalAlignment = VerticalAlignment.Top;
                saveGame.Margin = new Thickness(centerOffset, heightOffset += buttonDistance, 0, 0);
                saveGame.Width = buttonWidth;
                saveGame.Height = buttonHeight;
                saveGame.MouseEnter += new MouseEventHandler(MouseEnters);
                saveGame.MouseLeave += new MouseEventHandler(MouseExits);
                saveGame.Opacity = 0;
                //this.grid.Children.Add(saveGame);
                Button exitGame = new Button();
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
        }

        private void MouseEnters(object sender, MouseEventArgs e)
        {
            Button temp = (Button)sender;
            temp.Opacity = 0.2;
        }

        private void MouseExits(object sender, MouseEventArgs e)
        {
            Button temp = (Button)sender;
            temp.Opacity = 0;
        }

        private void ExitGame(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            fReader.Initialize(out map);
            GameGrid grid = new GameGrid(map);
            grid.Show();
            this.Close();
        }
    }
}

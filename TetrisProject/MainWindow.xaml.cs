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
using System.Text.RegularExpressions;
using System.IO;

namespace TetrisProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {  
        private readonly ImageSource[] tileImages = new ImageSource[]
       {
            new BitmapImage(new Uri("Assets\\TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\TileRed.png", UriKind.Relative))
       };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets\\TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets\\Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] imgCtrls;
        private GameHandler handler;
        private string path;
        private List<User> usersScores;

        private void ReadDatabase() 
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                string[] splittedStrings;
                string nick;
                int score;
                while ((line = sr.ReadLine()) != null)
                {
                    splittedStrings = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    nick = splittedStrings[0];
                    score = Convert.ToInt32(splittedStrings[1]);
                    usersScores.Add(new User(nick, score));
                }
            }
        }
        private void SetPlaceholders()
        {
            int index = usersScores.Count() - 1;
            for (int i= index; i<5; i++)
            {
                usersScores.Add(new User("Not ranked yet", 0));
            }
        }
        private int CompareUsersByScore(User x, User y)
        {
            if (x.score < y.score)
                return 1;
            else if (x.score > y.score)
                return -1;
            else return 0;
        }
        private void AssignDatabaseToRanking()
        {
            First.Text = "1. " + usersScores[0].nickname + " " + usersScores[0].score;
            Second.Text = "2. " +  usersScores[1].nickname + " " + usersScores[1].score;
            Third.Text = "3. " + usersScores[2].nickname + " " + usersScores[2].score;
            Fourth.Text = "4. " + usersScores[3].nickname + " " + usersScores[3].score;
            Fifth.Text = "5. " + usersScores[4].nickname + " " + usersScores[4].score;
        }
        private Image[,] SetTetrisCanvas()
        {
            Image[,] imgCtrlls = new Image[22,10];
            int cellSize = 25;

            for (int r = 0; r < 22; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Image imgCtrl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imgCtrl, (r - 2) * cellSize);
                    Canvas.SetLeft(imgCtrl, c * cellSize);
                    GameCanvas.Children.Add(imgCtrl);
                    imgCtrlls[r, c] = imgCtrl;
                }
            }
            return imgCtrlls;
        }
        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imgCtrls[p.row, p.column].Opacity = 1;
                imgCtrls[p.row, p.column].Source = tileImages[block.id];
            }
        }
        private void DrawNext(Block block)
        {
            NextBlock.Source = blockImages[block.id];
        }
        private void DrawBoard(GameBoard board)
        {
            for (int r = 0; r < 22; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    int id = board[r,c];
                    imgCtrls[r, c].Opacity = 1;
                    imgCtrls[r, c].Source = tileImages[id];
                }
            }
        }
        private async Task Game()
        {
            while (!handler.IsGameEnded())
            {
                await Task.Delay(300);
                handler.MoveDown();
                handler.CheckRows();
                DrawBoard(handler.ReturnGameBoard());
                DrawBlock(handler.ReturnCurrentBlock());
                DrawNext(handler.ReturnNextBlock());
                Score.Text = $"Score: {handler.ReturnCurrentScore()}";
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            imgCtrls = SetTetrisCanvas();
            usersScores = new List<User>();

            path = Directory.GetCurrentDirectory();
            path += "\\database.txt";

            if (!File.Exists(path))
            { 
                StreamWriter sw = File.CreateText(path); 
            }
            else
            {
                ReadDatabase();
                usersScores.Sort(CompareUsersByScore);
                SetPlaceholders();
                AssignDatabaseToRanking();
            }
                
        }
        private void VolumeClick(object sender, RoutedEventArgs e)
        {
            VolumePanel.Visibility = Visibility.Visible;
        }
             
        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            VolumePanel.Visibility = Visibility.Hidden;
            RankingPanel.Visibility = Visibility.Hidden;
        }
        private void RankingClick(object sender, RoutedEventArgs e)
        {
            RankingPanel.Visibility = Visibility.Visible;
        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private async void StartGameClick(object sender, RoutedEventArgs e)
        {
            handler = new GameHandler();
            StartingMenu.Visibility = Visibility.Hidden;
            await Game();
            FinalScore.Text = $"Final Score: {handler.ReturnCurrentScore()}";
            EndGamePanel.Visibility = Visibility.Visible;
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Regex pattern = new Regex ( "^[A-za-z]+$" );

            if (pattern.IsMatch(Nickname.Text))
            {
                File.AppendAllText(path, Nickname.Text + " " + handler.ReturnCurrentScore() + "\n");
                usersScores.Add(new User(Nickname.Text, handler.ReturnCurrentScore()));
                usersScores.Sort(CompareUsersByScore);
                AssignDatabaseToRanking();
                StartingMenu.Visibility = Visibility.Visible;
                EndGamePanel.Visibility = Visibility.Hidden;
            }
            else
                MessageBox.Show("You have entered wrong nickname! Try again!", "Wrong nickname");
            Nickname.Text = "";
        }

        private void VolumeValueChanged(object sender, RoutedEventArgs e)
        {

        }
        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    handler.MoveLeft();
                    break;
                case Key.Right:
                    handler.MoveRight();
                    break;
                case Key.A:
                    handler.RotateCounterClockwise();
                    break;
                case Key.D:
                    handler.RotateClockwise();
                    break;
                default:
                    return;
            }
        }
    }
}

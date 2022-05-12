﻿using System;
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
using System.Media;

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
        private DatabaseManager database;
        private Regex pattern;
        private MediaElement medi;
        private void AssignDatabaseToRanking()
        {
            First.Text = "1. " + database[0].nickname + " " + database[0].score;
            Second.Text = "2. " + database[1].nickname + " " + database[1].score;
            Third.Text = "3. " + database[2].nickname + " " + database[2].score;
            Fourth.Text = "4. " + database[3].nickname + " " + database[3].score;
            Fifth.Text = "5. " + database[4].nickname + " " + database[4].score;
        }
        private void PlayMusic()
        {
            medi = new MediaElement();
            medi.LoadedBehavior = MediaState.Play;
            medi.UnloadedBehavior = MediaState.Manual;
            medi.Source = new Uri("C:\\Users\\wikar\\OneDrive\\Pulpit\\PROGRAMOWANIE\\Tetris\\TetrisProject\\Assets\\tet.wav");
            medi.MediaEnded += new System.Windows.RoutedEventHandler(medi_MediaEnded);
            medi.Play();
        }
        public void medi_MediaEnded(object sender, RoutedEventArgs e)
        {
            medi.Position = TimeSpan.Zero;
            medi.Play();
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
        private void DrawGhostBlock(Block block, int distance)
        {
            foreach (Position p in block.TilePositions())
            {
                imgCtrls[p.row+distance, p.column].Opacity = 0.25;
                imgCtrls[p.row+distance, p.column].Source = tileImages[block.id];
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
                DrawGhostBlock(handler.ReturnCurrentBlock(), handler.CalculateDistance());
                DrawBlock(handler.ReturnCurrentBlock());
                DrawNext(handler.ReturnNextBlock());
                Score.Text = $"Score: {handler.ReturnCurrentScore()}";
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            database = new DatabaseManager();
            pattern = new Regex("^[A-za-z]+$");
            imgCtrls = SetTetrisCanvas();
            database.ReadFromDatabase();
            AssignDatabaseToRanking();

            PlayMusic();
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
            if (pattern.IsMatch(Nickname.Text))
            {
                database.UploadDatabase(Nickname.Text, handler.ReturnCurrentScore());
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
            medi.Volume = VolumeSlider.Value / 100.0f;
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
                case Key.Down:
                    handler.HardDrop(handler.CalculateDistance());
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

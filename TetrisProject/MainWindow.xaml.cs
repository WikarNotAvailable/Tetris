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
            
        private readonly Image[,] imgCtrls;
        private GameBoard board = new GameBoard();
        private Block block = new BlockS(); 

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
            while (true)
            {
                await Task.Delay(100);
                DrawBoard(board);
                DrawBlock(block);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            imgCtrls = SetTetrisCanvas();
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
            StartingMenu.Visibility = Visibility.Hidden;
            await Game();
        }

        private void VolumeValueChanged(object sender, RoutedEventArgs e)
        {

        }
        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    block.Move(0, -1);
                    break;
                case Key.Right:
                    block.Move(0, 1);
                    break;
                case Key.Down:
                    block.Move(1, 0);
                    break;
                case Key.A:
                    block.RotateCounterClockwise();
                    break;
                case Key.D:
                    block.RotateClockwise();
                    break;
                default:
                    return;
            }
        }
    }
}

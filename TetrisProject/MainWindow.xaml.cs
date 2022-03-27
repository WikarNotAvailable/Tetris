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

namespace TetrisProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
      
        private void VolumeClick(object sender, RoutedEventArgs e)
        {
            VolumePanel.Visibility = Visibility.Visible;
        }
             
        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            VolumePanel.Visibility = Visibility.Hidden;
        }
        private void RankingClick(object sender, RoutedEventArgs e)
        {

        }
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void StartGameClick(object sender, RoutedEventArgs e)
        {
            StartingMenu.Visibility = Visibility.Hidden;
        }
        private void GameCanvasLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

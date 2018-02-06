using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace Poker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameWindow gameWindow;
        AutoPlay autoPlay;
        Statistics statistics;

        public MainWindow()
        {
            InitializeComponent();                              
        }

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                App.Current.Properties["P1"] = txt_P1.Text;
                App.Current.Properties["P2"] = txt_P2.Text;

                if (cmb_GameType.Text == "Regular")
                {
                    if (txt_P1.Text == "" || txt_P2.Text == "")
                    {
                        throw new Exception("Choose playernames");
                    }
                    gameWindow = new GameWindow();
                    gameWindow.Show();
                }
                else if (cmb_GameType.Text == "Autoplay")
                {
                    if (txt_P1.Text == "" || txt_P2.Text == "")
                    {
                        throw new Exception("Choose playernames");
                    }
                    autoPlay = new AutoPlay();
                    autoPlay.Show();
                }
                else if (cmb_GameType.Text == "Statistical")
                {
                    statistics = new Statistics();
                    statistics.Show();
                }
                else if (cmb_GameType.Text == "")
                {
                    throw new Exception("Choose a gametype");
                }

                wnd_Start.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }      

        private void cmb_GameType_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cmb_GameType.Text == "Statistical")
            {
                lbl_P1.Visibility = Visibility.Hidden;
                lbl_P2.Visibility = Visibility.Hidden;
                txt_P1.Visibility = Visibility.Hidden;
                txt_P2.Visibility = Visibility.Hidden;
            }
            else
            {
                lbl_P1.Visibility = Visibility.Visible;
                lbl_P2.Visibility = Visibility.Visible;
                txt_P1.Visibility = Visibility.Visible;
                txt_P2.Visibility = Visibility.Visible;
            }
        }
       
    }   
}

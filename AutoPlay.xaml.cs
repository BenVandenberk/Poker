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
using System.Windows.Shapes;
using System.Diagnostics;

namespace Poker
{
    /// <summary>
    /// Interaction logic for AutoPlay.xaml
    /// </summary>
    public partial class AutoPlay : Window
    {
        private PokerGame Game = new PokerGame((string)App.Current.Properties["P1"], (string)App.Current.Properties["P2"]);
        private string logLine = "";
        private int gameCount = 0;
        private bool stop = false;
        private Stopwatch sw = new Stopwatch();
        
        public AutoPlay()
        {
            InitializeComponent();
            lbl_P1Name.Content = Game.Player1.Name;
            lbl_P2Name.Content = Game.Player2.Name; 
        }

        private void btn_Run_Click(object sender, RoutedEventArgs e)
        {
            disableRadioButtons();
            btn_Menu.IsEnabled = false;
            int checkedRbtn = getChecked();
            stop = false;
            sw.Reset();

            sw.Start();

            gameCount = 0;
            lbl_GameCount.Content = gameCount;

            img_P1C1.Source = null;
            img_P1C2.Source = null;
            img_P2C1.Source = null;
            img_P2C2.Source = null;
            img_Flop1.Source = null;
            img_Flop2.Source = null;
            img_Flop3.Source = null;
            img_Turn.Source = null;
            img_River.Source = null;
            
            while (!stop)
            {                
                Game.Player1.Hand.Add(Game.Deck.DealCard());              
                Game.Player2.Hand.Add(Game.Deck.DealCard());                
                Game.Player1.Hand.Add(Game.Deck.DealCard());
                Game.Player2.Hand.Add(Game.Deck.DealCard());
                Game.TableCards.Add(Game.Deck.DealCard());
                Game.TableCards.Add(Game.Deck.DealCard());
                Game.TableCards.Add(Game.Deck.DealCard());
                Game.TableCards.Add(Game.Deck.DealCard());
                Game.TableCards.Add(Game.Deck.DealCard());               

                try
                {
                    Game.DecideWinner();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                gameCount++;              

                if ((int)Game.Player1.BestHand.PokerCombination == checkedRbtn || (int)Game.Player2.BestHand.PokerCombination == checkedRbtn)
                {
                    stop = true;                    
                    lbl_GameCount.Content = gameCount;
                    lbl_HandCount.Content = gameCount * 2;
                    lbl_PointsP1.Content = Game.Player1.Points;
                    lbl_PointsP2.Content = Game.Player2.Points;

                    sw.Stop();
                    lbl_Time.Content = sw.ElapsedMilliseconds / 1000.0 + " s";

                    img_P1C1.Source = new BitmapImage(new Uri(Game.Player1.Hand[0].ImagePath(), UriKind.Relative));
                    img_P1C2.Source = new BitmapImage(new Uri(Game.Player1.Hand[1].ImagePath(), UriKind.Relative));
                    img_P2C1.Source = new BitmapImage(new Uri(Game.Player2.Hand[0].ImagePath(), UriKind.Relative));
                    img_P2C2.Source = new BitmapImage(new Uri(Game.Player2.Hand[1].ImagePath(), UriKind.Relative));

                    img_Flop1.Source = new BitmapImage(new Uri(Game.TableCards[0].ImagePath(), UriKind.Relative));
                    img_Flop2.Source = new BitmapImage(new Uri(Game.TableCards[1].ImagePath(), UriKind.Relative));
                    img_Flop3.Source = new BitmapImage(new Uri(Game.TableCards[2].ImagePath(), UriKind.Relative));
                    img_Turn.Source = new BitmapImage(new Uri(Game.TableCards[3].ImagePath(), UriKind.Relative));
                    img_River.Source = new BitmapImage(new Uri(Game.TableCards[4].ImagePath(), UriKind.Relative));

                    txt_Log.Text += "///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;
                    txt_Log.Text += "---------------------------------------------BINGO!-----------------------------------------------------" + Environment.NewLine;
                    txt_Log.Text += "///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////" + Environment.NewLine;

                    txt_Log.Text += String.Format("The flop: {0}, {1}, {2}.", Game.TableCards[0], Game.TableCards[1], Game.TableCards[2]) + Environment.NewLine;
                    txt_Log.Text += String.Format("The turn: {0}.", Game.TableCards[3]) + Environment.NewLine;
                    txt_Log.Text += String.Format("The river: {0}.", Game.TableCards[4]) + Environment.NewLine;
                    txt_Log.Text += Game.Player1.Name + " has " + PokerHelp.PokerCombinationToString(Game.Player1.BestHand.PokerCombination) + Environment.NewLine;
                    txt_Log.Text += Game.Player2.Name + " has " + PokerHelp.PokerCombinationToString(Game.Player2.BestHand.PokerCombination) + Environment.NewLine;
                    if (Game.Player1.Won && !Game.Player2.Won)
                    {
                        txt_Log.Text += Game.Player1.Name + " wins!" + Environment.NewLine;
                    }
                    else if (Game.Player2.Won && !Game.Player1.Won)
                    {
                        txt_Log.Text += Game.Player2.Name + " wins!" + Environment.NewLine;
                    }
                    else if (Game.Player1.Won && Game.Player2.Won)
                    {
                        txt_Log.Text += "Split pot!" + Environment.NewLine;
                    }
                    txt_Log.Text += Environment.NewLine;                    
                }
                else
                {
                    Game.Reset();
                }
            }

            Game.Reset();
            enableRadioButtons();
            btn_Menu.IsEnabled = true;
        }

        private void txt_Log_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_Log.CaretIndex = txt_Log.Text.Length;
            txt_Log.ScrollToEnd();
        }

        private void disableRadioButtons()
        {
            rbt_1.IsEnabled = false;
            rbt_2.IsEnabled = false;
            rbt_3.IsEnabled = false;
            rbt_4.IsEnabled = false;
            rbt_5.IsEnabled = false;
            rbt_6.IsEnabled = false;
            rbt_7.IsEnabled = false;
            rbt_8.IsEnabled = false;
            rbt_9.IsEnabled = false;
            rbt_10.IsEnabled = false;
        }

        private void enableRadioButtons()
        {
            rbt_1.IsEnabled = true;
            rbt_2.IsEnabled = true;
            rbt_3.IsEnabled = true;
            rbt_4.IsEnabled = true;
            rbt_5.IsEnabled = true;
            rbt_6.IsEnabled = true;
            rbt_7.IsEnabled = true;
            rbt_8.IsEnabled = true;
            rbt_9.IsEnabled = true;
            rbt_10.IsEnabled = true;
        }

        private int getChecked()
        {
            if (rbt_1.IsChecked == true)
                return 1;
            else if (rbt_2.IsChecked == true)
                return 2;
            else if (rbt_3.IsChecked == true)
                return 3;
            else if (rbt_4.IsChecked == true)
                return 4;
            else if (rbt_5.IsChecked == true)
                return 5;
            else if (rbt_6.IsChecked == true)
                return 6;
            else if (rbt_7.IsChecked == true)
                return 7;
            else if (rbt_8.IsChecked == true)
                return 8;
            else if (rbt_9.IsChecked == true)
                return 9;
            else if (rbt_10.IsChecked == true)
                return 10;

            return 0;
        }

        private void btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            wnd_Autoplay.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
            mw.txt_P1.Text = (string)App.Current.Properties["P1"];
            mw.txt_P2.Text = (string)App.Current.Properties["P2"];
        }
    }
}

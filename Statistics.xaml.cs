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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private PokerGame Game = new PokerGame((string)App.Current.Properties["P1"], (string)App.Current.Properties["P2"]);
        private int gameCount = 0;
        private Stopwatch sw = new Stopwatch();
        private int[] counters = new int[10];
        
        public Statistics()
        {
            InitializeComponent();
        }

        private void btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            wnd_Statistics.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
            mw.txt_P1.Text = (string)App.Current.Properties["P1"];
            mw.txt_P2.Text = (string)App.Current.Properties["P2"];
        }

        private void playAmountOfGames(int amount)
        {
            sw.Start();

            for (int i = 0; i < amount; i++)
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

                Game.DecideWinner();
                gameCount++;

                counters[(int)Game.Player1.BestHand.PokerCombination - 1]++;
                counters[(int)Game.Player2.BestHand.PokerCombination - 1]++;

                Game.Reset();
            }

            lbl_GameCount.Content = gameCount;
            lbl_HandCount.Content = gameCount * 2;

            sw.Stop();
        }

        private void btn_100_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();

            playAmountOfGames(100);
            updateTellers();

            lbl_Time.Content = sw.ElapsedMilliseconds / 1000.0 + " s";

            enableButtons();
        }

        private void btn_1000_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();

            playAmountOfGames(1000);
            updateTellers();

            lbl_Time.Content = sw.ElapsedMilliseconds / 1000.0 + " s";

            enableButtons();
        }

        private void btn_10000_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();

            playAmountOfGames(10000);
            updateTellers();

            lbl_Time.Content = sw.ElapsedMilliseconds / 1000.0 + " s";

            enableButtons();
        }

        private void btn_100000_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();

            playAmountOfGames(100000);
            updateTellers();

            lbl_Time.Content = sw.ElapsedMilliseconds / 1000.0 + " s";

            enableButtons();
        }

        private void btn_1000000_Click(object sender, RoutedEventArgs e)
        {
            disableButtons();

            playAmountOfGames(1000000);
            updateTellers();

            lbl_Time.Content = sw.ElapsedMilliseconds / 1000.0 + " s";

            enableButtons();
        }

        private void updateTellers()
        {
            int som = 0;
            
            lbl_1.Content = counters[0];
            lbl_2.Content = counters[1];
            lbl_3.Content = counters[2];
            lbl_4.Content = counters[3];
            lbl_5.Content = counters[4];
            lbl_6.Content = counters[5];
            lbl_7.Content = counters[6];
            lbl_8.Content = counters[7];
            lbl_9.Content = counters[8];
            lbl_10.Content = counters[9];

            foreach (int i in counters)
            {
                som += i;
            }

            lbl_1P.Content = Math.Round(counters[0] / (double)som * 100.0, 3) + " %";
            lbl_2P.Content = Math.Round(counters[1] / (double)som * 100.0, 3) + " %";
            lbl_3P.Content = Math.Round(counters[2] / (double)som * 100.0, 3) + " %";
            lbl_4P.Content = Math.Round(counters[3] / (double)som * 100.0, 3) + " %";
            lbl_5P.Content = Math.Round(counters[4] / (double)som * 100.0, 3) + " %";
            lbl_6P.Content = Math.Round(counters[5] / (double)som * 100.0, 3) + " %";
            lbl_7P.Content = Math.Round(counters[6] / (double)som * 100.0, 3) + " %";
            lbl_8P.Content = Math.Round(counters[7] / (double)som * 100.0, 3) + " %";
            lbl_9P.Content = Math.Round(counters[8] / (double)som * 100.0, 3) + " %";
            lbl_10P.Content = Math.Round(counters[9] / (double)som * 100.0, 3) + " %";
        }

        private void disableButtons()
        {
            btn_Menu.IsEnabled = false;
            btn_100.IsEnabled = false;
            btn_1000.IsEnabled = false;
            btn_10000.IsEnabled = false;
            btn_100000.IsEnabled = false;
            btn_1000000.IsEnabled = false;
        }

        private void enableButtons()
        {
            btn_Menu.IsEnabled = true;
            btn_100.IsEnabled = true;
            btn_1000.IsEnabled = true;
            btn_10000.IsEnabled = true;
            btn_100000.IsEnabled = true;
            btn_1000000.IsEnabled = true;
        }
    }
}

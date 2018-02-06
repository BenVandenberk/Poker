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

namespace Poker
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Card current;
        private PokerGame Game = new PokerGame((string)App.Current.Properties["P1"], (string)App.Current.Properties["P2"]);
        private string logLine = "";
        private int handCount = 0;
       
        public GameWindow()
        {
            InitializeComponent();
            lbl_P1Name.Content = Game.Player1.Name;
            lbl_P2Name.Content = Game.Player2.Name;             
        }            

        private void btn_Deel_Click(object sender, RoutedEventArgs e)
        {                       
            current = Game.Deck.DealCard();
            Game.Player1.Hand.Add(current);
            img_P1C1.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

            current = Game.Deck.DealCard();
            Game.Player2.Hand.Add(current);
            if (cbx_Hide.IsChecked == false)
            {
                img_P2C1.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));
            }
            else
            {
                img_P2C1.Source = new BitmapImage(new Uri(@"Images\b2fv.png", UriKind.Relative));
            }

            current = Game.Deck.DealCard();
            Game.Player1.Hand.Add(current);
            img_P1C2.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

            current = Game.Deck.DealCard();
            Game.Player2.Hand.Add(current);
            if (cbx_Hide.IsChecked == false)
            {
                img_P2C2.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));
            }
            else
            {
                img_P2C2.Source = new BitmapImage(new Uri(@"Images\b2fv.png", UriKind.Relative));
            }
            
            btn_Deel.IsEnabled = false;
            btn_Table.IsEnabled = true;
            cbx_Hide.IsEnabled = false;
        }

        private void btn_Table_Click(object sender, RoutedEventArgs e)
        {                       
            if (img_Flop1.Source == null)
            {
                logLine += "The flop: ";
                
                current = Game.Deck.DealCard();
                Game.TableCards.Add(current);
                logLine += current.ToString() + ", ";
                img_Flop1.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

                current = Game.Deck.DealCard();
                Game.TableCards.Add(current);
                logLine += current.ToString() + ", ";
                img_Flop2.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

                current = Game.Deck.DealCard();
                Game.TableCards.Add(current);
                logLine += current.ToString() + ".";
                img_Flop3.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

                txt_Log.Text += logLine + Environment.NewLine;
                logLine = "";

                btn_Table.Content = "Turn";
            }
            else if (img_Turn.Source == null)
            {
                logLine += "The turn: ";
                
                current = Game.Deck.DealCard();
                Game.TableCards.Add(current);
                logLine += current.ToString() + ".";
                img_Turn.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

                txt_Log.Text += logLine + Environment.NewLine;
                logLine = "";

                btn_Table.Content = "River";
            }
            else
            {
                logLine += "The river: ";
                
                current = Game.Deck.DealCard();
                Game.TableCards.Add(current);
                logLine += current.ToString() + ".";
                img_River.Source = new BitmapImage(new Uri(current.ImagePath(), UriKind.Relative));

                txt_Log.Text += logLine + Environment.NewLine;
                logLine = "";

                if (cbx_Hide.IsChecked == true)
                {
                    img_P2C1.Source = new BitmapImage(new Uri(Game.Player2.Hand[0].ImagePath(), UriKind.Relative));
                    img_P2C2.Source = new BitmapImage(new Uri(Game.Player2.Hand[1].ImagePath(), UriKind.Relative));
                }

                try
                {
                    Game.DecideWinner();

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
                    else
                    {
                        throw new Exception("Impossible winsituation");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                lbl_PointsP1.Content = Game.Player1.Points;
                lbl_PointsP2.Content = Game.Player2.Points;

                handCount++;
                lbl_HandCount.Content = handCount;
                
                txt_Log.Text += "--------------------------------------------------------------------------------------------------------" + Environment.NewLine;

                btn_Table.IsEnabled = false;
                btn_Restart.IsEnabled = true;
                cbx_Hide.IsEnabled = true;
            }
        }

        private void btn_Restart_Click(object sender, RoutedEventArgs e)
        {
            img_P1C1.Source = null;
            img_P1C2.Source = null;
            img_P2C1.Source = null;
            img_P2C2.Source = null;
            img_Flop1.Source = null;
            img_Flop2.Source = null;
            img_Flop3.Source = null;
            img_Turn.Source = null;
            img_River.Source = null;

            btn_Table.Content = "Flop";

            try
            {
                Game.Reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            btn_Restart.IsEnabled = false;
            btn_Deel.IsEnabled = true;
        }

        private void txt_Log_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_Log.CaretIndex = txt_Log.Text.Length;
            txt_Log.ScrollToEnd();
        }

        private void btn_Menu_Click(object sender, RoutedEventArgs e)
        {
            wnd_Game.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
            mw.txt_P1.Text = (string)App.Current.Properties["P1"];
            mw.txt_P2.Text = (string)App.Current.Properties["P2"];
        }
    }
}

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
using System.Windows.Threading;
using System.Collections.Generic;

namespace Mastermind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string colour1;
        string colour2;
        string colour3;
        string colour4;
        int attemptCounter = 1;
        int secondsCounter = 0;
        int score = 100;
        int numberOfAttempts;
        bool quitBool = false;
        private DispatcherTimer timer = new DispatcherTimer();
        List<Label> label1List = new List<Label>();
        List<Label> label2List = new List<Label>();
        List<Label> label3List = new List<Label>();
        List<Label> label4List = new List<Label>();
        public MainWindow()
        {
            InitializeComponent();
            GenerateColours(out colour1, out colour2, out colour3, out colour4);
            solutionTextBox.Text += $"{colour1}, {colour2}, {colour3}, {colour4}";
            Add10ItemsToLabelList(label1List, colour1Label1, colour1Label2, colour1Label3, colour1Label4, colour1Label5, colour1Label6, colour1Label7, colour1Label8, colour1Label9, colour1Label10);
            Add10ItemsToLabelList(label2List, colour2Label1, colour2Label2, colour2Label3, colour2Label4, colour2Label5, colour2Label6, colour2Label7, colour2Label8, colour2Label9, colour2Label10);
            Add10ItemsToLabelList(label3List, colour3Label1, colour3Label2, colour3Label3, colour3Label4, colour3Label5, colour3Label6, colour3Label7, colour3Label8, colour3Label9, colour3Label10);
            Add10ItemsToLabelList(label4List, colour4Label1, colour4Label2, colour4Label3, colour4Label4, colour4Label5, colour4Label6, colour4Label7, colour4Label8, colour4Label9, colour4Label10);
            timer.Tick += Countdown; //Event koppelen
            timer.Interval = new TimeSpan(0, 0, 1); //Elke seconde
             //Timer starten
        }

        //METHODS
        private void GenerateColours(out string colourSlot1, out string colourSlot2, out string colourSlot3, out string colourSlot4)
        {
            Random rng = new Random();
            List<string> colourList = new List<string>();
            int rngNumber;
            string rngString = "";
            
            for (int i=0; i<4; i++)
            {
                rngNumber = rng.Next(1, 7);
                switch (rngNumber)
                {
                    case 1:
                        rngString = "Red";
                        break;
                    case 2:
                        rngString = "Yellow";
                        break;
                    case 3:
                        rngString = "Orange";
                        break;
                    case 4:
                        rngString = "White";
                        break;
                    case 5:
                        rngString = "Green";
                        break;
                    case 6:
                        rngString = "Blue";
                        break;
                }
                colourList.Add(rngString);
            }
            colourSlot1 = colourList[0];
            colourSlot2 = colourList[1];
            colourSlot3 = colourList[2];
            colourSlot4 = colourList[3];
        }
        private void GenerateBackgrounds(Label label1, Label label2, Label label3, Label label4, out List<string> stringList)
        {
            List<string> colourList = new List<string>();
            colourList.Add(label1.Background.ToString());
            colourList.Add(label2.Background.ToString());
            colourList.Add(label3.Background.ToString());
            colourList.Add(label4.Background.ToString());
            stringList = new List<string>();
            for(int i=0; i<4; i++)
            {
                switch (colourList[i])
                {
                    case "#FFFF0000":
                        stringList.Add("Red");
                        break;
                    case "#FFFFFF00":
                        stringList.Add("Yellow");
                        break;
                    case "#FFFFA500":
                        stringList.Add("Orange");
                        break;
                    case "#FFFFFFFF":
                        stringList.Add("White");
                        break;
                    case "#FF008000":
                        stringList.Add("Green");
                        break;
                    case "#FF0000FF":
                        stringList.Add("Blue");
                        break;
                    default:
                        stringList.Add("Invalid");
                        break;
                }
            }
        }
        
        private void Countdown(object sender, EventArgs e)
        {
            
            secondsCounter++;
            if (secondsCounter % 10 == 0)
            {
                secondsCounter = 0;
                label1List[attemptCounter-1].Background = Brushes.Black;
                label2List[attemptCounter-1].Background = Brushes.Black;
                label3List[attemptCounter - 1].Background = Brushes.Black;
                label4List[attemptCounter - 1].Background = Brushes.Black;
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                attemptCounter++;
                attemptLabel.Content = $"Attempt: {attemptCounter}";
                score -= 8;
                scoreLabel.Content = $"Score: {score}";
                if (attemptCounter > 10)
                {
                    MessageBoxResult result = MessageBox.Show($"You have failed! The correct code was: {colour1} {colour2} {colour3} {colour4}.\nWould you like to try again?", "FAILED", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            GenerateColours(out colour1, out colour2, out colour3, out colour4);
                            solutionTextBox.Text = $"{colour1}, {colour2}, {colour3}, {colour4}";
                            attemptCounter = 1;
                            attemptLabel.Content = "Attempt: 1";
                            secondsCounter = 0;
                            timeLabel.Content = "Seconds: 0";
                            score = 100;
                            scoreLabel.Content = "Score: 100";
                            comboBox1.SelectedIndex = -1;
                            comboBox2.SelectedIndex = -1;
                            comboBox3.SelectedIndex = -1;
                            comboBox4.SelectedIndex = -1;
                            Clear10Labels(label1List);
                            Clear10Labels(label2List);
                            Clear10Labels(label3List);
                            Clear10Labels(label4List);
                            break;
                        case MessageBoxResult.No:
                            quitBool = true;
                            mastermindWindow.Close();
                            break;
                    }
                }
            }
            timeLabel.Content = $"Seconds: {secondsCounter}";
        }
        private void Add10ItemsToLabelList(List<Label> labelList, Label label1, Label label2, Label label3, Label label4, Label label5, Label label6, Label label7, Label label8, Label label9, Label label10)
        {
            labelList.Add(label1);
            labelList.Add(label2);
            labelList.Add(label3);
            labelList.Add(label4);
            labelList.Add(label5);
            labelList.Add(label6);
            labelList.Add(label7);
            labelList.Add(label8);
            labelList.Add(label9);
            labelList.Add(label10);
        }
        private void Clear10Labels(List<Label> labelList)
        {
            for (int i=0; i<10; i++)
            {
                labelList[i].Background = Brushes.Transparent;
                labelList[i].BorderBrush = Brushes.Transparent;
            }
        }
        //EVENT METHODS
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexAttempt = attemptCounter - 1;
            if (comboBox1.SelectedIndex != -1)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        label1List[indexAttempt].Background = Brushes.Red;
                        break;
                    case 1:
                        label1List[indexAttempt].Background = Brushes.Yellow;
                        break;
                    case 2:
                        label1List[indexAttempt].Background = Brushes.Orange;
                        break;
                    case 3:
                        label1List[indexAttempt].Background = Brushes.White;
                        break;
                    case 4:
                        label1List[indexAttempt].Background = Brushes.Green;
                        break;
                    case 5:
                        label1List[indexAttempt].Background = Brushes.Blue;
                        break;
                }
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexAttempt = attemptCounter - 1;
            if (comboBox2.SelectedIndex != -1)
            {
                switch (comboBox2.SelectedIndex)
                {
                    case 0:
                        label2List[indexAttempt].Background = Brushes.Red;
                        break;
                    case 1:
                        label2List[indexAttempt].Background = Brushes.Yellow;
                        break;
                    case 2:
                        label2List[indexAttempt].Background = Brushes.Orange;
                        break;
                    case 3:
                        label2List[indexAttempt].Background = Brushes.White;
                        break;
                    case 4:
                        label2List[indexAttempt].Background = Brushes.Green;
                        break;
                    case 5:
                        label2List[indexAttempt].Background = Brushes.Blue;
                        break;
                }
            }
        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexAttempt = attemptCounter - 1;
            if (comboBox3.SelectedIndex != -1)
            {
                switch (comboBox3.SelectedIndex)
                {
                    case 0:
                        label3List[indexAttempt].Background = Brushes.Red;
                        break;
                    case 1:
                        label3List[indexAttempt].Background = Brushes.Yellow;
                        break;
                    case 2:
                        label3List[indexAttempt].Background = Brushes.Orange;
                        break;
                    case 3:
                        label3List[indexAttempt].Background = Brushes.White;
                        break;
                    case 4:
                        label3List[indexAttempt].Background = Brushes.Green;
                        break;
                    case 5:
                        label3List[indexAttempt].Background = Brushes.Blue;
                        break;
                }
            }
        }

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexAttempt = attemptCounter - 1;
            if (comboBox4.SelectedIndex != -1)
            {
                switch (comboBox4.SelectedIndex)
                {
                    case 0:
                        label4List[indexAttempt].Background = Brushes.Red;
                        break;
                    case 1:
                        label4List[indexAttempt].Background = Brushes.Yellow;
                        break;
                    case 2:
                        label4List[indexAttempt].Background = Brushes.Orange;
                        break;
                    case 3:
                        label4List[indexAttempt].Background = Brushes.White;
                        break;
                    case 4:
                        label4List[indexAttempt].Background = Brushes.Green;
                        break;
                    case 5:
                        label4List[indexAttempt].Background = Brushes.Blue;
                        break;
                }
            }
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {

            int indexAttempt = attemptCounter - 1;
            attemptCounter += 1;
            attemptLabel.Content = $"Attempt: {attemptCounter}";
            secondsCounter = 0;
            List<string> colourList = new List<string>();
            colourList.Add(colour1);
            colourList.Add(colour2);
            colourList.Add(colour3);
            colourList.Add(colour4);
            List<string> backgroundList = new List<string>();
            GenerateBackgrounds(label1List[indexAttempt], label2List[indexAttempt], label3List[indexAttempt], label4List[indexAttempt], out backgroundList);
            if (backgroundList.Contains("Invalid"))
            {
                label1List[indexAttempt].Background = Brushes.Black;
                label2List[indexAttempt].Background = Brushes.Black;
                label3List[indexAttempt].Background = Brushes.Black;
                label4List[indexAttempt].Background = Brushes.Black;
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
                comboBox4.SelectedIndex = -1;
                score -= 8;
                scoreLabel.Content = $"Score: {score}";
                if (attemptCounter > 10)
                {
                    MessageBoxResult result = MessageBox.Show($"You have failed! The correct code was: {colour1} {colour2} {colour3} {colour4}.\nWould you like to try again?", "FAILED", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            GenerateColours(out colour1, out colour2, out colour3, out colour4);
                            solutionTextBox.Text = $"{colour1}, {colour2}, {colour3}, {colour4}";
                            attemptCounter = 1;
                            attemptLabel.Content = "Attempt: 1";
                            secondsCounter = 0;
                            timeLabel.Content = "Seconds: 0";
                            score = 100;
                            scoreLabel.Content = "Score: 100";
                            comboBox1.SelectedIndex = -1;
                            comboBox2.SelectedIndex = -1;
                            comboBox3.SelectedIndex = -1;
                            comboBox4.SelectedIndex = -1;
                            Clear10Labels(label1List);
                            Clear10Labels(label2List);
                            Clear10Labels(label3List);
                            Clear10Labels(label4List);
                            break;
                        case MessageBoxResult.No:
                            quitBool = true;
                            mastermindWindow.Close();
                            break;
                    }
                    return;
                }
                MessageBox.Show("At least one of the combo boxes is empty, try again.");
                return;
            }
            List<string> borderList = new List<string>();
            for (int i=0; i<4; i++)
            {
                if (backgroundList[i] == colourList[i])
                {
                    borderList.Add("DarkRed");
                }
                else if (colourList.Contains(backgroundList[i]))
                {
                    borderList.Add("Wheat");
                }
                else borderList.Add("None");
            }
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (borderList[i] == "DarkRed")
                        {
                            label1List[indexAttempt].BorderBrush = Brushes.DarkRed;
                        }
                        else if (borderList[i] == "Wheat")
                        {
                            label1List[indexAttempt].BorderBrush = Brushes.Wheat;
                            score -= 1;
                        }
                        else
                        {
                            label1List[indexAttempt].BorderBrush = Brushes.Transparent;
                            score -= 2;
                        }
                        break;
                    case 1:
                        if (borderList[i] == "DarkRed")
                        {
                            label2List[indexAttempt].BorderBrush = Brushes.DarkRed;
                        }
                        else if (borderList[i] == "Wheat")
                        {
                            label2List[indexAttempt].BorderBrush = Brushes.Wheat;
                            score -= 1;
                        }
                        else
                        {
                            label2List[indexAttempt].BorderBrush = Brushes.Transparent;
                            score -= 2;
                        }
                        break;
                    case 2:
                        if (borderList[i] == "DarkRed")
                        {
                            label3List[indexAttempt].BorderBrush = Brushes.DarkRed;
                        }
                        else if (borderList[i] == "Wheat")
                        {
                            label3List[indexAttempt].BorderBrush = Brushes.Wheat;
                            score -= 1;
                        }
                        else
                        {
                            label3List[indexAttempt].BorderBrush = Brushes.Transparent;
                            score -= 2;
                        }
                        break;
                    case 3:
                        if (borderList[i] == "DarkRed")
                        {
                            label4List[indexAttempt].BorderBrush = Brushes.DarkRed;
                        }
                        else if (borderList[i] == "Wheat")
                        {
                            label4List[indexAttempt].BorderBrush = Brushes.Wheat;
                            score -= 1;
                        }
                        else
                        {
                            label4List[indexAttempt].BorderBrush = Brushes.Transparent;
                            score -= 2;
                        }
                        break;
                }
            }
            if (borderList[0] == "DarkRed" && borderList[1] == "DarkRed" && borderList[2] == "DarkRed" && borderList[3] == "DarkRed")
            {
                MessageBoxResult result = MessageBox.Show($"The correct code has been found in {attemptCounter.ToString()} attempts. \nWould you like to play again?", "Winner", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        GenerateColours(out colour1, out colour2, out colour3, out colour4);
                        solutionTextBox.Text = $"{colour1}, {colour2}, {colour3}, {colour4}";
                        attemptCounter = 1;
                        attemptLabel.Content = "Attempt: 1";
                        secondsCounter = 0;
                        timeLabel.Content = "Seconds: 0";
                        score = 100;
                        scoreLabel.Content = "Score: 100";
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        comboBox3.SelectedIndex = -1;
                        comboBox4.SelectedIndex = -1;
                        Clear10Labels(label1List);
                        Clear10Labels(label2List);
                        Clear10Labels(label3List);
                        Clear10Labels(label4List);
                        break;
                    case MessageBoxResult.No:
                        quitBool = true;
                        mastermindWindow.Close();
                        break;
                }
                return;
            }
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            scoreLabel.Content = $"Score: {score}";
        }
        private void ToggleDebug(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.F12)
                {
                    if (solutionTextBox.Visibility == Visibility.Hidden)
                    {
                        solutionTextBox.Visibility = Visibility.Visible;
                    }
                    else solutionTextBox.Visibility = Visibility.Hidden;
                }
            }
        }

        private void mastermindWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (quitBool == false)
            {
                e.Cancel = true;
                if (MessageBox.Show("Continue closing the window?", "CLOSING GAME", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                };
            }
        }

        private void fileButton_Click(object sender, RoutedEventArgs e)
        {
            fileButton.Visibility = Visibility.Hidden;
            settingsButton.Visibility = Visibility.Hidden;
            newGameButton.Visibility = Visibility.Visible;
            highscoresButton.Visibility = Visibility.Visible;
            closeButton.Visibility = Visibility.Visible;
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            fileButton.Visibility = Visibility.Hidden;
            settingsButton.Visibility = Visibility.Hidden;
            numberOfAttemptsLabel.Visibility = Visibility.Visible;
            attemptsTextBox.Visibility = Visibility.Visible;
            confirmAttemptsButton.Visibility = Visibility.Visible;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            newGameButton.Visibility = Visibility.Hidden;
            highscoresButton.Visibility = Visibility.Hidden;
            closeButton.Visibility = Visibility.Hidden;
            timer.Start();
            comboBox1.Visibility = Visibility.Visible;
            comboBox2.Visibility = Visibility.Visible;
            comboBox3.Visibility = Visibility.Visible;
            comboBox4.Visibility = Visibility.Visible;
            checkButton.Visibility = Visibility.Visible;
            attemptLabel.Visibility = Visibility.Visible;
            timeLabel.Visibility = Visibility.Visible;
            scoreLabel.Visibility = Visibility.Visible;
        }

        private void confirmAttemptsButton_Click(object sender, RoutedEventArgs e)
        {
            numberOfAttempts = int.Parse(attemptsTextBox.Text);
            fileButton.Visibility = Visibility.Visible;
            settingsButton.Visibility = Visibility.Visible;
            numberOfAttemptsLabel.Visibility = Visibility.Hidden;
            attemptsTextBox.Visibility = Visibility.Hidden;
            confirmAttemptsButton.Visibility = Visibility.Hidden;
        }
    }
}


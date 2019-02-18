using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace MatchingPairsGame
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;

        DateTime start;
        int count = 0;


        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!","!","N","N",",",",","k","k",
            "b","b","c","c","r","r","p","p"
        };

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
            start = StartTime();


        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                 
                Label iconLabel = control as Label;
                

                if (iconLabel != null)
                {
                    int randromNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randromNumber];

                    
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randromNumber);
                }
            }
            

        }

        private void Label_click(object sender, EventArgs e)
        {
            

            if (timer1.Enabled == true)
            {
                return;
            }

            Label clickedLabel = sender as Label;
            

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

               

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    count++;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                count++;


                CheckForWinner();
                

                if (firstClicked.Text == secondClicked.Text)
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\media\Alarm03.wav");
                    simpleSound.Play();

                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                
                timer1.Start();
                
            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\media\chord.wav");
            simpleSound.Play();

            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;


        }



        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }

                }
            }

            

            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\media\KidsCheering.wav");
            simpleSound.Play();

           var stop = StopTime();


            TimeSpan interval = stop - start;
            var totalTime = Convert.ToInt32(interval.TotalSeconds);


            MessageBox.Show($"You mached all the icons in {totalTime} seconds and {count} clicks", "Congratulations and well done! ");
            
           Application.Restart();
        }


        private void NewGameButton_Click(object sender, EventArgs e)
        {
            
            Application.Restart();
            

        }

        public DateTime StartTime()
        {
            DateTime StartTime = DateTime.Now;
            return StartTime;

        }

        public DateTime StopTime()
        {
            DateTime StopTime = DateTime.Now;
            return StopTime;
        }


    }
}

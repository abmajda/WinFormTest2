using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Random randomizer = new Random();

        int
            // addition values
            addend1,
            addend2,
            // subtraction values
            minuend,
            subtrahend,
            // multiplication values
            multiplicand,
            multiplier,
            //division values
            dividend,
            divisor,
            // timer
            timeLeft;

        public void StartTheQuiz()
        {
            // generate random numbers for the test
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // add them to the labels
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // make sure the sum value is 0
            sum.Value = 0;

            // handle subtraction
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // handle multiplication
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // handle division
            divisor = randomizer.Next(2, 11);
            int tempQuotient = randomizer.Next(2, 11);
            dividend = divisor * tempQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) 
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend/divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // if CheckTheAnswer returns true, then the user got the answers right. Stop timer and show a messagebox
            if (CheckTheAnswer())
            {
                timer1.Stop();
                timeLabel.BackColor = DefaultBackColor;
                MessageBox.Show("You got all the answers right!", "Congratulations!");
                startButton.Enabled = true;
            }
            // keep the time updated
            else if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";

                // change the background color when less than 5 seconds remain
                if (timeLeft == 5)
                {
                    timeLabel.BackColor = Color.Red;
                }
            }

            // if time runs out, stop the timer, show a messagebox and fill in the answers
            else
            {
                timer1.Stop();
                timeLabel.BackColor = DefaultBackColor;
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the control
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int LengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, LengthOfAnswer);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }
    }
}

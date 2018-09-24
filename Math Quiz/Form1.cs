using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Math_Quiz
{
    public partial class Form1 : Form
    {
        // Declare an object called randomizer
        Random randomizer = new Random();

        // This integer variable keeps track of the 
        // remaining time.
        int timeLeft;

        // Declare two int variables for addition
        int addend1;
        int addend2;

        // Declare two int variables for subtraction
        int minuend;
        int subtrahend;

        // Declare two int variables for multiplication
        int multiplicand;
        int multiplier;

        // Declare two int variables for division
        int dividend;
        int divisor;

        /**********************************************************************************
         * CheckTheAnswer()
         * Check the answer to see if the user got everything right
         * Return: bool
         * *******************************************************************************/
        private bool CheckTheAnswer()
        {
            // check answers
            if ((addend1 + addend2 == sum.Value)
        && (minuend - subtrahend == difference.Value)
        && (multiplicand * multiplier == product.Value)
        && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        /**********************************************************************************
         * StartTheQuiz()
         * Runs the quiz program
         * Return: void
         * *******************************************************************************/
        public void StartTheQuiz()
        {
            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            
            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            plusLeftLabel.Text  = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text  = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text  = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        // Main program
        public Form1()
        {
            InitializeComponent();
        }

        // Start button
        private void startButton_Click(object sender, EventArgs e)
        {
            // Run the quiz and disable the button
            StartTheQuiz();
            startButton.Enabled = false;

            // Set timeLabel color  to white
            timeLabel.BackColor = Color.White;
        }

        // Timer ticker
        private void timer1_Tick(object sender, EventArgs e)
        {
            // If there is less than 5 seconds left, set timeLabel in red
            if (timeLeft < 7)
            {
                timeLabel.BackColor = Color.Red;
            }

            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer, play a sound,  
                // and show a MessageBox.
                timer1.Stop();
                System.Media.SystemSounds.Question.Play();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");

                // Set timeLabel color to green for success
                timeLabel.BackColor = Color.LightGreen;
                startButton.Enabled = true;
            }

            else if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
            }

            else
            {
                // If the user ran out of time, stop the timer, play a sound, 
                // and show a message
                timer1.Stop();
                System.Media.SystemSounds.Question.Play();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry");

                // Solve and reveal answers
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;

                // Re-enable the start button
                startButton.Enabled = true;
            }
        }

        // Ensure answer box is filled with a value
        private void answer_enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        // Load the date
        private void Form1_Load(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToString("dd, MMMM, yyyy");
        }

        // If the sum is correct, play a sound
        private void sum_ValueChanged(object sender, EventArgs e)
        {
            if (sum.Value == addend1 + addend2)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // If the difference is correct, play a sound
        private void difference_ValueChanged(object sender, EventArgs e)
        {
            if (difference.Value == minuend - subtrahend)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // If the product is correct, play a sound
        private void product_ValueChanged(object sender, EventArgs e)
        {
            if (product.Value == multiplicand * multiplier)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        // If the quotient is correct, play a sound
        private void quotient_ValueChanged(object sender, EventArgs e)
        {
            if (quotient.Value == dividend / divisor)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rock_Paper_Scissors_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enChoice
        {
            Rock,
            Paper,
            Scissors
        }

        enum enWinner
        {
            Player,
            Computer,
            Draw
        }
        struct stGameInfo
        {
            public enChoice PlayerChoice;
            public enChoice ComputerChoice;
            public enWinner Winner;
           
        }

        void ResetGame()
        {
         
            lblPlayer.Tag = 0;
            lblComputer.Tag = 0;
            lblPlayerScore.Text = "0";
            lblComputerScore.Text = "0";

            lblComputerChoice.Text = "";
            lblPlayerChoice.Text = "";

            lblWinner.Text = "Start Game";

            lblGameCount.Tag = 0;
            lblGameCount.Text = "0";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(250, 250, 250, 250);
            Pen pen = new Pen(White);

            pen.Width = 3;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Flat;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Flat;

            e.Graphics.DrawLine(pen, 430, 190, 430, 400);
        }

        enChoice GetComputerChoice()
        {
            Random random = new Random();
            return (enChoice)random.Next(0, 3);
        }

        enWinner WhoIsWinner(enChoice ComputerChoice,enChoice PlayerChoice)
        {
            if (ComputerChoice == PlayerChoice)
                return enWinner.Draw;

            if ((ComputerChoice == enChoice.Rock && PlayerChoice == enChoice.Scissors) ||
                (ComputerChoice == enChoice.Scissors && PlayerChoice == enChoice.Paper) ||
                (ComputerChoice == enChoice.Paper && PlayerChoice == enChoice.Rock))
                return enWinner.Computer;

            return enWinner.Player;

        }

        enChoice ReadPlayerChoice(Button PlayerChoice)
        {
            if (PlayerChoice == btnRock)
            {
               return enChoice.Rock;
            }

            if (PlayerChoice == btnPaper)
            {
                return enChoice.Paper;
            }

            return enChoice.Scissors;
         
        }

        void ChangeGameScore(enWinner Winner)
        {
            if (Winner == enWinner.Player)
                lblPlayer.Tag = Convert.ToInt32(lblPlayer.Tag) + 1;

            if (Winner == enWinner.Computer)
                lblComputer.Tag = Convert.ToInt32(lblComputer.Tag) + 1;


        }

        string GetStringChoice(enChoice Choice)
        {
            switch (Choice)
            {
                case enChoice.Rock:
                    return "Rock";

                case enChoice.Paper:
                    return "Paper";

                default:
                    return "Scissors";
            }
        }

        void ShowGameInfo(stGameInfo GameInfo)
        {
            lblGameCount.Text = lblGameCount.Tag.ToString();
            lblPlayerChoice.Text = GetStringChoice(GameInfo.PlayerChoice);
            lblComputerChoice.Text = GetStringChoice(GameInfo.ComputerChoice);
            lblPlayerScore.Text = lblPlayer.Tag.ToString();
            lblComputerScore.Text = lblComputer.Tag.ToString();

            if (GameInfo.Winner == enWinner.Player)
            {
                lblWinner.Text = "You Win";
                return;
            }
            if (GameInfo.Winner == enWinner.Computer)
            {
                lblWinner.Text = "You Lose";
                return;
            }

            lblWinner.Text = "   Draw";

        }

        stGameInfo GameInfo(Button PlayerChoice)
        {
            stGameInfo GameInfo = new stGameInfo();

            lblGameCount.Tag = Convert.ToInt32(lblGameCount.Tag) + 1;
            GameInfo.PlayerChoice = ReadPlayerChoice(PlayerChoice);
            GameInfo.ComputerChoice = GetComputerChoice();
            GameInfo.Winner = WhoIsWinner(GameInfo.ComputerChoice, GameInfo.PlayerChoice);
            ChangeGameScore(GameInfo.Winner);

            return GameInfo;
        }

        void StartGame(Button PlayerChoice)
        {
            ShowGameInfo(GameInfo(PlayerChoice));
        }

        private void PlayerChoice_Click(object sender, EventArgs e)
        {
            StartGame((Button)sender);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

      
    }
}

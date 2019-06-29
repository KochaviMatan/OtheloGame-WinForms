using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using Ex05_Othelo;

namespace Ex05_Othelo
{
    public class OtheloGameFormUI : Form
    {
        private readonly byte r_BoardSize;
        private Square[,] m_CoordinationButtons;
        private OtheloGameManager m_OtheloGame = null;
        private bool m_IsAgainstComputer;

        public OtheloGameFormUI(byte i_OtheloBoardSize, bool i_IsAgainstComputer)
        {
            r_BoardSize = i_OtheloBoardSize;
            m_IsAgainstComputer = i_IsAgainstComputer;
            m_CoordinationButtons = new Square[r_BoardSize, r_BoardSize];

            initializeGameManager();
            initializeOtheloGameFormUI();
            initializeButtonsComponent();
            initializeStartingPosition();
        }

        private void initializeOtheloGameFormUI()
        {
            /// 
            /// OtheloGameFormUI
            ///
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size((m_OtheloGame.GamePanel.Size * 40) + (2 * 12), (m_OtheloGame.GamePanel.Size * 40) + (2 * 12));
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.AutoSize = true;
            this.MaximizeBox = false;
            this.Name = "OthelloGame";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = m_OtheloGame.GetCurrentPlayer().Team + "'s Turn";
            this.ResumeLayout(false);
        }

        private void InitializeGame()
        {
            this.ShowDialog();
        }

        private void initializeGameManager()
        {
            if (m_IsAgainstComputer == true)
            {
                m_OtheloGame = new OtheloGameManager(r_BoardSize, "Player1", "Computer", m_IsAgainstComputer);
            }
            else
            {
                m_OtheloGame = new OtheloGameManager(r_BoardSize, "Player1", "Player2", m_IsAgainstComputer);
            }

            m_OtheloGame.InitializeGame();
        }

        private void initializeButtonsComponent()
        {
            for (int x = 0; x < r_BoardSize; x++)
            {
                for (int y = 0; y < r_BoardSize; y++)
                {
                    addEnableButton((byte)x, (byte)y);
                }
            }
        }

        private void addEnableButton(byte i_X, byte i_Y)
        {
            Square coordinationButton = new Square(i_X, i_Y);

            coordinationButton.Location = new Point((i_X * 40) + 12, ((i_Y + 1) * 40) - 28);
            coordinationButton.Name = "Coordination Button";
            coordinationButton.Size = new Size(40, 40);
            coordinationButton.UseVisualStyleBackColor = true;
            m_CoordinationButtons[i_X, i_Y] = coordinationButton;
            coordinationButton.Click += doPlayerMoveAppon_Click;
            Controls.Add(m_CoordinationButtons[i_X, i_Y]);
        }

        private void doPlayerMoveAppon_Click(object sender, EventArgs e)
        {
            Square button = sender as Square;
            bool gameOver;
            bool nextPlayerHaveValidMoves;

            if (m_OtheloGame.GetCurrentPlayer().IsPlayerIsComputer == false)
            {
                m_OtheloGame.SetInputPieceAndFlipAllTheInfluencedPieces(button.squareCoordinates);
            }
            else
            {
                m_OtheloGame.SetComputerPiece();
            }

            UpdatePlayersValidMoves();

            gameOver = checkIfGameOver();

            if (gameOver == true)
            {
                currentGameRoundIsOver();
            }
            else
            {
                nextPlayerHaveValidMoves = checkIfNextPlayerHaveValidMoves();
                changeTurn();

                if (nextPlayerHaveValidMoves == false)
                {                   
                    setPiecesOnBoard();
                    disableUnavabileButtons();
                    string massege = string.Format("{0}'s Player dont have valid move. {1}The turn goes back to {2}'s Player", m_OtheloGame.GetCurrentPlayer().Team, Environment.NewLine, m_OtheloGame.GetOpposingPlayer().Team);
                    MessageBox.Show(massege, "OtheloGame", MessageBoxButtons.OK);
                    changeTurn();
                }

                setPiecesOnBoard();
                disableUnavabileButtons();

                if (m_OtheloGame.GetCurrentPlayer().IsPlayerIsComputer == true)
                {
                    makeADelay();
                    doPlayerMoveAppon_Click(sender, e);
                }
            }
        }

        private void changeTurn()
        {
            m_OtheloGame.ChangeTurn();
            this.Text = m_OtheloGame.GetCurrentPlayer().Team + "'s Turn";
        }

        private bool checkIfGameOver()
        {
            bool gameOver = false;

            if (m_OtheloGame.GetCurrentPlayer().IsHaveValidMove == false)
            {
                if (m_OtheloGame.GetOpposingPlayer().IsHaveValidMove == false)
                {
                    gameOver = true;
                }
            }

            return gameOver;
        }

        private bool checkIfNextPlayerHaveValidMoves()
        {
            bool haveValidMove = true;

            if (m_OtheloGame.GetOpposingPlayer().IsHaveValidMove == false)
            {
                haveValidMove = false;
            }

            return haveValidMove;
        }

        public void UpdatePlayersValidMoves()
        {
            m_OtheloGame.makeCurrentPlayerListOfMoves();
            m_OtheloGame.makeOppositPlayerListOfMoves();
        }

        private void currentGameRoundIsOver()
        {
            setPiecesOnBoard();
            disableUnavabileButtons();
            m_OtheloGame.UpdatePlayerScore();
            showMessageBoxOfRoundOver();
        }

        private void showMessageBoxOfRoundOver()
        {
            string massege = null;
            string gameOverMassege = "GAME OVER!";
            string titleMassege = "Othello Game";
            if (m_OtheloGame.Winner[m_OtheloGame.Round] == null)
            {
                if (m_OtheloGame.Round == (int)eNumberOfRounds.WinsNeededToWin && m_OtheloGame.GetCurrentPlayer().Wins == 0 && m_OtheloGame.GetOpposingPlayer().Wins == 0)
                {
                    massege += string.Format("TIE! Round ({0}/{0}) and no clear winner. {1} {2} ", (int)eNumberOfRounds.Round, Environment.NewLine, gameOverMassege);
                    MessageBox.Show(massege, gameOverMassege, MessageBoxButtons.OK);
                    Application.Exit();
                }
                else
                {
                    massege = string.Format("TIE! ({0}/{1}) {2}Would you like another aound?", m_OtheloGame.Winner[m_OtheloGame.Round].Pieces.Count, m_OtheloGame.Loser[m_OtheloGame.Round].Pieces.Count, Environment.NewLine);
                }
            }
            else
            {
                if (m_OtheloGame.Winner[m_OtheloGame.Round].Wins < (int)eNumberOfRounds.WinsNeededToWin)
                {
                    massege = string.Format("{0}'s Player Won! ({1}/{2}) ({3}/{4}) {5}Would you like another aound?", m_OtheloGame.Winner[m_OtheloGame.Round].Team, m_OtheloGame.Winner[m_OtheloGame.Round].Pieces.Count, m_OtheloGame.Loser[m_OtheloGame.Round].Pieces.Count, m_OtheloGame.Winner[m_OtheloGame.Round].Wins, (int)eNumberOfRounds.Round, Environment.NewLine);
                }
                else
                {
                    massege += string.Format("{0}{1}{2} {3}'s Player Won! ({4}/{5})", Environment.NewLine, gameOverMassege, Environment.NewLine, m_OtheloGame.Winner[m_OtheloGame.Round].Team, m_OtheloGame.Winner[m_OtheloGame.Round].Wins, (int)eNumberOfRounds.Round);
                    MessageBox.Show(massege, titleMassege, MessageBoxButtons.OK);
                    Application.Exit();
                }
            }

            DialogResult gameOverDialog = MessageBox.Show(massege, titleMassege, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (gameOverDialog == DialogResult.No)
            {
                Application.Exit();
            }
            else if (gameOverDialog == DialogResult.Yes)
            {
                InitializeNewRound();
            }
        }

        public void InitializeNewRound()
        {
            m_OtheloGame.IncRoundNumber();
            m_OtheloGame.InitializeGame();
            this.Text = m_OtheloGame.GetCurrentPlayer().Team + "'s Turn";
            initializeStartingPosition();
        }

        public void initializeStartingPosition()
        {
            removeAllPiecesFromBoard();
            setPiecesOnBoard();
            disableUnavabileButtons();
        }

        private void disableUnavabileButtons()
        {
            m_OtheloGame.makeCurrentPlayerListOfMoves();

            foreach (Square button in m_CoordinationButtons)
            {
                button.Enabled = false;
                button.UseVisualStyleBackColor = true;
                button.BackColor = default(Color);

                if (m_OtheloGame.IsValidPlaceToChoose(button.squareCoordinates) == true)
                {
                    if (m_OtheloGame.GetCurrentPlayer().IsPlayerIsComputer == false)
                    {
                        button.Enabled = true;
                        button.BackColor = Color.LightGreen;
                    }
                }

                button.Refresh();
            }
        }

        private void setPiecesOnBoard()
        {
            List<Piece> allThePiecesFormBlackPlayer;
            List<Piece> allThePiecesFormWhitePlayer;

            if (m_OtheloGame.GetCurrentPlayer().Team == Player.eTeam.Red)
            {
                allThePiecesFormBlackPlayer = m_OtheloGame.GetCurrentPlayer().Pieces;
                allThePiecesFormWhitePlayer = m_OtheloGame.GetOpposingPlayer().Pieces;
            }
            else
            {
                allThePiecesFormBlackPlayer = m_OtheloGame.GetOpposingPlayer().Pieces;
                allThePiecesFormWhitePlayer = m_OtheloGame.GetCurrentPlayer().Pieces;
            }

            foreach (Piece blackPiece in allThePiecesFormBlackPlayer)
            {
                m_CoordinationButtons[blackPiece.CoordinatesOnBoard.X, blackPiece.CoordinatesOnBoard.Y].BackgroundImage = Properties.Resources.CoinRed;
                m_CoordinationButtons[blackPiece.CoordinatesOnBoard.X, blackPiece.CoordinatesOnBoard.Y].BackgroundImageLayout = ImageLayout.Stretch;
                m_CoordinationButtons[blackPiece.CoordinatesOnBoard.X, blackPiece.CoordinatesOnBoard.Y].Refresh();
            }

            foreach (Piece whitePiece in allThePiecesFormWhitePlayer)
            {
                m_CoordinationButtons[whitePiece.CoordinatesOnBoard.X, whitePiece.CoordinatesOnBoard.Y].BackgroundImage = Properties.Resources.CoinYellow;
                m_CoordinationButtons[whitePiece.CoordinatesOnBoard.X, whitePiece.CoordinatesOnBoard.Y].BackgroundImageLayout = ImageLayout.Stretch;
                m_CoordinationButtons[whitePiece.CoordinatesOnBoard.X, whitePiece.CoordinatesOnBoard.Y].Refresh();
            }
        }

        private void removeAllPiecesFromBoard()
        {
            foreach (Square Button in m_CoordinationButtons)
            {
                Button.BackgroundImage = null;
            }
        }

        private void makeADelay()
        {
            System.Threading.Thread.Sleep((int)System.TimeSpan.FromSeconds(0.7).TotalMilliseconds);
        }
    }
}

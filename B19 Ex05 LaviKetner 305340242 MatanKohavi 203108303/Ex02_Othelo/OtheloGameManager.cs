using System;
using System.Collections.Generic;

namespace Ex05_Othelo
{
    public class OtheloGameManager
    {
        //--------------------------------------------------------------------------------------//
        //                                   Conts & Enum                                       //
        //--------------------------------------------------------------------------------------//
        private const byte k_RightUpBlack = 0,
                           k_LeftUpWhite = 1,
                           k_LeftDownBlack = 2,
                           k_RightDownWhite = 3,
                           k_NumOfDirections = 8;

        private enum eMoveDirection
        {
            TopRightDirection  = 0,
            TopDirection       = 1,
            TopLeftDirection   = 2,
            LeftDirection      = 3,
            LeftDownDirection  = 4,
            DownDirection      = 5,
            RightDownDirection = 6,
            RightDirection     = 7
        }

        //--------------------------------------------------------------------------------------//
        //                                   Data Members                                       //
        //--------------------------------------------------------------------------------------//
        private static GamePanel s_GamePanel;
        private Player m_Player1;
        private Player m_Player2;
        private int m_Round = 0;
        private static Player.eTeam s_Turn = Player.eTeam.Red;
        private Player[] m_Winner = new Player[(int)eNumberOfRounds.Round];
        private Player[] m_Loser = new Player[(int)eNumberOfRounds.Round];
        private List<Piece>[,] m_ChangeTeamPieces;

        //--------------------------------------------------------------------------------------//
        //                                  Run Game                                            //
        //--------------------------------------------------------------------------------------//
        public OtheloGameManager(byte i_BoardSize, string i_Player1Name, string i_Player2Name, bool i_IsPlayer2IsComputer)
        {
            const bool v_Player1IsAlwaysNotComputer = false;

            s_GamePanel = new GamePanel(i_BoardSize);
            m_Player1 = new Player(i_Player1Name, v_Player1IsAlwaysNotComputer, Player.eTeam.Red);
            m_Player2 = new Player(i_Player2Name, i_IsPlayer2IsComputer, Player.eTeam.Yellow);
            initializeChangeTeamPiecesMember();
        }

        //--------------------------------------------------------------------------------------//
        //                              Initialize Function                                     //
        //--------------------------------------------------------------------------------------//
        public void InitializeGame()
        {
            s_Turn = Player.eTeam.Red;
            m_Player1.Pieces.Clear();
            m_Player1.Score = 0;
            m_Player2.Pieces.Clear();
            m_Player2.Score = 0;
            s_GamePanel.ClearBoard();
            initializeStartPositionOfPiecesOnBoard();
            makeCurrentPlayerListOfMoves();
            makeOppositPlayerListOfMoves();
        }

        private void initializeStartPositionOfPiecesOnBoard()
        {
            Coordinates[] fourInitializeCoordinates = new Coordinates[4];
            initializeFourCoordinats(fourInitializeCoordinates);

            Piece[] fourInitializePieces = new Piece[4];
            initializeFourPieces(fourInitializePieces, fourInitializeCoordinates);

            assignTheFourInitializePiecesToPlayers(fourInitializePieces);

            placeTheFourInitializePiecesOnBoard(fourInitializePieces, fourInitializeCoordinates);
        }

        private void initializeFourPieces(Piece[] i_fourInitializePieces, Coordinates[] i_fourInitializeCoordinates)
        {
            i_fourInitializePieces[k_RightUpBlack]   = new Piece(Player.eTeam.Red, i_fourInitializeCoordinates[k_RightUpBlack]);
            i_fourInitializePieces[k_LeftUpWhite]    = new Piece(Player.eTeam.Yellow, i_fourInitializeCoordinates[k_LeftUpWhite]);
            i_fourInitializePieces[k_LeftDownBlack]  = new Piece(Player.eTeam.Red, i_fourInitializeCoordinates[k_LeftDownBlack]);
            i_fourInitializePieces[k_RightDownWhite] = new Piece(Player.eTeam.Yellow, i_fourInitializeCoordinates[k_RightDownWhite]);
        }

        private void initializeFourCoordinats(Coordinates[] i_fourInitializeCoordinate)
        {
            byte middleRow = (byte)((s_GamePanel.Size / 2) - 1);
            byte middleColumn = middleRow;

            i_fourInitializeCoordinate[k_RightUpBlack] = new Coordinates(middleRow, (byte)(middleColumn + 1));
            i_fourInitializeCoordinate[k_LeftUpWhite] = new Coordinates(middleRow, middleColumn);
            i_fourInitializeCoordinate[k_LeftDownBlack] = new Coordinates((byte)(middleRow + 1), (byte)middleColumn);
            i_fourInitializeCoordinate[k_RightDownWhite] = new Coordinates((byte)(middleRow + 1), (byte)(middleColumn + 1));
        }

        private void assignTheFourInitializePiecesToPlayers(Piece[] i_fourInitializePiece)
        {
            m_Player1.AddPiece(i_fourInitializePiece[k_RightUpBlack]);
            m_Player2.AddPiece(i_fourInitializePiece[k_LeftUpWhite]);
            m_Player1.AddPiece(i_fourInitializePiece[k_LeftDownBlack]);
            m_Player2.AddPiece(i_fourInitializePiece[k_RightDownWhite]);
        }

        private void placeTheFourInitializePiecesOnBoard(Piece[] i_fourInitializePieces, Coordinates[] i_fourInitializeCoordinates)
        {
            s_GamePanel[i_fourInitializeCoordinates[k_RightUpBlack]] = i_fourInitializePieces[k_RightUpBlack];
            s_GamePanel[i_fourInitializeCoordinates[k_LeftUpWhite]] = i_fourInitializePieces[k_LeftUpWhite];
            s_GamePanel[i_fourInitializeCoordinates[k_RightDownWhite]] = i_fourInitializePieces[k_RightDownWhite];
            s_GamePanel[i_fourInitializeCoordinates[k_LeftDownBlack]] = i_fourInitializePieces[k_LeftDownBlack];
        }

        private void initializeChangeTeamPiecesMember()
        {
            m_ChangeTeamPieces = new List<Piece>[s_GamePanel.Size, s_GamePanel.Size];

            for (int i = 0; i < s_GamePanel.Size; i++) 
            {
                for (int j = 0; j < s_GamePanel.Size; j++) 
                {
                    m_ChangeTeamPieces[i, j] = new List<Piece>();
                }
            }
        }

        private void clearListOfCurrectMoves()
        {
            for (int i = 0; i < s_GamePanel.Size; i++)
            {
                for (int j = 0; j < s_GamePanel.Size; j++)
                {
                    m_ChangeTeamPieces[i, j].Clear();
                }
            }
        }

        //--------------------------------------------------------------------------------------//
        //        Make list of currect movment and Sequence lists for change team pieces        //
        //--------------------------------------------------------------------------------------//
        private void makeAListOfCurrectMoves()
        {
            GetCurrentPlayer().IsHaveValidMove = false;

            List<Piece> allThePiecesFormCurrentPlayer = GetCurrentPlayer().Pieces;

            foreach (Piece currentPieceOnPlayerList in allThePiecesFormCurrentPlayer)
            {
                Coordinates currentPieceCoordinate = currentPieceOnPlayerList.CoordinatesOnBoard;

                for (eMoveDirection currentDirection = eMoveDirection.TopRightDirection; (byte)currentDirection < k_NumOfDirections; currentDirection++)
                {
                    List<Piece> currentListOfsequencePieces = new List<Piece>();
                    Coordinates currentCoordinate = currentPieceCoordinate;
                    currentCoordinate = getCellCoordinateToProcced(currentCoordinate, currentDirection);

                    while (isAValidMove(currentCoordinate))
                    {
                        Piece currentRivalPiece = s_GamePanel[currentCoordinate];
                        currentListOfsequencePieces.Add(currentRivalPiece);
                        currentCoordinate = getCellCoordinateToProcced(currentCoordinate, currentDirection);

                        if (checkIfArriveToEmptyCellOnBoard(currentCoordinate))
                        {
                            saveTheSequenceListToChangeTeamPiecesMember(ref currentListOfsequencePieces, currentCoordinate);
                            GetCurrentPlayer().IsHaveValidMove = true;
                            currentListOfsequencePieces.Clear();
                        }
                        else if (isCurrentCoordinateContainAllyPiece(currentCoordinate)) 
                        {
                            currentListOfsequencePieces.Clear();
                            break;
                        }
                    }

                    currentListOfsequencePieces.Clear();
                }
            }
        }

        private bool isCurrentCoordinateContainAllyPiece(Coordinates i_CurrentCoordinate)
        {
            return s_GamePanel.DoesCellExist(i_CurrentCoordinate) && s_GamePanel[i_CurrentCoordinate].Team == s_Turn;
        }

        private bool checkIfArriveToEmptyCellOnBoard(Coordinates i_CurrentCoordinate)
        {
            return s_GamePanel.DoesCellExist(i_CurrentCoordinate) && s_GamePanel[i_CurrentCoordinate] == null;
        }

        private void saveTheSequenceListToChangeTeamPiecesMember(ref List<Piece> io_CurrentListOfsequencePieces, Coordinates i_CurrentCoordinate)
        {
            foreach (Piece RivalPiece in io_CurrentListOfsequencePieces)
            {
                m_ChangeTeamPieces[i_CurrentCoordinate.X, i_CurrentCoordinate.Y].Add(RivalPiece);
            }
        }
        
        private Coordinates getCellCoordinateToProcced(Coordinates i_CurrentCoordinate, eMoveDirection i_CurrentDirection)
        {
            Coordinates nextCoordinateInDirection = new Coordinates();

            switch (i_CurrentDirection)
            {
                case eMoveDirection.TopRightDirection:
                    {
                        moveCoordinateTopRightDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.TopDirection:
                    {
                        moveCoordinateTopDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.TopLeftDirection:
                    {
                        moveCoordinateTopLeftDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.LeftDirection:
                    {
                        moveCoordinateLeftDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.LeftDownDirection:
                    {
                        moveCoordinateLeftDownDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.DownDirection:
                    {
                        moveCoordinateDownDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.RightDownDirection:
                    {
                        moveCoordinateRightDownDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;

                case eMoveDirection.RightDirection:
                    {
                        moveCoordinateRightDirection(ref nextCoordinateInDirection, i_CurrentCoordinate);
                    }

                    break;
            }

            return nextCoordinateInDirection;
        }

        private bool isAValidMove(Coordinates i_CurrentMove)
        {
            bool isValidMove = false;

            if (s_GamePanel.DoesCellExist(i_CurrentMove) && s_GamePanel.DoesCellOccupied(i_CurrentMove))
            {
                if (doesCellOccupiedByEnemey(i_CurrentMove))
                {
                    isValidMove = true;
                }
            }

            return isValidMove;
        }

        private bool doesCellOccupiedByEnemey(Coordinates i_Cell)
        {
            Piece PieceOnCoordinate = s_GamePanel[i_Cell];
            return PieceOnCoordinate.Team != s_Turn;
        }

        private void moveCoordinateTopRightDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X - 1);
            io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y + 1);
        }

        private void moveCoordinateTopDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X - 1);
            io_NextCoordinateInDirection.Y = (byte)i_CurrentCoordinate.Y;
        }

        private void moveCoordinateTopLeftDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X - 1);
            io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y - 1);
        }

        private void moveCoordinateLeftDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)i_CurrentCoordinate.X;
            io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y - 1);
        }

        private void moveCoordinateLeftDownDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X + 1);
            io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y - 1);
        }

        private void moveCoordinateDownDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X + 1);
            io_NextCoordinateInDirection.Y = (byte)i_CurrentCoordinate.Y;
        }

        private void moveCoordinateRightDownDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)(i_CurrentCoordinate.X + 1);
            io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y + 1);
        }

        private void moveCoordinateRightDirection(ref Coordinates io_NextCoordinateInDirection, Coordinates i_CurrentCoordinate)
        {
            io_NextCoordinateInDirection.X = (byte)i_CurrentCoordinate.X;
            io_NextCoordinateInDirection.Y = (byte)(i_CurrentCoordinate.Y + 1);
        }

        //--------------------------------------------------------------------------------------//
        //                                   Properties                                         //
        //--------------------------------------------------------------------------------------//
        public Player GetCurrentPlayer()
        {
            return s_Turn == m_Player1.Team ? m_Player1 : m_Player2;
        }

        public GamePanel GamePanel
        {
            get
            {
                return s_GamePanel;
            }
        }

        public Player[] Winner
        {
            get
            {
                return m_Winner;
            }

            set
            {
                m_Winner = value;
            }
        }

        public Player[] Loser
        {
            get
            {
                return m_Loser;
            }

            set
            {
                m_Loser = value;
            }
        }

        public int Round
        {
            get
            {
                return m_Round;
            }

            set
            {
                m_Round = value;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        //--------------------------------------------------------------------------------------//
        //                                  Public Methods                                      //
        //--------------------------------------------------------------------------------------//
        public void UpdatePlayerScore()
        {
            GetOpposingPlayer().Score = GetOpposingPlayer().Pieces.Count;
            GetCurrentPlayer().Score = GetCurrentPlayer().Pieces.Count;

            if (GetOpposingPlayer().Score > GetCurrentPlayer().Score)
            {
                    Winner[m_Round] = GetOpposingPlayer();
                    Loser[m_Round] = GetCurrentPlayer();
                    GetOpposingPlayer().Wins = GetOpposingPlayer().Wins + 1;               
            }
            else if (GetOpposingPlayer().Score < GetCurrentPlayer().Score)
            {
                    Winner[m_Round] = GetCurrentPlayer();
                    Loser[m_Round] = GetOpposingPlayer();
                    GetCurrentPlayer().Wins = GetCurrentPlayer().Wins + 1;
            }
            else
            {
                Winner[m_Round] = null;
            }
        }

        public void IncRoundNumber()
        {
            Round = Round + 1;
        }

        public void SetComputerPiece()
        {         
            Coordinates maxPiecesToFlipCoordinate;
            int maxPiecesToFlip;

            maxPiecesToFlipCoordinate = getMaxPiecesToFlipCoordinate(out maxPiecesToFlip);

            if (maxPiecesToFlip != 0) 
            {
                SetInputPieceAndFlipAllTheInfluencedPieces(maxPiecesToFlipCoordinate);
            }
        }

        public void SetInputPieceAndFlipAllTheInfluencedPieces(Coordinates i_InputCoordinate)
        {
            Piece newPiece = new Piece(s_Turn, i_InputCoordinate);
            s_GamePanel[i_InputCoordinate] = newPiece;
            
            GetCurrentPlayer().AddPiece(s_GamePanel[i_InputCoordinate]);
            
            foreach (Piece currentPieceToFlip in m_ChangeTeamPieces[i_InputCoordinate.X, i_InputCoordinate.Y])
            {
                currentPieceToFlip.ChangePieceTeam();
                GetCurrentPlayer().AddPiece(currentPieceToFlip);
                GetOpposingPlayer().RemovePiece(currentPieceToFlip);
            }
        }

        public bool IsValidPlaceToChoose(Coordinates i_InputCoordinate)
        {
            return m_ChangeTeamPieces[i_InputCoordinate.X, i_InputCoordinate.Y].Count != 0;
        }        
 
        public void ChangeTurn()
        {
            s_Turn = s_Turn == Player.eTeam.Red ? Player.eTeam.Yellow : Player.eTeam.Red;
        }

        public void makeCurrentPlayerListOfMoves()
        {
            clearListOfCurrectMoves();
            makeAListOfCurrectMoves();
        }

        public void makeOppositPlayerListOfMoves()
        {
            ChangeTurn();
            clearListOfCurrectMoves();
            makeAListOfCurrectMoves();
            ChangeTurn();
        }

        public Player GetOpposingPlayer()
        {
            return s_Turn == m_Player1.Team ? m_Player2 : m_Player1;
        }

        private Coordinates getMaxPiecesToFlipCoordinate(out int o_MaxPiecesToFlip)
        {
            o_MaxPiecesToFlip = 0;
            Coordinates maxPiecesToFlipCoordinate = new Coordinates(0, 0);
            List<Coordinates> maxListPiecesToFlipCoordinate = new List<Coordinates>();

            for (byte i = 0; i < s_GamePanel.Size; i++)
            {
                for (byte j = 0; j < s_GamePanel.Size; j++)
                {
                    if (m_ChangeTeamPieces[i, j].Count > o_MaxPiecesToFlip)
                    {
                        o_MaxPiecesToFlip = m_ChangeTeamPieces[i, j].Count;
                        maxPiecesToFlipCoordinate = new Coordinates(i, j);
                        maxListPiecesToFlipCoordinate.Add(maxPiecesToFlipCoordinate);
                    }
                }
            }

            System.Random randomMaxCoordinate = new System.Random();
            int randomMaxCoordinateLocation = randomMaxCoordinate.Next(0, maxListPiecesToFlipCoordinate.Count - 1);

            return maxListPiecesToFlipCoordinate[randomMaxCoordinateLocation];
        }
    }
}
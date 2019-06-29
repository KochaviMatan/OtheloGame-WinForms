using System.Text;
using System;

namespace Ex05_Othelo
{
    public class GamePanel
    {
        //--------------------------------------------------------------------------------------//
        //                                     Conts                                            //
        //--------------------------------------------------------------------------------------//
        private const char k_FirstLetterOfHeader      = 'A';
        private const byte k_NumberOfSpacesBetwenColumns = 3;
        private const char k_RowsBufferSymbol         = '=';
        private const char k_ColumnsBufferSymbol      = '|';
        private const byte k_BufferLength             = 4;

        //--------------------------------------------------------------------------------------//
        //                                   Data Members                                       //
        //--------------------------------------------------------------------------------------//
        private readonly byte r_Size;
        private Piece[,] m_Board;

        public GamePanel(byte i_Size)
        {
            r_Size = i_Size;
            m_Board = new Piece[r_Size, r_Size];
        }

        //--------------------------------------------------------------------------------------//
        //                                     Indexers                                         //
        //--------------------------------------------------------------------------------------//
        public Piece this[Coordinates i_Cell]
        {
            get
            {
                return m_Board[i_Cell.X, i_Cell.Y];
            }

            set
            {
                m_Board[i_Cell.X, i_Cell.Y] = value;
            }
        }

        public byte Size
        {
            get
            {
                return r_Size;
            }
        }

        //--------------------------------------------------------------------------------------//
        //                                   Build Board                                        //
        //--------------------------------------------------------------------------------------//
        public bool DoesCellExist(Coordinates i_Cell)
        {
            return i_Cell.X >= 0 && i_Cell.X <= (byte)(r_Size - 1) && i_Cell.Y >= 0 && i_Cell.Y <= (byte)(r_Size - 1);
        }

        public bool DoesCellOccupied(Coordinates i_Cell)
        {
            return m_Board[i_Cell.X, i_Cell.Y] != null;
        }
        
        public bool isCellEmpty(Coordinates i_Cell)
        {
            bool isCellEmpty;

            if (m_Board[i_Cell.X, i_Cell.Y] == null)
            {
                isCellEmpty = true;
            }
            else
            {
                isCellEmpty = false;
            }

            return isCellEmpty;
        }

        private string buildEmptyCellString()
        {
            string sequenceOfSpaces = new string(' ', k_NumberOfSpacesBetwenColumns);
            string emptyCellString = string.Format("{0}{1}", k_ColumnsBufferSymbol, sequenceOfSpaces);
            return emptyCellString;
        }

        private string buildEndOfRowString()
        {
            string endOfRowString = string.Format("{0}{1}{2}", k_ColumnsBufferSymbol, System.Environment.NewLine, buildSequenceOfBufferSymbol());
            return endOfRowString;
        }

        private string buildSequenceOfBufferSymbol()
        {
            string sequenceOfSymbol = new string(k_RowsBufferSymbol, (k_BufferLength * r_Size) + 1);
            string bufferRowStringSymbol = string.Format("{0}{1}", " ", sequenceOfSymbol);
            return bufferRowStringSymbol;
        }

        private string buildBoardHeaderString()
        {
            char currentLetter = k_FirstLetterOfHeader;
            string sequenceOfSpaces = new string(' ', k_NumberOfSpacesBetwenColumns);

            StringBuilder boardHeaderString = new StringBuilder(sequenceOfSpaces);

            for (int i = 0; i < r_Size; i++)
            {
                boardHeaderString.Append(currentLetter);
                boardHeaderString.Append(sequenceOfSpaces);
                currentLetter++;
            }

            boardHeaderString.AppendLine();

            return string.Format("{0}{1}{2}", boardHeaderString, buildSequenceOfBufferSymbol(), Environment.NewLine);
        }

        public void ClearBoard()
        {
            for (byte row = 0; row < r_Size; row++)
            {
                for (byte column = 0; column < r_Size; column++)
                {
                    m_Board[row, column] = null;
                }
            }
        }
    }
}

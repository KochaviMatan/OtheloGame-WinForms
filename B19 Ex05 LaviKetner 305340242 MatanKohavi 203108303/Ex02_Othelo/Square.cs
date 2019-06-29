using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ex05_Othelo;

namespace Ex05_Othelo
{
    public class Square : Button
    {
        private Coordinates m_squareCoordinates;

        public Square(byte i_Row, byte i_Column)
        {
            m_squareCoordinates = new Coordinates(i_Row, i_Column);
        }

        public Coordinates squareCoordinates
        {
            get
            {
                return m_squareCoordinates;
            }

            set
            {
                m_squareCoordinates = value;
            }
        }
    }
}

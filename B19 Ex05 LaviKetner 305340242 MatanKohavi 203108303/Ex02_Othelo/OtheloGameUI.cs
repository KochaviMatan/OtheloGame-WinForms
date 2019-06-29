using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ex05_Othelo;

namespace Ex05_Othelo
{
    public class OtheloGameUI 
    {
        private OtheloStartFormUI m_OtheloStartForm; 
        private OtheloGameFormUI m_OtheloGameForm;

        public OtheloGameUI()
        {
            InitializeOtheloStartFormUI();
            InitializeOtheloGameFormUI();
        }

        private void InitializeOtheloStartFormUI()
        {
            m_OtheloStartForm = new OtheloStartFormUI();
            Application.Run(m_OtheloStartForm);
        }

        private void InitializeOtheloGameFormUI()
        {
            if (m_OtheloStartForm.DialogResult == DialogResult.OK)
            {
                m_OtheloGameForm = new OtheloGameFormUI((byte)m_OtheloStartForm.BoardSize, m_OtheloStartForm.IsAgainstCoumputer);
                Application.Run(m_OtheloGameForm);
            }
        }
    }
}

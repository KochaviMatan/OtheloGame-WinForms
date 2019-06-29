using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ex05_Othelo;

namespace Ex05_Othelo
{
    public enum eBoardSize
    {
        Six = 6,
        Eight = 8,
        Ten = 10,
        Twelve = 12
    }

    public class OtheloStartFormUI : Form
    {
        private Button m_BoardSizeButton;
        private Button m_PlayAgainComputerButton;
        private Button m_PlayAgainstYourFriendButton;
        private eBoardSize m_BoardSize = eBoardSize.Six;
        private bool m_IsAgainstCoumputer = false;

        public OtheloStartFormUI()
        {
            InitializeComponent();
        }

        public eBoardSize BoardSize
        {
            get { return m_BoardSize; }
        }

        public bool IsAgainstComputer
        {
            get { return m_IsAgainstCoumputer; }
        }

        private void InitializeComponent()
        {
            this.m_BoardSizeButton = new System.Windows.Forms.Button();
            this.m_PlayAgainComputerButton = new System.Windows.Forms.Button();
            this.m_PlayAgainstYourFriendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            /// 
            /// m_BoardSizeButton
            /// 
            this.m_BoardSizeButton.Location = new System.Drawing.Point(12, 12);
            this.m_BoardSizeButton.Name = "m_BoardSizeButton";
            this.m_BoardSizeButton.Size = new System.Drawing.Size(272, 42);
            this.m_BoardSizeButton.TabIndex = 0;
            this.m_BoardSizeButton.Text = "Board Size: 6x6 (Click to increase)";
            this.m_BoardSizeButton.UseVisualStyleBackColor = true;
            this.m_BoardSizeButton.Click += new System.EventHandler(this.BoardSizeButton_Click);
            /// 
            /// m_PlayAgainComputerButton
            /// 
            this.m_PlayAgainComputerButton.Location = new System.Drawing.Point(12, 74);
            this.m_PlayAgainComputerButton.Name = "m_PlayAgainComputerButton";
            this.m_PlayAgainComputerButton.Size = new System.Drawing.Size(130, 49);
            this.m_PlayAgainComputerButton.TabIndex = 1;
            this.m_PlayAgainComputerButton.Text = "Play against the Computer";
            this.m_PlayAgainComputerButton.UseVisualStyleBackColor = true;
            this.m_PlayAgainComputerButton.Click += new System.EventHandler(this.PlayAgainComputerButton_Click);
            /// 
            /// m_PlayAgainstYourFriendButton
            /// 
            this.m_PlayAgainstYourFriendButton.Location = new System.Drawing.Point(148, 74);
            this.m_PlayAgainstYourFriendButton.Name = "m_PlayAgainstYourFriendButton";
            this.m_PlayAgainstYourFriendButton.Size = new System.Drawing.Size(136, 49);
            this.m_PlayAgainstYourFriendButton.TabIndex = 2;
            this.m_PlayAgainstYourFriendButton.Text = "Play against your friend";
            this.m_PlayAgainstYourFriendButton.UseVisualStyleBackColor = true;
            this.m_PlayAgainstYourFriendButton.Click += new System.EventHandler(this.PlayAgainstYourFriendButton_Click);
            /// 
            /// OtheloStartFormUI
            /// 
            this.ClientSize = new System.Drawing.Size(295, 136);
            this.Controls.Add(this.m_PlayAgainstYourFriendButton);
            this.Controls.Add(this.m_PlayAgainComputerButton);
            this.Controls.Add(this.m_BoardSizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OtheloStartFormUI";
            this.Text = "Othelo game";
            this.Load += new System.EventHandler(this.OtheloStartFormUI_Load);
            this.ResumeLayout(false);
        }

        private void BoardSizeButton_Click(object sender, EventArgs e)
        {
            IncreaseBoardSize();
            ChangeBoardSizeText();
        }

        public void IncreaseBoardSize()
        {
            if (m_BoardSize == eBoardSize.Six)
            {
                m_BoardSize = eBoardSize.Eight;
            }
            else if (m_BoardSize == eBoardSize.Eight)
            {
                m_BoardSize = eBoardSize.Ten;
            }
            else if (m_BoardSize == eBoardSize.Ten)
            {
                m_BoardSize = eBoardSize.Twelve;
            }
            else if (m_BoardSize == eBoardSize.Twelve)
            {
                m_BoardSize = eBoardSize.Six;
            }
        }

        private void ChangeBoardSizeText()
        {
            if (m_BoardSize == eBoardSize.Six)
            {
                this.m_BoardSizeButton.Text = "Board Size: 6x6 (Click to increase)";
            }
            else if (m_BoardSize == eBoardSize.Eight)
            {
                this.m_BoardSizeButton.Text = "Board Size: 8x8 (Click to increase)";
            }
            else if (m_BoardSize == eBoardSize.Ten)
            {
                this.m_BoardSizeButton.Text = "Board Size: 10x10 (Click to increase)";
            }
            else if (m_BoardSize == eBoardSize.Twelve)
            {
                this.m_BoardSizeButton.Text = "Board Size: 12x12 (Click to increase)";
            }
        }

        private void PlayAgainComputerButton_Click(object sender, EventArgs e)
        {
            m_IsAgainstCoumputer = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PlayAgainstYourFriendButton_Click(object sender, EventArgs e)
        {
            m_IsAgainstCoumputer = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public bool IsAgainstCoumputer
        {
            get { return m_IsAgainstCoumputer; } 
        }

        private void OtheloStartFormUI_Load(object sender, EventArgs e)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    public class OtheloGameSettingsForm : Form
    {
        private Button m_DoneButton;
        private RadioButton m_6x6BoardSizeChooseButton;
        private RadioButton m_8x8BoardSizeChooseButton;
        private RadioButton m_10x10BoardSizeChooseButton;
        private CheckBox m_RealRivalPlayerCheckBox;
        private TextBox m_FirstPlayerNameTextBox;
        private TextBox m_SecondPlayerNameTextBox;
        private Label m_BoardSizeLabel;
        private Label m_Player1Label;
        private Label m_PlayersLabel;
        private ErrorProvider m_NameErrorProvider;
        private System.ComponentModel.IContainer m_Components;
        private ErrorProvider m_ErrorProvider1;

        public OtheloGameSettingsForm()
        {
            initializeComponent();
        }

        private void initializeComponent()
        {
            this.m_Components = new System.ComponentModel.Container();
            this.m_DoneButton = new Button();
            this.m_6x6BoardSizeChooseButton = new RadioButton();
            this.m_8x8BoardSizeChooseButton = new RadioButton();
            this.m_10x10BoardSizeChooseButton = new RadioButton();
            this.m_RealRivalPlayerCheckBox = new CheckBox();
            this.m_FirstPlayerNameTextBox = new TextBox();
            this.m_SecondPlayerNameTextBox = new TextBox();
            this.m_BoardSizeLabel = new Label();
            this.m_Player1Label = new Label();
            this.m_PlayersLabel = new Label();
            this.m_NameErrorProvider = new ErrorProvider(this.m_Components);
            this.m_ErrorProvider1 = new ErrorProvider(this.m_Components);
            ((System.ComponentModel.ISupportInitialize)this.m_NameErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.m_ErrorProvider1).BeginInit();
            this.SuspendLayout();
            this.m_DoneButton.Location = new System.Drawing.Point(130, 175);
            this.m_DoneButton.Name = "m_DoneButton";
            this.m_DoneButton.Size = new System.Drawing.Size(80, 35);
            this.m_DoneButton.TabIndex = 0;
            this.m_DoneButton.Text = "Done";
            this.m_DoneButton.UseVisualStyleBackColor = true;
            this.m_DoneButton.Click += new System.EventHandler(this.m_DoneButton_Click);
            this.m_6x6BoardSizeChooseButton.AutoSize = true;
            this.m_6x6BoardSizeChooseButton.Checked = true;
            this.m_6x6BoardSizeChooseButton.Location = new System.Drawing.Point(25, 40);
            this.m_6x6BoardSizeChooseButton.Name = "m_6x6BoardSizeChooseButton";
            this.m_6x6BoardSizeChooseButton.Size = new System.Drawing.Size(48, 17);
            this.m_6x6BoardSizeChooseButton.TabIndex = 1;
            this.m_6x6BoardSizeChooseButton.TabStop = true;
            this.m_6x6BoardSizeChooseButton.Text = "6 x 6";
            this.m_6x6BoardSizeChooseButton.UseVisualStyleBackColor = true;
            this.m_8x8BoardSizeChooseButton.AutoSize = true;
            this.m_8x8BoardSizeChooseButton.Location = new System.Drawing.Point(85, 40);
            this.m_8x8BoardSizeChooseButton.Name = "m_8x8BoardSizeChooseButton";
            this.m_8x8BoardSizeChooseButton.Size = new System.Drawing.Size(48, 17);
            this.m_8x8BoardSizeChooseButton.TabIndex = 3;
            this.m_8x8BoardSizeChooseButton.TabStop = true;
            this.m_8x8BoardSizeChooseButton.Text = "8 x 8";
            this.m_8x8BoardSizeChooseButton.UseVisualStyleBackColor = true;
            this.m_10x10BoardSizeChooseButton.AutoSize = true;
            this.m_10x10BoardSizeChooseButton.Location = new System.Drawing.Point(145, 40);
            this.m_10x10BoardSizeChooseButton.Name = "m_10x10BoardSizeChooseButton";
            this.m_10x10BoardSizeChooseButton.Size = new System.Drawing.Size(60, 17);
            this.m_10x10BoardSizeChooseButton.TabIndex = 2;
            this.m_10x10BoardSizeChooseButton.TabStop = true;
            this.m_10x10BoardSizeChooseButton.Text = "10 x 10";
            this.m_10x10BoardSizeChooseButton.UseVisualStyleBackColor = true;
            this.m_RealRivalPlayerCheckBox.AutoSize = true;
            this.m_RealRivalPlayerCheckBox.Location = new System.Drawing.Point(25, 135);
            this.m_RealRivalPlayerCheckBox.Name = "m_RealRivalPlayerCheckBox";
            this.m_RealRivalPlayerCheckBox.Size = new System.Drawing.Size(67, 17);
            this.m_RealRivalPlayerCheckBox.TabIndex = 4;
            this.m_RealRivalPlayerCheckBox.Text = "Player 2:";
            this.m_RealRivalPlayerCheckBox.UseVisualStyleBackColor = true;
            this.m_RealRivalPlayerCheckBox.CheckedChanged += new System.EventHandler(this.m_RealRivalPlayerCheckBox_CheckedChanged);
            this.m_FirstPlayerNameTextBox.Location = new System.Drawing.Point(105, 100);
            this.m_FirstPlayerNameTextBox.Name = "m_FirstPlayerNameTextBox";
            this.m_FirstPlayerNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.m_FirstPlayerNameTextBox.TabIndex = 6;
            this.m_FirstPlayerNameTextBox.TextChanged += new System.EventHandler(this.m_SecondPlayerNameTextBox_TextChanged);
            this.m_FirstPlayerNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.m_FirstPlayerNameTextBox_Validating);
            this.m_SecondPlayerNameTextBox.Enabled = false;
            this.m_SecondPlayerNameTextBox.Location = new System.Drawing.Point(105, 135);
            this.m_SecondPlayerNameTextBox.Name = "m_SecondPlayerNameTextBox";
            this.m_SecondPlayerNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.m_SecondPlayerNameTextBox.TabIndex = 5;
            this.m_SecondPlayerNameTextBox.Text = "[Computer]";
            this.m_SecondPlayerNameTextBox.TextChanged += new System.EventHandler(this.m_FirstPlayerNameTextBox_TextChanged);
            this.m_SecondPlayerNameTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.m_SecondPlayerNameTextBox_Validating);
            this.m_BoardSizeLabel.AutoSize = true;
            this.m_BoardSizeLabel.Location = new System.Drawing.Point(10, 20);
            this.m_BoardSizeLabel.Name = "m_BoardSizeLabel";
            this.m_BoardSizeLabel.Size = new System.Drawing.Size(61, 13);
            this.m_BoardSizeLabel.TabIndex = 7;
            this.m_BoardSizeLabel.Text = "Board Size:";
            this.m_Player1Label.AutoSize = true;
            this.m_Player1Label.Location = new System.Drawing.Point(25, 100);
            this.m_Player1Label.Name = "m_Player1Label";
            this.m_Player1Label.Size = new System.Drawing.Size(48, 13);
            this.m_Player1Label.TabIndex = 8;
            this.m_Player1Label.Text = "Player 1:";
            this.m_PlayersLabel.AutoSize = true;
            this.m_PlayersLabel.Location = new System.Drawing.Point(10, 70);
            this.m_PlayersLabel.Name = "m_PlayersLabel";
            this.m_PlayersLabel.Size = new System.Drawing.Size(44, 13);
            this.m_PlayersLabel.TabIndex = 9;
            this.m_PlayersLabel.Text = "Players:";
            this.m_NameErrorProvider.ContainerControl = this;
            this.m_ErrorProvider1.ContainerControl = this;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 225);
            this.Controls.Add(this.m_PlayersLabel);
            this.Controls.Add(this.m_Player1Label);
            this.Controls.Add(this.m_BoardSizeLabel);
            this.Controls.Add(this.m_SecondPlayerNameTextBox);
            this.Controls.Add(this.m_FirstPlayerNameTextBox);
            this.Controls.Add(this.m_RealRivalPlayerCheckBox);
            this.Controls.Add(this.m_10x10BoardSizeChooseButton);
            this.Controls.Add(this.m_8x8BoardSizeChooseButton);
            this.Controls.Add(this.m_6x6BoardSizeChooseButton);
            this.Controls.Add(this.m_DoneButton);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Othelo Game Settings";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)this.m_NameErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.m_ErrorProvider1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private bool getPlayer2Type()
        {
            bool IsPlayer2Computer;

            if (m_RealRivalPlayerCheckBox.Checked)
            {
                IsPlayer2Computer = false;
            }
            else
            {
                IsPlayer2Computer = true;
            }

            return IsPlayer2Computer;
        }

        private void checkValidName(string i_Name)
        {
            if (i_Name.Length > 20)
            {
                throw new Exception("name of player 1 must be less than 20 letters ");
            }

            for (int i = 0; i < m_FirstPlayerNameTextBox.Text.Length; i++)
            {
                if (i_Name[i] == ' ')
                {
                    throw new Exception("name of player 1 must be without spaces ");
                }
            }
        }

        private int getSizeBoard()
        {
            int boardSize = 0;

            if (m_6x6BoardSizeChooseButton.Checked == true)
            {
                boardSize = 6;
            }
            else if (m_8x8BoardSizeChooseButton.Checked == true)
            {
                boardSize = 8;
            }
            else
            {
                boardSize = 10;
            }

            return boardSize;
        }

        private void Form_Load(object sender, EventArgs e)
        {
        }

        private void m_FirstPlayerNameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void m_SecondPlayerNameTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void m_FirstPlayerNameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                checkValidName(m_FirstPlayerNameTextBox.Text);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                m_FirstPlayerNameTextBox.Focus();
                m_NameErrorProvider.SetError(m_FirstPlayerNameTextBox, ex.Message);
                MessageBox.Show(ex.Message, "Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            e.Cancel = false;
            m_NameErrorProvider.SetError(m_FirstPlayerNameTextBox, string.Empty);
        }

        private void m_SecondPlayerNameTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                checkValidName(m_SecondPlayerNameTextBox.Text);
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                m_SecondPlayerNameTextBox.Focus();
                m_NameErrorProvider.SetError(m_SecondPlayerNameTextBox, ex.Message);
                MessageBox.Show(ex.Message, "Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            e.Cancel = false;
            m_NameErrorProvider.SetError(m_SecondPlayerNameTextBox, string.Empty);
        }

        private void m_DoneButton_Click(object sender, EventArgs e)
        {
            string mainPlayerName = m_FirstPlayerNameTextBox.Text;
            string secondPlayerName = m_SecondPlayerNameTextBox.Text;
            bool IsPlayer2Computer = getPlayer2Type();
            int sizeBoard = getSizeBoard();
            OtheloGameManager OtheloGameLogic = new OtheloGameManager((byte)sizeBoard, mainPlayerName, secondPlayerName, IsPlayer2Computer);
            OtheloGameFormUI OtheloGame = new OtheloGameFormUI(OtheloGameLogic);
            this.Hide();
            this.Close();
            OtheloGame.StartGame();
        }

        private void m_RealRivalPlayerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (m_SecondPlayerNameTextBox.Enabled == false)
            {
                m_SecondPlayerNameTextBox.Enabled = true;
            }
            else
            {
                m_SecondPlayerNameTextBox.Text = "[Computer]";
                m_SecondPlayerNameTextBox.Enabled = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace minesweeperCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private static int rowNum = 10;
        private static int columNum = 10;
        private static int mineNum = 10;

        private int[] xDirection = { -1, +1, -1, -1, +1, +1, 0, 0 };
        private int[] yDirection = { 0, 0, -1, +1, -1, +1, -1, +1 };

        private Button[,] btnArray = new Button[rowNum, columNum];
        private bool[,] mineArray = new bool[rowNum, columNum];


        private void Form1_Load(object sender, EventArgs e)
        {
            // make 10*10 
            panelButton.Size = this.ClientSize;
            for (int i = 0; i <= rowNum - 1; i++)
            {
                for (int j = 0; (j <= (columNum - 1)); j++)
                {
                    btnArray[i, j] = new Button();
                    //  btnArray[i, j].Name = ("btn" & i.ToString & j.ToString);
                    btnArray[i, j].Size = new Size(35, 35);
                    btnArray[i, j].Location = new Point((10 + (j * 35)), (10 + (i * 35)));
                    btnArray[i, j].Tag = (i + ("," + j));
                    panelButton.Controls.Add(btnArray[i, j]);
                    btnArray[i, j].Click += new System.EventHandler(this.btnArray_Click);
                }
            }

            // set mine
            mineArray.Initialize();
            int idx = 0;

            while (idx < 100)
            {
                int rowIdx = this.genRandom(0, (rowNum - 1));
                int colIdx = this.genRandom(0, (columNum - 1));

                mineArray[rowIdx, colIdx] = true;
                int mineIdx = 0;
                for (int i = 0; (i <= (rowNum - 1)); i++)
                {
                    for (int j = 0; (j <= (columNum - 1)); j++)
                    {
                        if ((mineArray[i, j] == true))
                        {
                            mineIdx++;
                            if ((mineIdx == mineNum))
                            {
                                break;
                            }
                        }
                    }
                }

                if ((mineIdx == mineNum))
                {
                    break;
                }
            }
        }

        // event handler when push the btn
        public void btnArray_Click(object sender, System.EventArgs e)
        {
            Button clickBtn;
            int row;
            int colum;
            clickBtn = (Button)sender;
            row = int.Parse(clickBtn.Tag.ToString().Split(',')[0]);
            colum = int.Parse(clickBtn.Tag.ToString().Split(',')[1]);

            this.displayMine(row, colum);
        }

        public int genRandom(int min, int max)
        {
            Random generator = new System.Random(System.DateTime.Now.Millisecond);
            return generator.Next(min, max);
        }



    private void displayMine(int row, int colum)
    {
        int countMine = 0;
        if ((findMine(row, colum) == true))
        {
            this.btnArray[row, colum].Text = "M";
            btnArray[row, colum].Enabled = false;
            MessageBox.Show("BoooooooooooM");
        }
        else
        {
            for (int i = 0; (i <= 7); i++)
            {
                if (this.findMine((row + xDirection[i]), (colum + yDirection[i])))
                {
                    countMine++;
                }

            }

            if ((countMine == 0))
            {
                btnArray[row, colum].Text = "";
            }
            else
            {
                btnArray[row, colum].Text = countMine.ToString();
            }

            btnArray[row, colum].Enabled = false;
        }

    }

    private bool findMine(int row, int colum)
    {
        if (row < 0 || colum < 0 || row >= rowNum || colum >= columNum)
        {

            return false;
        }
        else
        {
            return this.mineArray[row, colum];
        }

    }

}
}




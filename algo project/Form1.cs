using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace algo_project
{
    public partial class Form1 : Form
    {

        List<int[,]> solution;
        int[,] intitialPazzle;
        Button[,] nums = new Button[3, 3];
        int moves, clks = 0;
        public Form1(List<int[,]> puzzles, int[,] inti, int moves)
        {
            solution = puzzles;
            intitialPazzle = new int[3, 3];
            
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                    intitialPazzle[j, k] = inti[j, k];
                    }
                  
                }
            this.moves = moves;
                InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            nums[0,0] = button1;
            nums[0,1] = button2;
            nums[0,2] = button3;

            nums[1, 0] = button4;
            nums[1, 1] = button5;
            nums[1, 2] = button6;

            nums[2, 0] = button7;
            nums[2, 1] = button8;
            nums[2, 2] = button9;

            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (intitialPazzle[j, k] == 0)
                    {
                        nums[j, k].Text = " ";
                        nums[j, k].FlatAppearance.BorderColor = Color.Gray;
                        nums[j, k].FlatStyle = FlatStyle.Flat;
                        nums[j, k].FlatAppearance.BorderSize = 0;
                    }
                    else
                    {
                        nums[j, k].FlatAppearance.BorderColor = Color.Black;
                        nums[j, k].FlatStyle = FlatStyle.Popup;
                        nums[j, k].Text = intitialPazzle[j, k].ToString();
                    }
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            showGuiAsync();
        }

        private async Task showGuiAsync()
        {
            solution.Reverse();
            foreach (int[,] i in solution)
            { 
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i[j, k] == 0)
                        {
                            nums[j, k].Text = " ";
                            nums[j, k].FlatAppearance.BorderColor = Color.Gray;
                            nums[j, k].FlatStyle = FlatStyle.Flat;
                            nums[j, k].FlatAppearance.BorderSize = 0;
                        }
                        else
                        {
                            nums[j, k].FlatAppearance.BorderColor = Color.Black;
                            nums[j, k].FlatStyle = FlatStyle.Popup;
                            
                            nums[j, k].Text = i[j, k].ToString();
                        }
                    }
                }
                await Task.Delay(500);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris_base;

namespace tetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox[,] screen = new PictureBox[20, 10];

        int[,] kruchek_left = new int[,] { { 0, 1, 1, 0 },
                                          { 0, 1, 0, 0 },
                                          { 0, 1, 0, 0 },
                                          { 0, 0, 0, 0 } };

        int[,] kruchek_right = new int[,]{{ 0, 1, 1, 0 },
                                          { 0, 0, 1, 0 },
                                          { 0, 0, 1, 0 },
                                          { 0, 0, 0, 0 } };

        int[,] square = new int[,] { { 0, 0, 0, 0 },
                                     { 0, 1, 1, 0 },
                                     { 0, 1, 1, 0 },
                                     { 0, 0, 0, 0 } };

        int[,] stick = new int[,] { { 0, 0, 0, 0, },
                                    { 1, 1, 1, 1 },
                                    { 0, 0, 0, 0 },
                                    { 0, 0, 0, 0 } };

        int[,] piramind = new int[,] { { 0, 0, 1, 0 },
                                       { 0, 1, 1, 1 },
                                       { 0, 0, 0, 0 },
                                       { 0, 0, 0, 0 } };

        int[,] zakaruchka_left = new int[,] { { 0, 0, 1, 0 },
                                              { 0, 1, 1, 0 },
                                              { 0, 1, 0, 0 },
                                              { 0, 0, 0, 0 } };

        int[,] zakaruchka_right = new int[,] { { 0, 1, 0, 0 },
                                              { 0, 1, 1, 0 },
                                              { 0, 0, 1, 0 },
                                              { 0, 0, 0, 0 } };

        int[,] active_fild = new int[,] { { 0, 0, 0, 0 },
                                            { 0, 0, 0, 0 },
                                            { 0, 0, 0, 0 },
                                            { 0, 0, 0, 0 } };
        int[,] active_fild_2 = new int[,] { { 0, 0, 0, 0 },
                                            { 0, 0, 0, 0 },
                                            { 0, 0, 0, 0 },
                                            { 0, 0, 0, 0 } };

        int x_screen_1 = 128;
        int y_screen_1 = 0;
        int[,] field = new int[25, 12];
        bool blockSpawnEnabled = true;
        int check_move = 0;
        int check_move_right = 0;
        int check_move_left = 0;
        int corde_x = 0;
        int corde_y = 4;
        btn btn1 = new btn();
        private void Form1_Load(object sender, EventArgs e)
        {
            btn1.Size = new Size(64, 64);
            btn1.Location = new Point(600, 320);
            Controls.Add(btn1);

            PictureBox next_picture = new PictureBox();
            next_picture.Size = new Size(128, 150);
            next_picture.Location = new Point(448, 0);
            next_picture.BackColor = Color.Black;
            Controls.Add(next_picture);
            PictureBox score_picture = new PictureBox();
            score_picture.Size = new Size(128, 128);
            score_picture.Location = new Point(448, 160);
            score_picture.BackColor = Color.Black;
            Controls.Add(score_picture);

            btn1.Name = "Start";
            btn1.Size = new Size(128, 128);
            btn1.Location = new Point(448, 298);
            btn1.Text = "Start";
            Controls.Add(btn1);
            btn1.Click += button1_Click;

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = 0;
                }
            }

            for (int j = 0; j < 11; j++)
            {
                field[24, j] = 3;
            }

            for (int i = 0; i < 25; i++)
            {
                field[i, 0] = 3;
                field[i, 11] = 3;
            }

            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    screen[i, j - 1] = new PictureBox();
                    screen[i, j - 1].Size = new Size(32, 32);
                    screen[i, j - 1].Location = new Point(x_screen_1, y_screen_1);
                    if (field[i + 5, j] == 0)
                    {
                        screen[i, j - 1].BackColor = Color.Aqua;
                    }
                    x_screen_1 += 32;
                    Controls.Add(screen[i, j - 1]);
                }
                y_screen_1 += 32;
                x_screen_1 = 128;
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {
            var current_Button = sender as Button;
            current_Button.Enabled = true;


            if (current_Button.Text == "Start")
            {
                timer1.Start();
                current_Button.Text = "Stop";
            }
            else
            {
                timer1.Stop();
                current_Button.Text = "Start";

            }
        }
        void myTimer_Tick(object sender, EventArgs e)
        {

            Random rnd = new Random();
            int value = rnd.Next(1, 7);

            Screen_update();

            if (blockSpawnEnabled)
            {
                Spawn_new_block(value);

                blockSpawnEnabled = false;
            }

            if (blockSpawnEnabled == false)
            {
                Check_block_all_move();
            }
            label2.Text = timer1.Interval.ToString(); // debag
            check_move = 0;

        }

        private void Move_block_right()
        {
            for (int i = 5; i < 25; i++)
            {
                for (int j = 11; j >= 1; j--)
                {
                    if (field[i, j] == 1)
                    {
                        field[i, j + 1] = field[i, j];
                        field[i, j] = 0;
                    }
                }
            }
            corde_y++;
        }

        private void Move_block_left()
        {
            for (int i = 5; i < 25; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (field[i, j] == 1)
                    {
                        field[i, j - 1] = field[i, j];
                        field[i, j] = 0;
                    }
                }
            }
            corde_y--;
        }


        private void Check_block_all_move()
        {
            bool move_down = true;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (field[i, j] == 1)
                    {
                        if (field[i + 1, j] >= 2)
                        {
                            move_down = false;
                            blockSpawnEnabled = true;

                        }

                    }
                }
            }
            if (move_down)
            {
                Block_down();
            }
            else
            {
                Stop_block();

            }
            for (int i = 5; i < 25; i++)
            {
                for (int j = 10; j >= 0; j--)
                {
                    if (field[i, j + 1] < 2 && field[i, j] == 1)
                    {
                        check_move_right++;
                    }
                }
            }
            for (int i = 5; i < 25; i++)
            {
                for (int j = 1; j <= 11; j++)
                {
                    if (field[i, j - 1] < 2 && field[i, j] == 1)
                    {
                        check_move_left++;
                    }
                }
            }

            if (check_move_left == 4 && check_move == -1)
            {
                Move_block_left();

            }
            else if (check_move_right == 4 && check_move == 1)
            {
                Move_block_right();

            }
            else if (check_move == 2)
            {
                timer1.Interval = 100;

            }
            else if (check_move == 3)
            {
                povorot_block();
            }
            check_move_left = 0;
            check_move_right = 0;
        }

        private void povorot_block()
        {
            Import_block();
            Turn_block();
            replace_block();
        }

        private void replace_block()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    field[corde_x + i, corde_y + j] = active_fild_2[i, j];
                }
            }
        }

        private void Turn_block()
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int buf = active_fild[i, j];
                    active_fild[i, j] = active_fild[j, i];
                    active_fild[j, i] = buf;
                }
            }
            int i__ = 3;
            int j__ = 3;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    active_fild_2[i, j] = active_fild[i__, j__];
                    j__--;
                }
                j__ = 3;
                i__--;
            }

        }

        private void Import_block()
        {
            int act_i = 0;
            int act_j = 0;
            for (int i = corde_x; i < corde_x + 4; i++)
            {
                for (int j = corde_y; j < corde_y + 4; j++)
                {
                    active_fild[act_i, act_j] = field[i, j];
                    act_j++;
                }
                act_j = 0;
                act_i++;
            }

        }

        private void Stop_block()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (field[i, j] == 1)
                    {
                        field[i, j] = 2;
                    }
                }
            }
        }

        private void Block_down()
        {
            for (int i = 24; i >= 0; i--)
            {
                for (int j = 11; j >= 1; j--)
                {
                    if (field[i, j] == 1)
                    {
                        field[i + 1, j] = field[i, j];
                        field[i, j] = 0;
                    }
                }
            }
            corde_x++;

        }

        private void Spawn_new_block(int value)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (value)
                    {
                        case 1:
                            field[i, j + 3] = kruchek_left[i, j];

                            break;
                        case 2:
                            field[i, j + 3] = kruchek_right[i, j];


                            break;
                        case 3:
                            field[i, j + 3] = square[i, j];

                            break;
                        case 4:
                            field[i, j + 3] = stick[i, j];


                            break;
                        case 5:
                            field[i, j + 3] = piramind[i, j];


                            break;
                        case 6:
                            field[i, j + 3] = zakaruchka_left[i, j];

                            break;
                        case 7:
                            field[i, j + 3] = zakaruchka_right[i, j];

                            break;

                    }
                }

            }
            corde_x = 0;
            corde_y = 4;
        }
        private void Screen_update()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (field[i + 5, j] == 0)
                    {
                        screen[i, j - 1].BackColor = Color.Aqua;
                    }
                    else if (field[i + 5, j] == 1)
                    {
                        screen[i, j - 1].BackColor = Color.Red;
                    }
                    else if (field[i + 5, j] == 2)
                    {
                        screen[i, j - 1].BackColor = Color.Black;
                    }
                    else if (field[i + 5, j] == 3)
                    {
                        screen[i, j - 1].BackColor = Color.White;
                    }
                }

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:

                    check_move = -1;
                    break;
                case Keys.D:
                    check_move = 1;
                    break;
                case Keys.S:
                    check_move = 2;
                    break;
                case Keys.W:
                    check_move = 3;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                timer1.Interval = 500;
            }
        }
    }
}

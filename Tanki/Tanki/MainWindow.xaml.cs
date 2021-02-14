using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tanki
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ObservableCollection<Cell>> Pole;
        static int sizeCol = 10;
        static int sizeRow = 10;

        static int tank_1_col = 0;
        static int tank_1_row = 9;
        static int[] tank_1_pos = {-1,0 };

        static int tank_2_col = 9;
        static int tank_2_row = 0;
        static int[] tank_2_pos = {1,0 };


        Tank tank1 = new Tank { Row = tank_1_row, Col = tank_1_col };
        Tank tank2 = new Tank { Row = tank_2_row, Col = tank_2_col };

        static int[] block_col = {1,3,3,7,1,3,5,3,5,6,8,9,1,2,2,4,5,6,7,8,8,1,5,6,0 };
        static int[] block_row = {0,0,1,1,2,2,2,3,4,4,4,4,5,5,6,6,6,6,6,6,7,8,8,8,0 };

        public MainWindow()
        {
            InitializeComponent();
            tank1.Color = "DarkRed";
            tank2.Color = "DarkGreen";
            GenPole();
            leftBorder.DataContext = tank1;
            rightBorder.DataContext = tank2;
        }
        private void Window_KeyDown(Object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    if (tank1.Row - 1 >= 0)
                    {
                        if (Pole[tank1.Row - 1][tank1.Col] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank1.Row - 1][tank1.Col] is Wall)
                        {
                            break;
                        }
                        else if(Pole[tank1.Row - 1][tank1.Col] is Cell)
                        {
                            tank1.Row -= 1;
                            Pole[tank1.Row + 1][tank1.Col] = new Cell { Row = tank1.Row + 1, Col = tank1.Col };
                            Pole[tank1.Row][tank1.Col] = tank1;
                            

                        }
                         
                    }
                    tank1.File = "2_1.png";
                    tank_1_pos[0] = -1;
                    tank_1_pos[1] = 0;
                    break;
                case Key.S:
                    if (tank1.Row + 1 < sizeRow) {
                        if (Pole[tank1.Row + 1][tank1.Col] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank1.Row + 1][tank1.Col] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank1.Row + 1][tank1.Col] is Cell)
                        {
                            tank1.Row += 1;
                            Pole[tank1.Row - 1][tank1.Col] = new Cell { Row = tank1.Row - 1, Col = tank1.Col };
                            Pole[tank1.Row][tank1.Col] = tank1;
                        }
                    }
                    tank1.File = "2_3.png";
                    tank_1_pos[0] = 1;
                    tank_1_pos[1] = 0;
                    break;
                case Key.A:
                    if (tank1.Col - 1 >= 0)
                    {
                        if (Pole[tank1.Row][tank1.Col - 1] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank1.Row][tank1.Col - 1] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank1.Row][tank1.Col - 1] is Cell)
                        {
                            tank1.Col -= 1;
                            Pole[tank1.Row][tank1.Col + 1] = new Cell { Row = tank1.Row, Col = tank1.Col + 1 };
                            Pole[tank1.Row][tank1.Col] = tank1;
                        }
                         
                    }
                    tank1.File = "2_4.png";
                    tank_1_pos[0] = 0;
                    tank_1_pos[1] = -1;
                    break;
                case Key.D:
                    if (tank1.Col + 1 < sizeCol)
                    {
                        if (Pole[tank1.Row][tank1.Col + 1] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank1.Row][tank1.Col + 1] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank1.Row][tank1.Col + 1] is Cell)
                        {
                            tank1.Col += 1;
                            Pole[tank1.Row][tank1.Col - 1] = new Cell { Row = tank1.Row, Col = tank1.Col - 1 };
                            Pole[tank1.Row][tank1.Col] = tank1;
                        }
                        
                    }
                    tank1.File = "2_2.png";
                    tank_1_pos[0] = 0;
                    tank_1_pos[1] = 1;
                    break;
                case Key.Space:
                    if (tank1.File == "2_1.png")
                    {
                        tank1.File = "2_1_f.png";
                    }
                   else if (tank1.File == "2_2.png")
                    {
                        tank1.File = "2_2_f.png";
                    }
                   else if (tank1.File == "2_3.png")
                    {
                        tank1.File = "2_3_f.png";
                    }
                    else if (tank1.File == "2_4.png")
                    {
                        tank1.File = "2_4_f.png";
                    }
                    TankFire(tank1, tank_1_pos);
                    break;
                //-------------------------------
                case Key.Up:
                    if (tank2.Row - 1 >= 0)
                    {
                        if (Pole[tank2.Row - 1][tank2.Col] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank2.Row - 1][tank2.Col] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank2.Row - 1][tank2.Col] is Cell)
                        {
                            tank2.Row -= 1;
                            Pole[tank2.Row + 1][tank2.Col] = new Cell { Row = tank2.Row + 1, Col = tank2.Col };
                            Pole[tank2.Row][tank2.Col] = tank2;
                        }
                        
                    }
                    tank2.File = "1_1.png";
                    tank_2_pos[0] = -1;
                    tank_2_pos[1] = 0;
                    break;
                case Key.Down:
                    if (tank2.Row + 1 < sizeRow)
                    {
                        if (Pole[tank2.Row + 1][tank2.Col] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank2.Row + 1][tank2.Col] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank2.Row + 1][tank2.Col] is Cell)
                        {
                            tank2.Row += 1;
                            Pole[tank2.Row - 1][tank2.Col] = new Cell { Row = tank2.Row - 1, Col = tank2.Col };
                            Pole[tank2.Row][tank2.Col] = tank2;
                        }
                    }
                    tank2.File = "1_3.png";
                    tank_2_pos[0] = 1;
                    tank_2_pos[1] = 0;
                    break;
                case Key.Left:
                    if (tank2.Col - 1 >= 0)
                    {
                        if (Pole[tank2.Row][tank2.Col - 1] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank2.Row][tank2.Col - 1] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank2.Row][tank2.Col - 1] is Cell)
                        {
                            tank2.Col -= 1;
                            Pole[tank2.Row][tank2.Col + 1] = new Cell { Row = tank2.Row, Col = tank2.Col + 1 };
                            Pole[tank2.Row][tank2.Col] = tank2;
                        }
                         
                    }
                    tank2.File = "1_4.png";
                    tank_2_pos[0] = 0;
                    tank_2_pos[1] = -1;
                    break;
                case Key.Right:
                    if (tank2.Col + 1 < sizeCol)
                    {
                        if (Pole[tank2.Row][tank2.Col + 1] is Tank)
                        {
                            Destr_All(tank1, tank2);
                        }
                        else if (Pole[tank2.Row][tank2.Col + 1] is Wall)
                        {
                            break;
                        }
                        else if (Pole[tank2.Row][tank2.Col + 1] is Cell)
                        {
                            tank2.Col += 1;
                            Pole[tank2.Row][tank2.Col - 1] = new Cell { Row = tank2.Row, Col = tank2.Col - 1 };
                            Pole[tank2.Row][tank2.Col] = tank2;
                        }
                        
                    }
                    tank2.File = "1_2.png";
                    tank_2_pos[0] = 0;
                    tank_2_pos[1] = 1;
                    break;
                case Key.RightShift:
                    if (tank2.File == "1_1.png")
                    {
                        tank2.File = "1_1_f.png";
                    }
                    else if (tank2.File == "1_2.png")
                    {
                        tank2.File = "1_2_f.png";
                    }
                    else if (tank2.File == "1_3.png")
                    {
                        tank2.File = "1_3_f.png";
                    }
                    else if (tank2.File == "1_4.png")
                    {
                        tank2.File = "1_4_f.png";
                    }
                    TankFire(tank2, tank_2_pos);
                    break;
            }
        }

        void Win()
        {
            if (tank2.Live == 0 && tank1.Live == 0)
            {
                MessageBox.Show("Draw! New game?");
                GenPole();
                
            }
            else if (tank2.Live == 0)
            {
                MessageBox.Show("Red win! New game?");
                GenPole();
            }
            else if (tank1.Live == 0)
            {
                MessageBox.Show("Green win! New game?");
                GenPole();
            }
        }
        void Destr_All(Tank tank1, Tank tank2)
        {
            Pole[tank1.Row][tank1.Col] = new Cell { Row = tank1.Row, Col = tank1.Col };
            Pole[tank2.Row][tank2.Col] = new Cell { Row = tank2.Row, Col = tank2.Col };
            tank1.Row = tank_1_row;
            tank1.Col = tank_1_col;
            tank2.Row = tank_2_row;
            tank2.Col = tank_2_col;
            tank1.Live = tank1.Live - 1;
            tank2.Live = tank2.Live - 1;
            Pole[tank_1_row][tank_1_col] = tank1;
            Pole[tank_2_row][tank_2_col] = tank2;
            Win();
        }

        void GenPole()
        {
            Pole = new ObservableCollection<ObservableCollection<Cell>>();
            int num = 0;
            for (int i = 0; i < sizeRow; i++)
            {
                Pole.Add(new ObservableCollection<Cell>());
                for (int j = 0; j < sizeCol; j++)
                {
                    if (i == block_row[num] && j == block_col[num]) { Pole[i].Add(new Wall { Row = i, Col = j }); num += 1;}
                    else { Pole[i].Add(new Cell { Row = i, Col = j }); }
                }
            }
            tank1.File = "2_1.png"; tank2.File = "1_3.png";
            tank1.Live = 3; tank2.Live = 3;
            Pole[tank1.Row][tank1.Col] = new Cell { Row = tank1.Row, Col = tank1.Col };
            Pole[tank2.Row][tank2.Col] = new Cell { Row = tank2.Row, Col = tank2.Col };
            tank1.Row = tank_1_row; tank1.Col = tank_1_col;
            tank2.Row = tank_2_row; tank2.Col = tank_2_col;
            Pole[tank_1_row][tank_1_col] = tank1; Pole[tank_2_row][tank_2_col] = tank2;
            ic.ItemsSource = Pole;
            
        }
        void TankFire(Tank tank, int[] tank_pos)
        {
            if (tank.Row + tank_pos[0] >= 0 && tank.Row + tank_pos[0] < sizeRow && tank.Col + tank_pos[1] >= 0 && tank.Col + tank_pos[1] < sizeCol)
            {
                if (Pole[tank.Row + tank_pos[0]][tank.Col + tank_pos[1]] is Wall)
                {
                    return;
                }
                else if ((Pole[tank.Row + tank_pos[0]][tank.Col + tank_pos[1]] == tank2))
                {
                    tank2.Row = tank_2_row;
                    tank2.Col = tank_2_col;
                    tank2.Live = tank2.Live - 1;
                    Pole[tank.Row + tank_pos[0]][tank.Col + tank_pos[1]] = new Cell { Row = tank.Row + tank_pos[0], Col = tank.Col + tank_pos[1] };
                    Pole[tank_2_row][tank_2_col] = tank2;
                    Win();
                }
                else if ((Pole[tank.Row + tank_pos[0]][tank.Col + tank_pos[1]] == tank1))
                {
                    tank1.Row = tank_1_row;
                    tank1.Col = tank_1_col;
                    tank1.Live = tank1.Live - 1;
                    Pole[tank.Row + tank_pos[0]][tank.Col + tank_pos[1]] = new Cell { Row = tank.Row + tank_pos[0], Col = tank.Col + tank_pos[1] };
                    Pole[tank_1_row][tank_1_col] = tank1;
                    Win();
                }
                else
                {
                    Bullet bullet = new Bullet { Row = tank.Row + tank_pos[0], Col = tank.Col + tank_pos[1] };
                    Pole[tank.Row + tank_pos[0]][tank.Col + tank_pos[1]] = bullet;
                    BulletFly(bullet, tank_pos, 0);

                }
            }
        }

        void BulletFly(Cell bullet, int[] tank_pos, int i)
        {
            if (bullet.Row + tank_pos[0] >= 0 && bullet.Row + tank_pos[0] < sizeRow && bullet.Col + tank_pos[1] >= 0 && bullet.Col + tank_pos[1] < sizeCol)
            {
                if (Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]] is Wall)
                {
                    Pole[bullet.Row][bullet.Col] = new Cell { Row = bullet.Row, Col = bullet.Col };
                }
                else if (Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]] == tank2)
                {
                    tank2.Row = tank_2_row;
                    tank2.Col = tank_2_col;
                    tank2.Live = tank2.Live - 1;
                    Pole[tank_2_row][tank_2_col] = tank2;
                    Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]] = new Cell { Row = bullet.Row + tank_pos[0], Col = bullet.Col + tank_pos[1] };
                    Pole[bullet.Row][bullet.Col] = new Cell { Row = bullet.Row, Col = bullet.Col };
                    Win();
                }
                else if (Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]] == tank1)
                {
                    tank1.Row = tank_1_row;
                    tank1.Col = tank_1_col;
                    tank1.Live = tank1.Live - 1;
                    Pole[tank_1_row][tank_1_col] = tank1;
                    Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]] = new Cell { Row = bullet.Row + tank_pos[0], Col = bullet.Col + tank_pos[1] };
                    Pole[bullet.Row][bullet.Col] = new Cell { Row = bullet.Row, Col = bullet.Col };
                    Win();
                }
                else
                {
                    Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]] = new Bullet { Row = bullet.Row + tank_pos[0], Col = bullet.Col + tank_pos[1] };
                    Pole[bullet.Row][bullet.Col] = new Cell { Row = bullet.Row, Col = bullet.Col };
                    BulletFly(Pole[bullet.Row + tank_pos[0]][bullet.Col + tank_pos[1]], tank_pos, 0);

                }
            }
            else
            {
                Pole[bullet.Row][bullet.Col] = new Cell { Row = bullet.Row, Col = bullet.Col };
            }
            
        }

    }

    

    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Row, Col;
        string file = "1.jpg";
        public string File { get => file; set { file = value; Fire("File"); } }
        protected void Fire(params string[] props)
        {
            if (PropertyChanged != null)
                foreach (var prop in props)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    class Tank : Cell
    {
        int live = 3;
        string color = "White";
        string file = "1_1.png";
        public string File { get => file; set { file = value; Fire("File"); }}
        public int Live { get => live; set { live = value; Fire("Live"); } }
        public string Color { get => color; set { color = value; Fire("Color"); } }
    }
    class Wall : Cell
    {
        string color = "Gray";
        string file = "2.jpg";
        public string File { get => file; set { file = value; Fire("File"); } }
        public string Color { get => color; set { color = value; Fire("Color"); } }
    }
    class Bullet : Cell
    {
        string color = "White";
        public string Color { get => color; set { color = value; Fire("Color"); } }
    }
}

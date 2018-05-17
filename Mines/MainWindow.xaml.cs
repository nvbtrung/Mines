using Mines.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mines
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Member
        int row, col;
        int nbBomb;
        bool endGame;
        Cell[,] nResult;
        List<Button> cellEmpty = new List<Button>();

        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            Init();
        }
        #endregion

        /// <summary>
        /// Creer nouveau instance de jeu
        /// </summary>
        private void Init()
        {
            //Initialiser les configurations du jeu
            row = col = 8;
            nbBomb = 10;
            nResult = new Cell[row, col];

            //Initialiser l'interface
            Container.Background = Brushes.White;
            Container.ShowGridLines = true;

            //Ajouter des colognes
            for (var i = 0; i < col; i++)
            {
                var colDef = new ColumnDefinition();
                colDef.Width = new GridLength(1, GridUnitType.Star);
                Container.ColumnDefinitions.Add(colDef);
            }

            //Ajouter des lignes            
            for (var i = 0; i < row; i++)
            {
                var rowDef = new RowDefinition();
                rowDef.Height = new GridLength(1, GridUnitType.Star);
                Container.RowDefinitions.Add(rowDef);
            }
            NewGame();
        }

        #region Event handle
        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var colButton = Grid.GetColumn(button);
            var rowButton = Grid.GetRow(button);

            //Event click on bomb
            if (nResult[rowButton, colButton] == Cell.Bomb)
            {
                for (var i = 0; i < row; i++)
                {
                    for (var j = 0; j < col; j++)
                    {
                        if (nResult[i, j] == Cell.Bomb)
                        {
                            var temp = Container.Children.Cast<Button>().First(cell => Grid.GetRow(cell) == i && Grid.GetColumn(cell) == j);
                            temp.Background = Brushes.Orange;
                            temp.IsHitTestVisible = false;
                        }
                        else
                        {
                            Container.Children.Cast<Button>().First(cell => Grid.GetRow(cell) == i && Grid.GetColumn(cell) == j).IsHitTestVisible = false;
                        }
                    }
                }
                endGame = true;
            }

            //Event click on empty
            if (nResult[rowButton, colButton] == Cell.Empty)
            {                
                if (nbBombAround(rowButton, colButton) != 0)
                {
                    button.Content = nbBombAround(rowButton, colButton);
                }
                else
                {
                    button.Content = null;
                    button.Background = Brushes.Gray;
                    button.IsHitTestVisible = false;
                    cellEmpty.Add(button);
                    List<Tuple<int,int>> cells = getCellAround(rowButton, colButton);
                    cells.ForEach(cell =>
                    {
                        var temp = Container.Children.Cast<Button>().First(c => Grid.GetRow(c) == cell.Item1 && Grid.GetColumn(c) == cell.Item2);
                        if (!cellEmpty.Contains(temp))
                        {
                            var even = new RoutedEventArgs();
                            cellEmpty.Add(temp);
                            Cell_Click(temp, even);
                        }                        
                    });
                }
            }

            //Verifier si le jeu est fini
            checkWin();
        }

        private void Cell_RightClick(object sender, RoutedEventArgs e)
        {            
            var button = (Button)sender;
            var colButton = Grid.GetColumn(button);
            var rowButton = Grid.GetRow(button);
            if(nResult[rowButton, colButton] == Cell.Flag)
            {
                button.Content = null;
                nResult[rowButton, colButton] = Cell.Empty;
            }
            else if (nResult[rowButton, colButton] == Cell.FlagBomb)
            {
                button.Content = null;
                nResult[rowButton, colButton] = Cell.Bomb;
            }
            else
            {
                if (nResult[rowButton, colButton] == Cell.Empty)
                {
                    nResult[rowButton, colButton] = Cell.Flag;
                }
                if (nResult[rowButton, colButton] == Cell.Bomb)
                {
                    nResult[rowButton, colButton] = Cell.FlagBomb;
                }
                button.Content = "Flag";
            }
            checkWin();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        #endregion

        #region Function divers
        private void NewGame()
        {
            Container.Children.Clear();

            endGame = false;

            //Convert cell to Button
            for (var i = 0; i < col; i++)
            {
                for (var j = 0; j < row; j++)
                {
                    Button button = new Button();
                    button.Background = Brushes.White;
                    button.Click += Cell_Click;
                    button.MouseRightButtonDown += Cell_RightClick;
                    Container.Children.Add(button);
                    Grid.SetColumn(button, i);
                    Grid.SetRow(button, j);
                }
            }

            //Attribuer des cell à Empty
            for (var i = 0; i < row; i++)
            {
                for (var j = 0; j < col; j++)
                {
                    nResult[i, j] = Cell.Empty;
                }
            }
            //Place bomb dans cell
            Random rand = new Random();
            for (var i = 0; i < nbBomb; i++)
            {
                int colBomb, rowBomb;
                do
                {
                    colBomb = rand.Next(0, col);
                    rowBomb = rand.Next(0, row);
                }
                while (nResult[rowBomb, colBomb] == Cell.Bomb);
                nResult[rowBomb, colBomb] = Cell.Bomb;
            }
        }

        private List<Tuple<int,int>> getCellAround(int rowCell, int colCell)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            //corner left top
            if (rowCell == 0 && colCell == 0)
            {
                result.Add(new Tuple<int, int>(rowCell, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell + 1));
            }
            //corner right top
            if (rowCell == 0 && colCell == (col - 1))
            {
                result.Add(new Tuple<int, int>(rowCell, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell - 1));
            }
            //corner left bottom
            if (rowCell == (row - 1) && colCell == 0)
            {
                result.Add(new Tuple<int, int>(rowCell, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell + 1));
            }
            //corner right bottom
            if (rowCell == (row - 1) && colCell == (col - 1))
            {
                result.Add(new Tuple<int, int>(rowCell, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell - 1));
            }
            //top
            if (rowCell == 0 && colCell > 0 && colCell < (col - 1))
            {
                result.Add(new Tuple<int, int>(rowCell, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell, colCell + 1));
            }
            //left
            if (colCell == 0 && rowCell > 0 && rowCell < (row - 1))
            {
                result.Add(new Tuple<int, int>(rowCell - 1, colCell));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell));
            }
            //bottom
            if (rowCell == (row - 1) && colCell > 0 && colCell < (col - 1))
            {
                result.Add(new Tuple<int, int>(rowCell, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell, colCell + 1));
            }
            //right
            if (colCell == (col - 1) && rowCell > 0 && rowCell < (row - 1))
            {
                result.Add(new Tuple<int, int>(rowCell - 1, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell));
                result.Add(new Tuple<int, int>(rowCell, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell));
            }
            //normal
            if (rowCell > 0 && rowCell < (row - 1) && colCell > 0 && colCell < (col - 1))
            {
                result.Add(new Tuple<int, int>(rowCell - 1, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell));
                result.Add(new Tuple<int, int>(rowCell - 1, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell, colCell + 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell - 1));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell));
                result.Add(new Tuple<int, int>(rowCell + 1, colCell + 1));

            }
            return result;
        }

        private int nbBombAround(int rowCell, int colCell)
        {
            int result = 0;
            List<Tuple<int,int>> cells = getCellAround(rowCell, colCell);
            cells.ForEach(cell =>
            {
                if (nResult[cell.Item1, cell.Item2] == Cell.Bomb || nResult[cell.Item1, cell.Item2] == Cell.FlagBomb) result++;
            });

            return result;
        }

        private void checkWin()
        {
            if (!endGame)
            {
                if(!Container.Children.Cast<Button>().ToList().Any(button => button.Content == null && button.Background == Brushes.White))
                {
                    endGame = true;
                    MessageBox.Show("You win");
                }
            }
            else
            {
                MessageBox.Show("You win");
            }
        }
        #endregion
    }
}

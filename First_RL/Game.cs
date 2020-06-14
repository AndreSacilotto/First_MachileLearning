using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_RL
{
    class Game
    {
        int[,] game_matriz;
        int[,] rewards;
        int border;

        int pos_x, pos_y;

        int totalReward;
        const int rewards_to_win = 1;

        public Game(int grid_size) => ReSet(grid_size);

        public void ReSet(int grid_size)
        {
            totalReward = 0;
            border = grid_size;
            game_matriz = new int[grid_size, grid_size];
            rewards = new int[grid_size, grid_size];
            //criar tabuleiro do jogo
            int arrayBorder = border - 1;

            pos_x = pos_y = 0;

            game_matriz[pos_y, pos_x] = 1;

            rewards[arrayBorder, arrayBorder] = 1;
        }

        public int GameReset()
        {
            ReSet(border);
            return Matematica.Array2D_GetFlatIndex(game_matriz, pos_y, pos_x);
        }

        public (int new_state, int reward, bool done) Step(int action)
        {
            MoveByIndex(action);
            int new_state = Matematica.Array2D_GetFlatIndex(game_matriz, pos_y, pos_x);

            var reward = rewards[pos_x, pos_y];
            totalReward += reward;

            var done = false;
            if (rewards_to_win <= totalReward)
                done = true;

            return (new_state, reward, done);
        }

        void Move_X(int x)
        {
            if (!((pos_x + 1 < border && x == 1) || (pos_x - 1 >= 0 && x == -1)))
                return;
            game_matriz[pos_y, pos_x] = 0;
            pos_x += x;
            game_matriz[pos_y, pos_x] = 1;            
        }
        void Move_Y(int y)
        {
            if (!((pos_y + 1 < border && y == 1) || (pos_y - 1 >= 0 && y == -1)))
                return;
            game_matriz[pos_y, pos_x] = 0;
            pos_y += y;
            game_matriz[pos_y, pos_x] = 1;
        }
        public void MoveUp() => Move_Y(-1);
        public void MoveDown() => Move_Y(1);
        public void MoveLeft() => Move_X(-1);
        public void MoveRight() => Move_X(1);

        public void MoveByIndex(int act)
        {
            switch (act)
            {
                case 0: MoveUp(); break;
                case 1: MoveDown(); break;
                case 2: MoveRight(); break;
                case 3: MoveLeft(); break;
            }
        }

        public void Show_Array<T> (T[,] array)
        {
            for (int i = 0; i < border; i++)
            {
                for (int e = 0; e < border; e++)
                    Console.Write(array[i,e].ToString() + ' ');
                Console.WriteLine();
            }
        }
        public void Show_Game() => Show_Array(game_matriz);
        public void Show_Rewards() => Show_Array(rewards);

    }
}

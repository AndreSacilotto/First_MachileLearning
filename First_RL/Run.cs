using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_RL
{
    class Run
    {
        public void Start()
        {
            Main();
        }

        public void Main()
        {
            const int gameSize = 3;
            var game = new Game(gameSize);
            var q_table = Matematica.ArrayJagged_Ini<double>(gameSize*gameSize, 4);

            int num_episodes = 10000;
            int max_steps_per_episode = 100;

            double learning_rate = 0.1;
            double discount_rate = 0.99;

            double exploration_rate = 1;
            double max_exploration_rate = 1;
            double min_exploration_rate = 0.01;
            double exploration_decay_rate = 0.01;

            var rewards_all_episodes = new List<int>();

            //inside vars
            for (int i = 0; i < num_episodes; i++)
            {
                var state = game.GameReset();
                int rewards_current_episode = 0;

                for (int e = 0; e < max_steps_per_episode; e++)
                {
                    // Exploration-exploitation trade-off
                    double exploration_rate_threshold = Matematica.rng.NextDouble();

                    int action = Matematica.ArgMax(q_table[state]);

                    if (exploration_rate_threshold <= exploration_rate)
                        action = Explore_action(action);

                    //(new_state, reward, done, info) = env.step(action);
                    var (new_state, reward, done) = game.Step(action);
                    
                    q_table[state][action] = Matematica.Q_Table(q_table[state][action], q_table[new_state], learning_rate, reward, discount_rate);

                    state = new_state;
                    rewards_current_episode += reward;

                    if (done) break;
                }

                exploration_rate = Matematica.Exploration(min_exploration_rate, max_exploration_rate, exploration_decay_rate, i);

                rewards_all_episodes.Add(rewards_current_episode);
            }

            Average_Reward_Print(rewards_all_episodes, num_episodes);
            Console.WriteLine(q_table[2][1]);
        }

        int Explore_action(int act)
        {
            int newAct;
            do newAct = Matematica.rng.Next(4);
            while (act == newAct);
            return newAct;
        }

        void Average_Reward_Print(List<int> rewards_all_episodes, int num_episodes)
        {
            for (int i = 1000, e = 0; i <= num_episodes;i+=1000)
            {
                double sum = 0;
                for (; e < i; e++) sum += rewards_all_episodes[e];
                Console.WriteLine(i + ": " + sum / 1000);
            }
        }

        void HumamTestes()
        {
            var game = new Game(5);
            while (true)
            {
                game.Show_Game();
                HumanMoves(Console.ReadLine(), game);
            }
        }
        void HumanMoves(string str, Game game)
        {                
            if (str != string.Empty)
            switch (str[0])
            {
                case 'W':
                    game.MoveUp();
                break;

                case 'S':
                    game.MoveDown();
                break;

                case 'A':
                    game.MoveLeft();
                break;

                case 'D':
                    game.MoveRight();
                break;

                case 'R':
                    game.Show_Rewards();
                break;

                case 'G':
                    game.Show_Game();
                break;
            }
        }

    }
}

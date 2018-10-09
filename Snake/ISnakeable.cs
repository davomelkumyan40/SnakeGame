using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    interface ISnakeable
    {
        void Start();
        void Setup();
        void Drawing();
        void Input();
        void KeyCheck(char key);
        void Logic();
        bool Win();
        void Gameover();
        void Victory();
    }
}

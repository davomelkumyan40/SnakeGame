using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakelib
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
        void EndGame();
    }
}

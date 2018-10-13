using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    interface ISnake
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnake {

    struct PlayerControls {
        public ConsoleKey moveDown, moveUp, moveLeft, moveRight, pause;

        public PlayerControls(ConsoleKey moveDown, ConsoleKey moveUp, ConsoleKey moveLeft, ConsoleKey moveRight, ConsoleKey pause) {
            this.moveUp = moveUp;
            this.moveDown = moveDown;
            this.moveLeft = moveLeft;
            this.moveRight = moveRight;
            this.pause = pause;
        }
    }

    public enum Movement : int {
        DOWN, UP, LEFT, RIGHT
    }

    class Program {
        static void Main(string[] args) {
            Game game = new Game();
            game.start();
            Console.ReadLine();
            while(Console.KeyAvailable) {
            }
            //TODO implement new start
        }
    }
}
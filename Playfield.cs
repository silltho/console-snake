using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnake {
    class Playfield {
        public static int width = 20;
        public static int height = 20;
        public Position middlePosition = new Position(width / 2 + 1, height / 2 + 1);
        public Position firstPosition = new Position(width / 2 + 1, height / 3 + 1);
        public Position secondPosition = new Position(width / 2 + 1, 2 * height / 3 + 1);
        static char borderChar = '#';
        static char liningChar = '*';
        public Position lining = new Position(-1, -1);

        public void draw() {
            for(int i = 0; i < width + 2; i++) {
                Console.SetCursorPosition(i, 0);
                Console.Write(borderChar);
                Console.SetCursorPosition(i, height + 2);
                Console.Write(borderChar);
            }

            for(int i = 0; i < height + 2; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write(borderChar);
                Console.SetCursorPosition(width + 1, i);
                Console.Write(borderChar);
            }
        }

        public bool checkBorderError(int x, int y) {
            if(x == 0 || x == width + 1 || y == 0 || y == height + 2) {
                return true;
            }
            return false;
        }

        public void generateLining(LinkedList<Position> positionBlacklist) {
            Position randomPosition = generateNewRadomPosition();
            while(!checkLiningPosition(randomPosition, positionBlacklist)) {
                randomPosition = generateNewRadomPosition();
            }
            lining.x = randomPosition.x;
            lining.y = randomPosition.y;

            Console.SetCursorPosition(lining.x, lining.y);
            Console.Write(liningChar);
        }

        public bool eatLining(Position nextPosition) {
            if(nextPosition.x == this.lining.x && nextPosition.y == this.lining.y) {
                this.lining.x = -1;
                this.lining.y = -1;
                return true;
            }
            return false;
        }

        Position generateNewRadomPosition() {
            Random rnd = new Random();
            int randomX = rnd.Next(1, width + 1);
            int randomY = rnd.Next(1, height + 1);
            return new Position(randomX, randomY);
        }

        bool checkLiningPosition(Position liningPosition, LinkedList<Position> positionBlacklist) {
            foreach(Position pos in positionBlacklist) {
                if(pos.x == liningPosition.x && pos.y == liningPosition.y) {
                    return false;
                }
            }
            return true;
        }

        public void writeStartGameText() {
            Console.SetCursorPosition(width + 4, height);
            Console.Write("Enter zum starten!");
        }

        public void clearStartGameText() {
            Console.SetCursorPosition(width + 4, height);
            Console.Write("                  ");
        }

        public Position getPlayfieldSize() {
            return new Position(width,height);
        }

        public void writeGameOverText(string gameOverPlayer) {
            Console.SetCursorPosition(width + 3, height);
            Console.Write(gameOverPlayer + " hat verloren!       ");
        }
    }
}

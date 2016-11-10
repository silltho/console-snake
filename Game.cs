using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnake {
    class Game {
        Playfield playfield = new Playfield();
        Player[] players = new Player[2];
        PlayerControls player1Controls = new PlayerControls(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Spacebar);
        PlayerControls player2Controls = new PlayerControls(ConsoleKey.S, ConsoleKey.W, ConsoleKey.A, ConsoleKey.D, ConsoleKey.Spacebar);

        int difficulty = 0;

        public void initGame() {
            Console.CursorVisible = false;
            Console.Clear();
            generatePlayers();
            playfield.generateLining(getAllSnakesPositions());
            playfield.draw();
        }

        public void generatePlayers() {
            Position playFieldSize = this.playfield.getPlayfieldSize();
            this.players[0] = new Player(
                "player1",
                player1Controls,
                new Position(playFieldSize.x + 3, 1),
                this.playfield.firstPosition,
                'o',
                'O');
            this.players[1] = new Player(
                "player2",
                player2Controls,
                new Position(playFieldSize.x + 3, 3),
                this.playfield.secondPosition,
                'x',
                'X');
        }

        public void start() {
            chooseDifficulty();
            initGame();
            playfield.writeStartGameText();
            if(Console.ReadKey(true).Key == ConsoleKey.Enter) {
                playfield.clearStartGameText();
                play();
            }
        }

        void chooseDifficulty() {
            Console.CursorVisible = true;
            Console.WriteLine("Schwierigkeit auswählen (1-9)");
            while(!(difficulty <= 9 && difficulty >= 1)) {
                Console.WriteLine("Bitte eine Zahl zwischen 1 und 9 eingeben!");
                try {
                    difficulty = Int32.Parse(Console.ReadLine());
                }
                catch(Exception ex) {
                    difficulty = 0;
                }
            }
        }

        void play() {
            while(isGameOver() == false) {
                processAllInputs();
                checkNextPositions();
                if(isGameOver() == false) {
                    foreach(Player player in this.players) {
                        player.snake.move();
                    }
                }
                System.Threading.Thread.Sleep(1000 / difficulty);
            }
        }

        void checkNextPositions() {
            foreach(Player player in this.players) {
                Position nextPosition = player.snake.getNextPosition();
                checkError(nextPosition, player);
                checkEatLining(nextPosition, player);
            }
        }

        void processAllInputs() {
            while(Console.KeyAvailable) {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;
                foreach(Player player in this.players) {
                    player.processUserInput(pressedKey);
                }
            }
        }

        bool isGameOver() {
            foreach(Player player in this.players) {
                if(player.gameOver) {
                    this.playfield.writeGameOverText(player.playerName);
                    return true;
                }
            }
            return false;
        }

        bool checkEatLining(Position nextPosition, Player player) {
            if(playfield.eatLining(nextPosition)) {
                player.snake.grow();
                player.score += difficulty;
                player.writeScore();
                playfield.generateLining(getAllSnakesPositions());
                return true;
            }
            return false;
        }

        bool checkError(Position nextPosition, Player player) {
            bool borderError = this.playfield.checkBorderError(nextPosition.x, nextPosition.y);
            bool snakeError = player.checkSnakeError(getAllSnakesPositions());

            if(borderError || snakeError) {
                return player.gameOver = true;
            }
            return false;
        }

        LinkedList<Position> getAllSnakesPositions() {
            LinkedList<Position> allSnakesPositions = new LinkedList<Position>();
            foreach(Player player in this.players) {
                foreach(Position pos in player.snake.getSnakePositions()) {
                    allSnakesPositions.AddLast(pos);
                }
            }
            return allSnakesPositions;
        }
    }
}

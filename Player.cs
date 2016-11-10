using System;
using System.Collections.Generic;

namespace MySnake {
    class Player {
        public Snake snake;
        public bool gameOver = false;
        PlayerControls playerControls;
        public int score = 0;
        Position scorePosition;
        public string playerName;

        public Player(string playerName, PlayerControls playerControls, Position scorePosition, Position startPosition) {
            this.playerName = playerName;
            this.playerControls = playerControls;
            this.scorePosition = scorePosition;
            this.snake = new Snake(startPosition);
        }

        public Player(string playerName, PlayerControls playerControls, Position scorePosition, Position startPosition, char snakeTailChar, char snakeHeadChar) {
            this.playerName = playerName;
            this.playerControls = playerControls;
            this.scorePosition = scorePosition;
            this.snake = new Snake(snakeTailChar, snakeHeadChar, startPosition);
            writeScore();
        }

        public void writeScore() {
            Console.SetCursorPosition(this.scorePosition.x, this.scorePosition.y);
            Console.Write(playerName + ": " + score);
        }

        public void processUserInput(ConsoleKey userInput) {
            if(userInput == this.playerControls.moveDown) {
                snake.changeMovement(Movement.DOWN);
            }
            else if(userInput == this.playerControls.moveUp) {
                snake.changeMovement(Movement.UP);
            }
            else if(userInput == this.playerControls.moveLeft) {
                snake.changeMovement(Movement.LEFT);
            }
            else if(userInput == this.playerControls.moveRight) {
                snake.changeMovement(Movement.RIGHT);
            }
            else if(userInput == this.playerControls.pause) {
                //TODO pause function
            }
        }

        public bool checkSnakeError(LinkedList<Position> allSnakePositions) {
            if(this.snake.checkNextPositionError(allSnakePositions)) {
                return false;
            }
            return true;
        }
    }
}

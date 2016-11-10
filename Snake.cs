using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnake {
    class Snake {
        //TODO make snakeChars variable
        int length = 5;
        char tailChar = 'O';
        char headChar = '0';
        LinkedList<Position> tail = new LinkedList<Position>();
        Position head;
        Movement lastMovement;
        Movement nextMovement = Movement.DOWN;

        public Snake(Position startPosition) {
            this.head = startPosition;
            drawHead();
        }

        public Snake(char tailChar, char headChar, Position startPosition) {
            this.tailChar = tailChar;
            this.headChar = headChar;
            this.head = startPosition;
            drawHead();
        }

        public bool changeMovement(Movement newMovement) {
            switch(newMovement) {
                case Movement.UP:
                    if(this.lastMovement == Movement.DOWN) {
                        return false;
                    }
                    break;
                case Movement.DOWN:
                    if(this.lastMovement == Movement.UP) {
                        return false;
                    }
                    break;
                case Movement.LEFT:
                    if(this.lastMovement == Movement.RIGHT) {
                        return false;
                    }
                    break;
                case Movement.RIGHT:
                    if(this.lastMovement == Movement.LEFT) {
                        return false;
                    }
                    break;
            }
            this.nextMovement = newMovement;
            return true;
        }

        public Position getNextPosition() {
            int moveX = 0;
            int moveY = 0;
            switch(this.nextMovement) {
                case Movement.DOWN:
                    moveY = 1;
                    break;
                case Movement.UP:
                    moveY = -1;
                    break;
                case Movement.LEFT:
                    moveX = -1;
                    break;
                case Movement.RIGHT:
                    moveX = 1;
                    break;

            }
            return new Position(this.head.x + moveX, this.head.y + moveY);
        }

        public void grow() {
            length++;
        }

        /*public void placeSnake(Position position) {
            this.head = position;
            drawHead();
        }*/

        public void move() {
            clearEnd();
            this.head = getNextPosition();
            drawHead();
            this.lastMovement = this.nextMovement;
        }

        void clearEnd() {
            if(tail.Count >= length - 1) {
                Console.SetCursorPosition(tail.Last.Value.x, tail.Last.Value.y);
                Console.Write(" ");
                tail.RemoveLast();
            }
            tail.AddFirst(head);
            Console.SetCursorPosition(tail.First.Value.x, tail.First.Value.y);
            Console.Write(tailChar);
        }

        void drawHead() {
            Console.SetCursorPosition(this.head.x, this.head.y);
            Console.Write(headChar);
        }

        public Position getHeadPosition() {
            return this.head;
        }

        public LinkedList<Position> getSnakePositions() {
            LinkedList<Position> snakePositions = new LinkedList<Position>(this.tail);
            snakePositions.AddFirst(this.head);
            return snakePositions;
        }

        public bool checkTailError() {
            Position nextPosition = getNextPosition();
            foreach(Position pos in this.tail) {
                if(pos.equals(nextPosition)) {
                    return false;
                }
            }
            return true;
        }

        public bool checkNextPositionError(LinkedList<Position> restrictedPositions) {
            Position nextPosition = getNextPosition();
            foreach(Position pos in restrictedPositions) {
                if(pos.equals(nextPosition)) {
                    return false;
                }
            }
            return true;
        }
    }
}

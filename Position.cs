using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnake {
    class Position {
        public int x;
        public int y;

        public Position(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public bool equals(Position pos) {
            if(pos.x == this.x && pos.y == this.y) {
                return true;
            }
            return false;
        }
    }
}

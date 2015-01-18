using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_battleship
{
    public class Ship
    {
        public int size;
        public bool picked;
        public bool placed;
        public int[,] pos;
        public int state;
        public Ship(int _size)
        {
            pos = new int[size, 2];      // numbers of decks and 2 coors of deck
            size = _size;
            picked = false;
            placed = false;
            state = 1;
        }
    }
}
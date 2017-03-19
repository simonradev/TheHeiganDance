namespace TheHeiganDance
{
    public class Position
    {
        public static Position[] areaOfEffectOfASpell = new Position[]
        {
            new Position(-1, -1),
            new Position(-1, 0),
            new Position(-1, +1),
            new Position(0, +1),
            new Position(+1, +1),
            new Position(+1, 0),
            new Position(+1, -1),
            new Position(0, -1)
        };

        public static Position[] runawayPathsOfThePlayer = new Position[]
        {
            new Position(-1, 0), //up
            new Position(0, +1), //right
            new Position(+1, 0), //down
            new Position(0, -1)  //left
        };

        private int row;
        private int col;

        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }

        public static bool PositionIsValid(Position toCheck)
        {
            bool isValid = true;

            if (toCheck.row < Chamber.minRows ||
                toCheck.row >= Chamber.maxRows ||
                toCheck.col < Chamber.minCols ||
                toCheck.col >= Chamber.maxCols)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}

namespace TheHeiganDance
{
    using System.Collections.Generic;

    public class Spell
    {
        private string name;
        private int duration;
        private int demage;
        private bool inEffect;
        private List<Position> areaOfEffect;

        public Spell(int duration, int demage, string name)
        {
            this.name = name;
            this.duration = duration;
            this.demage = demage;
            this.inEffect = true;
            this.areaOfEffect = new List<Position>();
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Duration
        {
            get { return this.duration; }
        }

        public int Demage
        {
            get { return this.demage; }
        }

        public bool InEffect
        {
            get { return this.inEffect; }
        }

        public List<Position> AreaOfEffect
        {
            get { return this.areaOfEffect; }
        }

        public void ReduceDuration()
        {
            this.duration--;

            if (duration == 0)
            {
                this.inEffect = false;
            }
        }

        public void SetAreaOfEffect(Position targetCell)
        {
            if (Position.PositionIsValid(targetCell))
            {
                areaOfEffect.Add(targetCell);
            }

            foreach (Position neighbourPosition in Position.areaOfEffectOfASpell)
            {
                Position toAdd = new Position(targetCell.Row + neighbourPosition.Row, targetCell.Col + neighbourPosition.Col);

                if (Position.PositionIsValid(toAdd))
                {
                    areaOfEffect.Add(toAdd);
                }
            }
        }
    }
}

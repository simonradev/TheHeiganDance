namespace TheHeiganDance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        private int health;
        private decimal demagePerTurn;
        private Position position;
        private bool isKilled;
        private List<Spell> activeSpells;
        private string killerSpell; 

        public Player(decimal demagePerTurn)
        {
            this.killerSpell = string.Empty;
            this.health = 18500;
            this.demagePerTurn = demagePerTurn;
            this.position = new Position(7, 7);
            this.isKilled = false;
            this.activeSpells = new List<Spell>();
        }

        public int Health
        {
            get { return this.health; }
        }

        public decimal DemagePerTurn
        {
            get { return this.demagePerTurn; }
        }

        public Position Position
        {
            get { return this.position; }
        }

        public bool IsKilled
        {
            get { return this.isKilled; }
        }

        public void AddSpell(Spell toAdd)
        {
            this.activeSpells.Add(toAdd);
        }

        public string KillerSpell
        {
            get { return this.killerSpell; }
        }

        public void CheckIfSpellHits(Spell toCheck)
        {
            bool itHits = false;

            foreach (Position cellItEffects in toCheck.AreaOfEffect)
            {
                if (position.Row == cellItEffects.Row && position.Col == cellItEffects.Col)
                {
                    itHits = true;

                    break;
                }
            }

            if (!itHits)
            {
                return;
            }

            bool menagedToEscape = CheckIfPlayerCouldEscape(toCheck);

            if (!menagedToEscape)
            {
                AddSpell(toCheck);
            }
        }

        private bool CheckIfPlayerCouldEscape(Spell toCheck)
        {
            bool canEscape = true;

            Position newPlayerPosition = null;
            foreach (Position escapeWay in Position.runawayPathsOfThePlayer)
            {
                newPlayerPosition = new Position(position.Row + escapeWay.Row, position.Col + escapeWay.Col);

                canEscape = true;
                foreach (Position effectedCell in toCheck.AreaOfEffect)
                {
                    bool isOnTheEffectedCell = newPlayerPosition.Row == effectedCell.Row && newPlayerPosition.Col == effectedCell.Col;
                    bool newPlayerPositionIsNotValid = !Position.PositionIsValid(newPlayerPosition);

                    if (isOnTheEffectedCell || newPlayerPositionIsNotValid)
                    {
                        canEscape = false;

                        break;
                    }
                }

                if (canEscape)
                {
                    break;
                }
            }

            if (canEscape)
            {
                position = newPlayerPosition;
            }
            
            return canEscape;
        }

        public void TakeDamage()
        {
            activeSpells = activeSpells.OrderByDescending(n => n.Name).ToList();

            for (int currSpell = 0; currSpell < activeSpells.Count; currSpell++)
            {
                Spell spell = activeSpells[currSpell];

                this.health -= spell.Demage;

                spell.ReduceDuration();

                if (this.health <= 0)
                {
                    this.isKilled = true;
                    this.killerSpell = spell.Name;

                    break;
                }

                if (!spell.InEffect)
                {
                    activeSpells.Remove(activeSpells[currSpell]);

                    currSpell--;
                }
            }
        }
    }
}

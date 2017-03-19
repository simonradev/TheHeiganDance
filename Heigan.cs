namespace TheHeiganDance
{
    public class Heigan
    {
        private decimal health;
        private bool isDefeated;

        public Heigan()
        {
            this.health = 3000000m;
            this.isDefeated = false;
        }

        public decimal Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public bool IsDefeated
        {
            get { return this.isDefeated; }
            set { this.isDefeated = value; }
        }

        public void TakeDemage(decimal demageHeiganTakes)
        {
            if (this.health - demageHeiganTakes <= 0)
            {
                this.isDefeated = true;
            }
            else
            {
                this.health -= demageHeiganTakes;
            }
        }
    }
}

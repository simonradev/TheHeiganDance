namespace TheHeiganDance
{
    using System;
    using System.Text;

    public class TheHeiganDanceEntryPoint
    {
        public static void Main()
        {
            decimal demageDoneToHeiganEachTurn = decimal.Parse(Console.ReadLine());

            Player player = new Player(demageDoneToHeiganEachTurn);
            Heigan heigan = new Heigan();

            while (true)
            {
                if (player.IsKilled || heigan.IsDefeated)
                {
                    break;
                }

                string inputLine = Console.ReadLine().Trim().ToLower();

                string[] spellInfo = inputLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string spell = spellInfo[0];
                int row = int.Parse(spellInfo[1]);
                int col = int.Parse(spellInfo[2]);

                // 1. heigan takes demage (done)
                // 2. if heigan is dead we skip the other things (done)
                // 3. the spell is casted (done)
                // 4. the spell hits and radius are checked and placed (done)
                // 5. the player tries to move only if a spell is on his cell
                // 6. if he has nowhere to go he takes demage
                // 7. if he dies we skip other things and print the result

                heigan.TakeDemage(player.DemagePerTurn);
                if (heigan.IsDefeated)
                {
                    goto SkipSpellIfHeiganIsDead;
                }

                switch (spell)
                {
                    case "cloud":
                        Spell plagueCloud = new Spell(2, 3500, "Plague Cloud");
                        plagueCloud.SetAreaOfEffect(new Position(row, col));

                        player.CheckIfSpellHits(plagueCloud);
                        break;

                    case "eruption":
                        Spell eruption = new Spell(1, 6000, "Eruption");
                        eruption.SetAreaOfEffect(new Position(row, col));

                        player.CheckIfSpellHits(eruption);
                        break;
                        
                    default:
                        break;
                }

                SkipSpellIfHeiganIsDead:

                player.TakeDamage();
            }

            StringBuilder result = new StringBuilder();
            if (heigan.IsDefeated)
            {
                result.AppendLine("Heigan: Defeated!");
            }
            else
            {
                result.AppendLine($"Heigan: {heigan.Health:f2}");
            }

            if (player.IsKilled)
            {
                result.AppendLine($"Player: Killed by {player.KillerSpell}");
            }
            else
            {
                result.AppendLine($"Player: {player.Health}");
            }

            result.Append($"Final position: {player.Position.Row}, {player.Position.Col}");

            Console.WriteLine(result.ToString());
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace Tibia.Objects
{
    /// <summary>
    /// Battle list object.
    /// </summary>
    public class BattleList
    {
        private Client client;

        /// <summary>
        /// Create a battlelist object.
        /// </summary>
        /// <param name="c"></param>
        public BattleList(Client c)
        {
            client = c;
        }

        /// <summary>
        /// Get a list of all the creatures on the battlelist.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Creature> GetCreatures()
        {
            return Enumerable.Range(0,(int) Addresses.BattleList.MaxCreatures)
                    .Select(index => Addresses.BattleList.Start + (uint)index * Addresses.BattleList.StepCreatures)
                    .Where(address => client.Memory.ReadByte(address + Addresses.Creature.DistanceIsVisible) == 1)
                    .Select(address => new Creature(client, address));
        }

        /// <summary>
        /// Show invisible creatures permanently.
        /// </summary>
        public void ShowInvisible()
        {
            client.Memory.WriteByte(Addresses.Map.RevealInvisible1,
                Addresses.Map.RevealInvisible1Edited);
            client.Memory.WriteByte(Addresses.Map.RevealInvisible2,
                Addresses.Map.RevealInvisible2Edited);
        }

        /// <summary>
        /// Hide invisible creatures permanently.
        /// </summary>
        public void HideInvisible()
        {
            client.Memory.WriteByte(Addresses.Map.RevealInvisible1,
                Addresses.Map.RevealInvisible1Default);
            client.Memory.WriteByte(Addresses.Map.RevealInvisible2,
                Addresses.Map.RevealInvisible2Default);
        }
    }
    
}
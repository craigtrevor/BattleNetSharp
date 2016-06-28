using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BattleNetSharp.Community.Wow
{
    /// <summary>
    ///   Represents an item bonus stat
    /// </summary>
    [DataContract]
    public class ItemStat
    {
        /// <summary>
        ///   Gets or sets the bonus stat type
        /// </summary>
        [DataMember(Name = "stat", IsRequired = true)]
        public ItemStatType StatType { get; internal set; }

        /// <summary>
        ///   Gets or sets the bonus stat amount
        /// </summary>
        [DataMember(Name = "amount", IsRequired = true)]
        public int Amount { get; internal set; }


        /// <summary>
        ///   Gets or sets whether the stat is reforged. this is true for the reforged to stat (the stat that is increased)
        /// </summary>
        [DataMember(Name = "reforged", IsRequired = false)]
        public bool IsReforged { get; internal set; }

        /// <summary>
        ///    Gets or sets the reforged amount i.e. the stat that is decreased when the item is reforged (this should be negative)
        /// </summary>
        [DataMember(Name = "reforgedAmount", IsRequired = false)]
        public int ReforgedAmount { get; internal set; }

        /// <summary>
        ///   Gets string representation (for debugging purposes)
        /// </summary>
        /// <returns> Gets string representation (for debugging purposes) </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}: {1}", StatType, Amount);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class PowerupBuilder : FeatureBuilder
    {
        public override string FeatureType => "Powerup";
        public override string InitialPathTemplateCs => TemplatePaths.DirPowerup;
        public override string InitialDirDestination => UnityPaths.DirPowerupBase;
        public override string InitialPathObjectPoolCs => null;

        public PowerupType PowerupType { get; private set; }
        public string PowerupTypeName => PowerupType.ToString();

        public override string TemplateName => $"{base.TemplateName}{PowerupTypeName}";

        public PowerupBuilder(string name, PowerupType type) : base(name)
        {
            PowerupType = type;

            string templateSuffix = $@"{PowerupTypeName}\";
            PathTemplate.Cs += $"{templateSuffix}{TemplateName}Powerup.cs";
            DirDestination += templateSuffix;
        }

        /// <summary>
        /// The parent struct used to hold the PowerupBalanceManager entry for this Powerup.
        /// Example: "public struct OnFireBalance"
        /// </summary>
        public string BaseBalanceStartTag
        {
            get
            {
                string tagStart = UnityPaths.TagPowerupBalanceManagerSubTypeStart;
                string tagEnd = UnityPaths.TagPowerupBalanceManagerSubTypeEnd;
                string subType = PowerupTypeName;

                string ret = $"{tagStart}{subType}{tagEnd}";
                return ret;
            }
        }
    }
}

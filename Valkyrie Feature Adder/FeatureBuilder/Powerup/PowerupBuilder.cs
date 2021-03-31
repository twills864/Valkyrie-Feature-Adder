using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Valkyrie_Feature_Adder
{
    public class PowerupBuilder : FeatureBuilder
    {
        public override string Type => "Powerup";
        public override string InitialPathTemplateCs => TemplatePaths.DirPowerup;
        public override string InitialDirDestination => UnityPaths.DirPowerupBase;
        public override string InitialPathObjectPoolCs => null;

        public Powerup PowerupType { get; private set; }
        public string PowerupTypeName => PowerupType.ToString();

        public override string TemplateName => $"{base.TemplateName}{PowerupTypeName}";

        public PowerupBuilder(string name, Powerup type) : base(name)
        {
            PowerupType = type;

            string templateSuffix = $@"{PowerupTypeName}\";
            PathTemplate.Cs += $"{templateSuffix}{TemplateName}Powerup.cs";
            DirDestination += templateSuffix;
        }

        // Example: "public struct OnFireBalance";
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

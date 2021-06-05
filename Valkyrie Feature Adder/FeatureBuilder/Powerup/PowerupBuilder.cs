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

        private const PowerupType FirstOnFireEnum = PowerupType.OnDefaultWeaponFire;
        public bool IsDefaultWeapon => PowerupType >= FirstOnFireEnum;

        public override string TemplateName => $"{base.TemplateName}{PowerupTypeName}";

        public PowerupBuilder(string name, PowerupType type) : base(name)
        {
            PowerupType = type;

            // Powerups are sorted into directories based on type.
            // EXAMPLES:
            // Powerups\BasicPowerups\OnHit\SmitePowerup.cs
            // Powerups\DefaultWeaponPowerups\OnDefaultWeaponFire\FireTwicePowerup.cs
            string powerupTypeDirectory = IsDefaultWeapon ?
                TemplatePaths.DirNameDefaultWeaponPowerups : TemplatePaths.DirNameBasicPowerups;
            string templateSuffix = $@"{powerupTypeDirectory}\{PowerupTypeName}\";
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

namespace Valkyrie_Feature_Adder
{
    public static partial class TemplatePaths
    {
        /// <summary>
        /// The path to the directory holding template files to be used.
        /// Will most likely contain the path to the directory hosting this project.
        /// Example: @"C:\Users\TJ\source\repos\Valkyrie Feature Adder\Valkyrie Feature Adder\Templates\";
        /// </summary>
        public const string DirTemplate = null;

        #region Bullet

        public const string PathPlayerBullet = DirTemplate + @"Bullets\Player\BasicBullet.cs";
        public const string PathEnemyBullet = DirTemplate + @"Bullets\Enemy\BasicEnemyBullet.cs";

        #endregion Bullet

        #region Enemy

        public const string PathEnemy = DirTemplate + @"Enemies\BasicEnemy.cs";

        #endregion Enemy

        #region FireStrategies

        public const string PathPlayerFireStrategy = DirTemplate + @"FireStrategies\Player\BasicStrategy.cs";
        public const string PathEnemyFireStrategy = DirTemplate + @"FireStrategies\Enemy\BasicEnemyStrategy.cs";

        #endregion FireStrategies

        #region Powerup

        public const string DirPowerup = DirTemplate + @"Powerups\";

        public const string PathPowerupBalanceStructTemplate = DirPowerup + @"PowerupBalanceManagerTemplate.txt";

        public const string DirNameBasicPowerups = "BasicPowerups";
        public const string DirNameDefaultWeaponPowerups = "DefaultWeaponPowerups";

        #endregion Powerup
    }
}

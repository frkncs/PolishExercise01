using UnityEngine;

namespace DaysCome._GameData.Scripts
{
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "Game Data/New AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        [Header("In Game Sounds")]
        public AudioGroup playerBulletToEnemy;
        //public AudioGroup playerBulletToEnvironment;
        public AudioGroup playerDamaged;
        public AudioGroup playerDeath;
        public AudioGroup playerSpeedBoost;
        //public AudioGroup turretBulletToEnemy;
        //public AudioGroup turretBulletToEnvironment;
        public AudioGroup enemyDamaged;
        public AudioGroup enemyDeath;
        public AudioGroup enemyFall;
        public AudioGroup footstep;
        public AudioGroup footstepMetal;
        public AudioGroup keyEarned;
        public AudioGroup keysNotEnough;
        public AudioGroup loadKeysToUnlock;
        public AudioGroup doorOpening;
        public AudioGroup bulletShellBouncing;

        [Space(10)]
        [Header("Trap Sounds")]
        public AudioGroup boobyTrapOpening;
        public AudioGroup boobyTrapClosing;
        public AudioGroup switchSound;
        public AudioGroup ropePull;
        public AudioGroup ropeAttach;
        public AudioGroup ropeBreak;

        [Space(10)]
        [Header("UI Sounds")]
        public AudioGroup UIclick;
        public AudioGroup levelCompletion;
        public AudioGroup healthUpgrade;
        public AudioGroup weaponUpgrade;

        [Space(10)]
        [Header("Misc.")]
        public AudioGroup positiveFeedback;
        public AudioGroup negativeFeedback;
    }
}
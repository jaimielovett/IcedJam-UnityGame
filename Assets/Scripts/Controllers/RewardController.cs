using UnityEngine;

/*
 * The RewardController class stores whether each of the different rewards available from the shop has been 
 * unlocked or not. It also stores the price and name of each of them too, but this is not currently used.
 */

public class RewardController : MonoBehaviour
{
    public Reward X2MultiplierReward;
    public Reward X4MultiplierReward;
    public Reward X8MultiplierReward;
    public Reward PurpleColourReward;
    public Reward PentagonShapeReward;
    public Reward ExtraLifeReward;
    public Reward X20MaxMultiplierReward;
    public Reward HardModeReward;
    public Reward InsaneModeReward;

    public static RewardController Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        X2MultiplierReward = new Reward("X2 Reward", 10000, false);
        X4MultiplierReward = new Reward("X4 Reward", 20000, false);
        X8MultiplierReward = new Reward("X4 Reward", 30000, false);
        PurpleColourReward = new Reward("Purple", 10000, false);
        PentagonShapeReward = new Reward("Pentagon", 20000, false);
        ExtraLifeReward = new Reward("Extra Life", 30000, false);
        X20MaxMultiplierReward = new Reward("X20 Max Multiplier Reward", 10000, false);
        HardModeReward = new Reward("Hard Mode Reward", 20000, false);
        InsaneModeReward = new Reward("Insane Mode Reward", 30000, false);
    }
}

using UnityEngine;
using System.Collections;

public enum GameDifficulty {

    EASY,
    NORMAL,
    HARD,
    INSANE
};

public enum GameState {

    MAIN_MENU,
    LEVEL,
    BOSS_LEVEL,
    GAME_OVER,
    PAUSE,
    STORE
};

public enum LevelType {

    COLOURED_SHAPE,
    COLOUR,
    ODD_ONE_OUT,
    SHAPE,
    REACTION,
    AVOID,
    LARGEST_SIZE,
    SMALLEST_SIZE,
    PROXIMITY,
    //MEMORY,
    NUM_LEVEL_TYPES,
    GAME_OVER
};

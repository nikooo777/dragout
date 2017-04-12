using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class  GameState {
    public static int level = 1;
    private static int nextLevel = 5;
    public static int points = 0;
    public static void checkLevel() {
        if (points>=level*nextLevel) {
            level++;
        }
    }
}

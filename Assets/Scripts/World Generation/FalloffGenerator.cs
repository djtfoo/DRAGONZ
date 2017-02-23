using UnityEngine;
using System.Collections;

public static class FalloffGenerator {

	public static float[,] GenerateFalloffMap(int size) {
        float[,] map = new float[size, size];

        for (int i = 0; i < size; ++i) {
            for (int j = 0; j < size; ++j) {
                // convert values to a range of -1 to 1
                float x = i / (float)(size) * 2 - 1;
                float y = j / (float)(size) * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i,j] = Evaluate(value);
            }
        }

        return map;
    }

    static float Evaluate(float value) {
        float a = 5;
        float b = 2.2f;

        return Mathf.Pow(value,a) / (Mathf.Pow(value, a) + Mathf.Pow(b- b * value, a));
    }
}

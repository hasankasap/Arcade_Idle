using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public static class MouseUtils
{
    public static bool IsMouseOnUI()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
public static class Remap
{
    public static float Calculate(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (from - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }
}
public static class TextUtility
{
    private static readonly SortedDictionary<int, string> abbrevations = new SortedDictionary<int, string>
            {
                {1000,"K"},
                {1000000, "M" },
                {1000000000, "B" }
            };

    public static string FloatToStringConverter(float number)
    {
        for (int i = abbrevations.Count - 1; i >= 0; i--)
        {
            KeyValuePair<int, string> pair = abbrevations.ElementAt(i);
            float n = Mathf.Abs(number);
            if (n >= 10000 && n >= pair.Key)
            {
                //int roundedNumber = Mathf.FloorToInt(number / pair.Key);
                float roundedNumber  = (number / pair.Key);
                return roundedNumber.ToString("0.00") + pair.Value;
            }
        }
        return number.ToString("0");
    }

    public static string AddSpaceBetweenUppercase(string input)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            if (i > 0 && char.IsUpper(input[i]))
            {
                output.Append(' ');
            }

            output.Append(input[i]);
        }

        return output.ToString();
    }
    public static string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

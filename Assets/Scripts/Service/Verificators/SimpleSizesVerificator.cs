public static class SimpleSizesVerificator
{
    public static int GetIntFromStringSizeOfRoom(string s)
    {
        switch (s)
        {
            case "Маленькие":
                return UnityEngine.Random.Range(5, 12);
            case "Средние":
                return UnityEngine.Random.Range(12, 20);
            case "Большие":
                return UnityEngine.Random.Range(20, 30);
            default:
                return 15;
        }
    }

    public static int GetIntFromStringCountOfRoom(string s)
    {
        switch (s)
        {
            case "Малое":
                return UnityEngine.Random.Range(5, 12);
            case "Среднее":
                return UnityEngine.Random.Range(12, 20);
            case "Большое":
                return UnityEngine.Random.Range(20, 30);
            default:
                return 15;
        }
    }
}

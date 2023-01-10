using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneration : MonoBehaviour
{


    public static int CurrentX = 1;
    public static int CurrentY = 1;

    public static Stack<int> visitedX = new Stack<int>(10000);
    public static Stack<int> visitedY = new Stack<int>(10000);


    public GameObject wallCell;
    public GameObject floorCell;
    public GameObject finishCell;


    void Start()
    {
        int size = System.Convert.ToInt32(setValues.mapSize);
        string seed = System.Convert.ToString(setValues.seed);
        decimal difficulty = System.Convert.ToDecimal(setValues.difficulty);
        bool randomSeed = setValues.randomSeed;

        int size2 = size;
        size = size * 5 + 30;       // largest size is 70*70. Upon a size of 200*200, performance issues start to come in

        // ----------------------------------------------------------------- Seed Declaration -----------------------------------------------
        long seedNum = 1;
        int seedCharacter = 0;

        if (randomSeed || seed == null)
        {
            seedNum = Random.Range(10000000, 1000000000);
        }

        if (seed != null)
        {
            for (int count = 0; count < seed.Length - 1; count++)
            {
                seedNum = System.Convert.ToInt32(seed[count]) + seedNum;
            }

            foreach (char c in seed)
            {
                seedCharacter = seedCharacter + System.Convert.ToInt32(c);
            }

            for (int count1 = 1; count1 < seed.Length - 1; count1++)
            {
                seedNum = seedNum * (seedCharacter % count1 + 1);
            }
        }


        Debug.Log(seedNum);

        long seedNum2 = seedNum;
        seedNum2 = seedNum * seedNum * seedNum;

        if (seedNum2 < 0)
        {
            seedNum2 = seedNum2 * -1;
        }

        seedNum = seedNum * seedNum;
        if (seedNum < 0)
        {
            seedNum = seedNum * -1;
        }

        long seedNum3 = seedNum2 * seedNum * 3;
        if (seedNum3 < 0)
        {
            seedNum3 = seedNum3 * -1;
        }

        // --------------------------- Seed to Array --------------------

        int length1 = 0;
        long seed1temp = seedNum;

        while (seed1temp > 0)
        {
            seed1temp = seed1temp / 10;
            length1++;
        }
        int[] seedArray1 = new int[length1];

        for (int seedcount = 0; seedcount < length1; seedcount++)
        {
            seedArray1[seedcount] = System.Convert.ToInt32(seedNum % 10 ^ (length1 - seedcount));
            seedNum = seedNum / 10;
        }


        int length2 = 0;
        long seed2temp = seedNum2;

        while (seed2temp > 0)
        {
            seed2temp = seed2temp / 10;
            length2++;
        }
        int[] seedArray2 = new int[length2];

        for (int seedcount = 0; seedcount < length2; seedcount++)
        {
            seedArray2[seedcount] = System.Convert.ToInt32(seedNum2 % 10 ^ (length2 - seedcount));
            seedNum2 = seedNum2 / 10;
        }


        int length3 = 0;
        long seed3temp = seedNum3;

        while (seed3temp > 0)
        {
            seed3temp = seed3temp / 10;
            length3++;
        }
        int[] seedArray3 = new int[length3];

        for (int seedcount = 0; seedcount < length3; seedcount++)
        {
            seedArray3[seedcount] = System.Convert.ToInt32(seedNum3 % 10 ^ (length3 - seedcount));
            seedNum3 = seedNum3 / 10;
        }


        // ----------------------------------- Grid Making ----------------------------

        if (size2 % 2 == 0)
        {
            size--;
        }
        int[,] Grid = new int[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Grid[x, y] = 0;
            }
        }

        for (int lines = 0; lines < size; lines++)
        {
            Grid[lines, 0] = 2;
            Grid[lines, size - 1] = 2;
        }

        for (int edges = 0; edges < size; edges++)
        {
            Grid[0, edges] = 2;
            Grid[size - 1, edges] = 2;
        }



        int direction = 0;

        // -------------------------------- Main Generation Section ---------------------------

        CurrentX = 1;
        CurrentY = 1;



        visitedX.Push(1);
        visitedY.Push(1);

        Grid[CurrentX, CurrentY] = 1;

        move(Grid, direction, visitedX, visitedY, seedArray1, seedArray2);

        while (CurrentX != 1 || CurrentY != 1)
        {
            move(Grid, direction, visitedX, visitedY, seedArray1, seedArray2);
        }


        if (difficulty != 1)
        {
            Loops(Grid, difficulty, size);
        }


        if (seedArray3[1] % 2 == 0)
        {
            Grid[1, size - 1] = 3;
        }
        else
        {
            Grid[size - 1, 1] = 3;
        }




        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {


                if (Grid[x, y] == 0 || Grid[x,y] == 2)
                {
                    Vector3 position = new Vector3(x, 1, y);
                    Instantiate(wallCell, position, Quaternion.identity);
                }
                else if (Grid[x, y] == 1 || Grid[x, y] == 4)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    Instantiate(floorCell, position, Quaternion.identity);
                }

                else if (Grid[x,y] == 3)
                {
                    Vector3 position = new Vector3(x, 0, y);
                    Instantiate(finishCell, position, Quaternion.identity);
                }




            }
        }



    }






    // ------------------------------------------ Adjacent cell move ----------------------------
    static public int[,] move(int[,] Grid, int direction, Stack<int> VisitedX, Stack<int> VisitedY, int[] seedArray, int[] seedArray2)
    {

        direction = 0;

        int counter = 0;
        int chooseValue = -1;
        int directionNumber = 0;

        Stack<string> directionChoices = new Stack<string>();
        directionChoices.Clear();

        try
        {
            if (Grid[CurrentX, CurrentY + 2] == 0)
            {
                directionChoices.Push("Down");
                directionNumber++;
            }
            else { direction++; }
        }
        catch { direction++; }

        try
        {
            if (Grid[CurrentX, CurrentY - 2] == 0)
            {
                directionChoices.Push("Up");
                directionNumber++;
            }
            else { direction++; }
        }
        catch { direction++; }

        try
        {
            if (Grid[CurrentX + 2, CurrentY] == 0)
            {
                directionChoices.Push("Right");
                directionNumber++;
            }
            else { direction++; }
        }
        catch { direction++; }

        try
        {
            if (Grid[CurrentX - 2, CurrentY] == 0)
            {
                directionChoices.Push("Left");
                directionNumber++;
            }
            else { direction++; }
        }
        catch { direction++; }



        if (direction == 4)
        {

            backtrackX();
            backtrackY();
        }
        else
        {

            chooseValue = seedArray[counter] % (directionNumber + 1);
            seedArray[counter] = seedArray[counter] + 1;
 

            for (int x = directionNumber; x > chooseValue + 1; x--)
            {
                directionChoices.Pop();
            }

            if (directionChoices.Peek() == "Left")
            {
                Left(Grid);
            }

            if (directionChoices.Peek() == "Right")
            {
                Right(Grid);
            }

            if (directionChoices.Peek() == "Up")
            {
                Up(Grid);
            }

            if (directionChoices.Peek() == "Down")
            {
                Down(Grid);
            }
        }

        return Grid;

    }


    // ------------------------------- Backtracking -----------------------------------



    public static void backtrackX()
    {
        for (int x = 0; x < 2; x++)
        {
            try { visitedX.Pop(); }
            catch { }
        }
        try { CurrentX = visitedX.Peek(); }
        catch { CurrentX = 1; }
    }

    public static void backtrackY()
    {
        for (int x = 0; x < 2; x++)
        {
            try { visitedY.Pop(); }
            catch { }
        }
        try { CurrentY = visitedY.Peek(); }
        catch { CurrentY = 1; }
    }



    // ------------------------------------------- Direction Moving --------------------------------------------
    public static int[,] Left(int[,] Grid)
    {
        for (int i = 1; i < 3; i++)
        {
            Grid[CurrentX - i, CurrentY] = 1;
            visitedX.Push(CurrentX - i);
            visitedY.Push(CurrentY);
        }
        CurrentX = CurrentX - 2;
        return Grid;
    }


    public static int[,] Right(int[,] Grid)
    {
        for (int i = 1; i < 3; i++)
        {
            Grid[CurrentX + i, CurrentY] = 1;
            visitedX.Push(CurrentX + i);
            visitedY.Push(CurrentY);
        }
        CurrentX = CurrentX + 2;
        return Grid;
    }


    public static int[,] Up(int[,] Grid)
    {
        for (int i = 1; i < 3; i++)
        {
            Grid[CurrentX, CurrentY - i] = 1;
            visitedX.Push(CurrentX);
            visitedY.Push(CurrentY - i);
        }
        CurrentY = CurrentY - 2;
        return Grid;

    }
    public static int[,] Down(int[,] Grid)
    {
        for (int i = 1; i < 3; i++)
        {
            Grid[CurrentX, CurrentY + i] = 1;
            visitedX.Push(CurrentX);
            visitedY.Push(CurrentY + i);
        }
        CurrentY = CurrentY + 2;
        return Grid;
    }

    // --------------------------------------------------------------------------- Loops -------------------------------------------
    public static int[,] Loops(int[,] Grid, decimal difficulty, int size)
    {
        int loopNumber = System.Convert.ToInt32((3 * size) / ((2 * difficulty + 1) * 5));

        for (int x = 0; x < loopNumber; x++)
        {
            int chooseX = Random.Range(3, size - 2);
            int chooseY = Random.Range(3, size - 2);

            if (chooseX % 2 == 0 && chooseY % 2 == 0)
            {
                if (Random.Range(1, 2) == 1)
                {
                    chooseX++;
                }
                else
                {
                    chooseY++;
                }
            }

            if (Grid[chooseX, chooseY] == 0)
            {
                Grid[chooseX, chooseY] = 4;
            }
            else
            {
                x--;
            }
        }

        return Grid;
    }


}




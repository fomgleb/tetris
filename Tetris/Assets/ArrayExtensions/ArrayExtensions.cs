using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;

namespace ArrayExtensions
{
    public static class ArrayExtensions
    {
        // public static uint GetSolidLineWidth<T>(this T[,] inputTwoDimensionalArray, uint lineIndex)
        // {
        //     var lineStarted = false;
        //     var cellContentLine = new List<EcsEntity>();
        //
        //     for (var x = 0; x < line.Length; x++)
        //     {
        //         if (line[x].Get<CellContentComponent>().Content == contentToCheck)
        //         {
        //             lineStarted = true;
        //             cellContentLine.Add(line[x]);
        //         }
        //         else if (lineStarted)
        //             break;
        //     }
        //
        //     return cellContentLine;
        // }
        
        public static T[,] ToTwoDimensionalArray<T>(this T[] inputArray, uint width)
        {
            var height = (uint)Math.Ceiling(inputArray.Length / (float)width);

            var twoDimensionalArray = new T[height, width];

            var y = 0;
            var x = 0;
            foreach (var arrayElement in inputArray)
            {
                if (x == width)
                {
                    y++;
                    x = 0;
                }
                
                twoDimensionalArray[y, x] = arrayElement;

                x++;
            }

            return twoDimensionalArray;
        }
    }
}
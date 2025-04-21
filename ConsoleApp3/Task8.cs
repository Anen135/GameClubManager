using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks {
    class Task8 {

        public Task8() {
            int[] arr1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[][] arr2 = new int[5][];
            arr2[0] = arr1[0..1];
            arr2[1] = arr1[0..2];
            arr2[2] = arr1[0..3];
            arr2[3] = arr1[0..4];
            arr2[4] = arr1[0..];

            int[,] matrix = new int[5, arr1.Length];

            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < arr1.Length; j++) {
                    matrix[i, j] = arr1[j];
                }
            }

            List<int[]> list = new List<int[]>();
            list.Add(arr1[0..]);
            list.Add(arr1[0..]);
            list.Add(arr1[0..]);
            list.Add(arr1[0..]);
            list.Add(arr1[0..]);

            arr1[0] = 100;


            // Print

            Console.WriteLine("Arr1 Rank: " +  arr1.Rank);
            Console.WriteLine("Arr2 Rank: " + arr2.Rank);
            Console.WriteLine("Matrix Rank: " + matrix.Rank);
            

            Console.WriteLine("GetLength(0) = " + matrix.GetLength(0));
            Console.WriteLine("GetLength(1) = " + matrix.GetLength(1));
            Console.WriteLine("Matrix.Length = " + matrix.Length);
            Console.WriteLine("List.Count = " + list.Count);
            Console.WriteLine("Arr2.Length = " + arr2.Length);


            Console.WriteLine("Array:");
            foreach (var item in arr1) {
                Console.Write(item + " ");
            }
            Console.WriteLine("\nMatrix:");
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < arr1.Length; j++) {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("List:");
            foreach (var item in list) {
                foreach (var item2 in item) {
                    Console.Write(item2 + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Arr2:");
            foreach (var item in arr2) {
                foreach (var item2 in item) {
                    Console.Write(item2 + " ");
                }
                Console.WriteLine ();
            }
        }
    }
}

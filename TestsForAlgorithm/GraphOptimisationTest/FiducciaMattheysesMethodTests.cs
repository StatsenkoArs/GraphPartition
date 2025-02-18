using Feduchi_Matteus_method_exampl_1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FiducciaMattheysesMethod_EqualDistribution10Vertex_EqualDistribution()
        {
            //arrange
            int[][] g = new int[][]
            {
                new int[]{1, 2 },
                new int[]{0, 2, 3},
                new int[]{0, 1, 4},
                new int[]{1, 4},
                new int[]{2, 3, 5, 6},
                new int[]{4, 6, 7, 8},
                new int[]{4, 5, 7, 9},
                new int[]{5, 6, 8, 9},
                new int[]{5, 7},
                new int[]{6, 7}
            };
            GraphSRC graph = new GraphSRC(g);
            int[] r = new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];

            //act
            newR = f.OptimisePartition(graph, r, 1);

            //assert
            Assert.AreEqual(5, newR.Sum());
        }



        [TestMethod]
        public void FiducciaMattheysesMethod_NotEqualDistribution10Vertex_EqualDistribution()
        {
            //arrange
            Generator generator = new Generator();
            int[][] g = new int[][]
            {
                new int[]{1, 2 },
                new int[]{0, 2, 3},
                new int[]{0, 1, 4},
                new int[]{1, 4},
                new int[]{2, 3, 5, 6},
                new int[]{4, 6, 7, 8},
                new int[]{4, 5, 7, 9},
                new int[]{5, 6, 8, 9},
                new int[]{5, 7},
                new int[]{6, 7}
            };
            GraphSRC graph = new GraphSRC(g);
            int[] r = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];

            //act
            newR = f.OptimisePartition(graph, r, 1);

            //assert
            Assert.AreEqual(5, newR.Sum());
        }


        //[TestMethod]
        //public void Graph_30Vertex()
        //{
           
        //    int[][] g = Method.ReadGraph("C:\\Users\\ASUS\\Desktop\\ННГУ\\ДИПЛОМ\\Попытка в методы\\graphs\\graph_30.txt", 30);
        //    GraphSRC graph = new GraphSRC(g);


        //    Generator generator = new Generator();
        //    int[] r = generator.GenerateSubgrapf(30);
        //    FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();

        //    int[] newR = new int[r.Length];
        //    newR = f.OptimisePartition(graph, r, 1);
        //    Assert.AreEqual(15, newR.Sum());
        //}

        //[TestMethod]
        //public void Graph_50Vertex()
        //{
        //    Generator generator = new Generator();
        //    int[][] g = Method.ReadGraph("C:\\Users\\ASUS\\Desktop\\ННГУ\\ДИПЛОМ\\Попытка в методы\\graphs\\graph_50.txt", 50);
        //    GraphSRC graph = new GraphSRC(g);
        //    int[] r = generator.GenerateSubgrapf(50);
        //    FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();

        //    int[] newR = new int[r.Length];
        //    newR = f.OptimisePartition(graph, r, 1);
        //    Assert.AreEqual(25, newR.Sum());
        //}

        //[TestMethod]
        //public void Graph_500Vertex()
        //{
        //    Generator generator = new Generator();
        //    const int n = 500;
        //    int[][] g = generator.Generate(n, n/2 + n/3, n-n/3, 3);
        //    GraphSRC graph = new GraphSRC(g);
        //    int[] r = generator.GenerateSubgrapf(500);
        //    FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();

        //    int[] newR = new int[r.Length];
        //    newR = f.OptimisePartition(graph, r, 1);
        //    Assert.AreEqual(250, newR.Sum());
        //}

        
        [TestMethod]
        public void FiducciaMattheysesMethod_UnconnectedGraphAndNotEqualDistribution2Vertex_EqualDistribution()
        {
            //arrange
            int[][] g = new int[][]
            {
                new int[]{},
                new int[]{},
            };
            GraphSRC graph = new GraphSRC(g);
            int[] r = new int[] {0, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];
            //act
            newR = f.OptimisePartition(graph, r, 1);
            //assert
            Assert.AreEqual(1, newR.Sum());
        }

        [TestMethod]
        public void FiducciaMattheysesMethod_UnconnectedGraphAndEqualDistribution2Vertex_EqualDistribution()
        {
            //arrange
            int[][] g = new int[][]
            {
                new int[]{},
                new int[]{},
            };
            GraphSRC graph = new GraphSRC(g);
            int[] r = new int[] { 1, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];
            //act
            newR = f.OptimisePartition(graph, r, 1);
            //assert
            Assert.AreEqual(1, newR.Sum());
        }




    }

}

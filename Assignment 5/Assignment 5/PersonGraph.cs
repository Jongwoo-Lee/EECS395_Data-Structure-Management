using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment_5
{
    /// <summary>
    /// Implements an undirected graph of "connections" between named people
    /// a la LinkedIn or Facebook.
    /// </summary>
    public class GraphEdge
    {
        public bool visited { get; set; }

        public int distance { get; set; }

        public List<string> friends { get; set; }

        public GraphEdge()
        {
            this.visited = false;
            this.distance = 0;
            this.friends = new List<string>();
        }

    }
    public class PersonGraph
    {
        public List<GraphEdge> People;
        public Dictionary<string, int> Index;
        public static int i = 0;
        public PersonGraph()
        {
            People = new List<GraphEdge>();
            Index = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds a new person (node) to the graph
        /// </summary>
        /// <param name="name">Name of the person</param>

        public void AddPerson(string name)
        {
            People.Add(new GraphEdge());
            Index.Add(name, i);
            i++;
        }

        /// <summary>
        /// Adds a new edge to the graph
        /// </summary>
        /// <param name="person1">Name of first person</param>
        /// <param name="person2">Name of second person</param>
        public void AddConnection(string person1, string person2)
        {
            if (!Index.ContainsKey(person1))
            {
                AddPerson(person1);
            }
            if (!Index.ContainsKey(person2))
            {
                AddPerson(person2);
            }

            People[Index[person1]].friends.Add(person2);

            People[Index[person2]].friends.Add(person1);

        }

        /// <summary>
        /// Returns the length of the shortest path between two people in the graph
        /// For example, the distance from a node to itself is 0, from a node to a
        /// neighbor is 1, etc.
        /// </summary>
        /// <param name="person1">Name of the first person</param>
        /// <param name="person2">Name of the second person</param>
        /// <returns>Length of the path</returns>
        public int Distance(string person1, string person2)
        {
            if (person1 == person2)
                return People[Index[person1]].distance;

            Queue<string> q = new Queue<string>();
            string node;
            q.Enqueue(person1);
            People[Index[person1]].distance = 0;
            People[Index[person1]].visited = true;

            while (q.Count != 0)
            {
                node = q.Dequeue();
                foreach (string edge in People[Index[node]].friends)
                {
                    if (People[Index[edge]].visited == false)
                    {
                        q.Enqueue(edge);
                        People[Index[edge]].visited = true;
                        People[Index[edge]].distance = People[Index[node]].distance + 1;
                        if (edge == person2)
                        {
                            int aa = People[Index[edge]].distance;
                            foreach (var dis in People)
                            {
                                dis.distance = 0;
                                dis.visited = false;
                            }
                            return aa;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
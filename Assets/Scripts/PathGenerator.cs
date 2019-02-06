using System;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{

    static public Path<Node> FindPath<Node>(
        Node start,
        Node destination,
        Func<Node, Node, double> distance,
        Func<Node, double> estimate)
        where Node : IHasNeighbours<Node>
    {
        var closed = new HashSet<Node>();
        var queue = new PriorityQueue<double, Path<Node>>();
        queue.Enqueue(0, new Path<Node>(start));
        while (!queue.IsEmpty)
        {
            var path = queue.Dequeue();
            if (closed.Contains(path.LastStep))
                continue;
            if (path.LastStep.Equals(destination))
                return path;
            closed.Add(path.LastStep);
            foreach (Node n in path.LastStep.Neighbours)
            {
                double d = distance(path.LastStep, n);
                var newPath = path.AddStep(n, d);
                queue.Enqueue(newPath.TotalCost + estimate(n), newPath);
            }
        }
        return null;
    }

    static public IEnumerable<Node> FindAllVisitableNodes<Node>(
       Node start,
       double maxDistance,
       Func<Node, Node, double> distance)
       where Node : IHasNeighbours<Node>
    {
        var closed = new HashSet<Node>();
        var queue = new PriorityQueue<double, Path<Node>>();
        queue.Enqueue(0, new Path<Node>(start));
        while (!queue.IsEmpty)
        {
            var path = queue.Dequeue();
            if (closed.Contains(path.LastStep))
                continue;

            closed.Add(path.LastStep);
            foreach (Node n in path.LastStep.Neighbours)
            {
                double d = distance(path.LastStep, n);
                var newPath = path.AddStep(n, d);
                var newCost = newPath.TotalCost;
                if (newCost < maxDistance)
                    queue.Enqueue(newCost, newPath);
            }
        }
        List<Node> ret = new List<Node>();
        foreach (var n in closed)
        {
            ret.Add(n);
        }

        return ret;
    }
}
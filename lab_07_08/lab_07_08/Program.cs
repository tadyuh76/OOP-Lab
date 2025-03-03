// Lab 07 - 08 (Lab 07 từ câu 01-05, Lab 08 từ câu 06-07).
// Một lớp Point trong hệ toạ độ Descartes 2 chiều gồm các
// thuộc tính: x, y. Một lớp Cluster chứa một list các Point
// 1/ Xây dựng lớp Point.
// 2/ Bổ sung vào Point: ToString dạng A(x, y), distance
// tính khoảng cách giữa 2 điểm theo Euclidean.
// 3/ Bổ sung vào lớp Cluster: ToString dạng {A(x, y), B(x,
// 4/ Bổ sung phương thức distance cho Cluster để tính
// khoảng cách giữa các cụm theo single linkage (theo
// khoảng cách nhỏ nhất giữa các cặp điểm của 2 cụm).
// 5/ Bổ sung operator + để hợp 2 Cluster.
// 6/ Cài đặt thuật toán hierarchical clustering để gom
// các cụm lại với nhau (với single linkage).
// public List<Cluster> HierarchicalClustering()
// 7/ Triển khai kết quả trong hàm main.


using System;
using System.Collections.Generic;
using System.Text;

namespace lab_07_08
{
    // 1/ Xây dựng lớp Point
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        // 2/ Bổ sung vào Point: ToString dạng A(x, y), distance
        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }

        // Tính khoảng cách giữa 2 điểm theo Euclidean
        public double Distance(Point other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }

    class Cluster
    {
        public List<Point> Points { get; private set; }

        public Cluster()
        {
            Points = new List<Point>();
        }

        public Cluster(Point point)
        {
            Points = new List<Point> { point };
        }

        public Cluster(List<Point> points)
        {
            Points = new List<Point>(points);
        }

        // 3/ Bổ sung vào lớp Cluster: ToString dạng {A(x, y), B(x, y), ...}
        public override string ToString()
        {
            if (Points.Count == 0)
                return "{}";

            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            for (int i = 0; i < Points.Count; i++)
            {
                sb.Append(Points[i].ToString());
                if (i < Points.Count - 1)
                    sb.Append(", ");
            }

            sb.Append("}");
            return sb.ToString();
        }

        // 4/ Bổ sung phương thức distance cho Cluster để tính khoảng cách giữa các cụm theo single linkage
        public double Distance(Cluster other)
        {
            if (Points.Count == 0 || other.Points.Count == 0)
                return double.MaxValue;

            double minDistance = double.MaxValue;

            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < other.Points.Count; j++)
                {
                    double distance = Points[i].Distance(other.Points[j]);
                    if (distance < minDistance)
                        minDistance = distance;
                }
            }

            return minDistance;
        }

        // 5/ Bổ sung operator + để hợp 2 Cluster
        public static Cluster operator +(Cluster c1, Cluster c2)
        {
            Cluster result = new Cluster();

            for (int i = 0; i < c1.Points.Count; i++)
            {
                result.Points.Add(c1.Points[i]);
            }

            for (int i = 0; i < c2.Points.Count; i++)
            {
                result.Points.Add(c2.Points[i]);
            }

            return result;
        }

        // 6/ Cài đặt thuật toán hierarchical clustering để gom các cụm lại với nhau (với single linkage)
        public static List<Cluster> HierarchicalClustering(List<Cluster> initialClusters, int targetClusters = 1)
        {
            List<Cluster> clusters = new List<Cluster>();

            // Copy list clusters
            for (int i = 0; i < initialClusters.Count; i++)
            {
                clusters.Add(initialClusters[i]);
            }

            // Merge cho tới khi có được số cluster như mong muốn
            while (clusters.Count > targetClusters)
            {
                double minDistance = double.MaxValue;
                int minI = -1, minJ = -1;

                // Tìm 2 cụm gần nhau nhất
                for (int i = 0; i < clusters.Count; i++)
                {
                    for (int j = i + 1; j < clusters.Count; j++)
                    {
                        double distance = clusters[i].Distance(clusters[j]);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            minI = i;
                            minJ = j;
                        }
                    }
                }

                // Merge 2 cụm gần nhau nhất
                if (minI != -1 && minJ != -1)
                {
                    Cluster merged = clusters[minI] + clusters[minJ];

                    // Xoá 2 cụm đã merge (ưu tiên xoá cụm có index lớn hơn trước)
                    if (minI > minJ)
                    {
                        clusters.RemoveAt(minI);
                        clusters.RemoveAt(minJ);
                    }
                    else
                    {
                        clusters.RemoveAt(minJ);
                        clusters.RemoveAt(minI);
                    }

                    clusters.Add(merged);
                }
                else
                {
                    // Nếu không tìm được 2 cụm gần nhau nhất thì thoát vòng lặp
                    break;
                }
            }

            return clusters;
        }
    }

    class Program
    {
        // 7/ Triển khai kết quả trong hàm main
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Tạo các điểm
            Point p1 = new Point(1, 2);
            Point p2 = new Point(2, 3);
            Point p3 = new Point(8, 7);
            Point p4 = new Point(9, 8);
            Point p5 = new Point(10, 10);
            Point p6 = new Point(15, 16);
            Point p7 = new Point(16, 18);

            // Create initial clusters (one point per cluster)
            List<Cluster> initialClusters = new List<Cluster>();
            initialClusters.Add(new Cluster(p1));
            initialClusters.Add(new Cluster(p2));
            initialClusters.Add(new Cluster(p3));
            initialClusters.Add(new Cluster(p4));
            initialClusters.Add(new Cluster(p5));
            initialClusters.Add(new Cluster(p6));
            initialClusters.Add(new Cluster(p7));

            Console.WriteLine("Các cụm ban đầu:");
            for (int i = 0; i < initialClusters.Count; i++)
            {
                Console.WriteLine($"Cụm {i + 1}: {initialClusters[i]}");
            }

            // Run hierarchical clustering to get 3 clusters
            int targetClusters = 0;
            Console.Write("\nNhập số cụm muốn phân thành: ");
            while (!int.TryParse(Console.ReadLine(), out targetClusters) || targetClusters < 1 || targetClusters > initialClusters.Count)
            {
                Console.Write("Số cụm không hợp lệ. Vui lòng nhập lại: ");
            }

            List<Cluster> resultClusters = Cluster.HierarchicalClustering(initialClusters, targetClusters);

            Console.WriteLine($"\nKết quả phân cụm thành {targetClusters} cụm sau khi chạy thuật toán Hierachical Clustering:");
            for (int i = 0; i < resultClusters.Count; i++)
            {
                Console.WriteLine($"Cụm {i + 1}: {resultClusters[i]}");
            }

            Console.ReadLine();
        }
    }
}

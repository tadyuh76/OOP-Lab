/* Lab 05 */
/*
    1. Khai báo một interface IVector gồm có các phương thức:
        - Add(IVector vector): IVector
        - Subtract(IVector vector): IVector
        - Multiply(double scalar): IVector
        - Divide(double scalar): IVector
        - Length(): double
        - Normalize(): IVector
        - DotProduct(IVector vector): double
        - CrossProduct(IVector vector): IVector
        để thực thi các chức năng: cộng 2 vector, trừ 2 vector, nhân vector với một số,
        chia vector cho một số, tính độ dài vector, chuẩn hóa vector (chia từng thành phần cho độ dài của vector,
        tích vô hướng, tích có hướng.
    2. Khai báo một lớp Vector2D và Vector3D kế thừa từ IVector và thực thi các phương thức của IVector.
    3. Trong hàm main, tạo ra một List các IVector và triệu gọi hàm tạo của Vector2D và Vector3D ngẫu nhiên cho từng
    phần tử trong List. Sau đó triệu gọi các phương thức tương ứng của IVector cho từng phần tử trong List.
    4. Cho phép Vector2D và Vector3D thực thi bổ sung 2 interface ICloneable và IComparable. Với IComeparable, các vector
    có thể so sánh với nhau dựa trên độ dài của nó (thông qua Length). Sắp xếp các vector trong List của câu 3 theo
    thứ tự tăng dần của độ dài. In ra dưới dạng bảng toạ độ các vector (x, y, z) và độ dài tương ứng.
    5. Bổ sung vào lớp Vector2D một phương thức ConvertToVector3D() để chuyển đổi từ Vector2D sang Vector3D. Trong đó, có
    sử dụng đến phương thức Clone() để tạo ra một bản sao của Vector2D trước khi chuyển đổi.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lab_05
{
    public interface IVector
    {
        IVector Add(IVector vector);
        IVector Subtract(IVector vector);
        IVector Multiply(double scalar);
        IVector Divide(double scalar);
        double Length();
        IVector Normalize(); // Chuẩn hóa
        double DotProduct(IVector vector); // Tích vô hướng
        IVector CrossProduct(IVector vector); // Tích có hướng
        string ShowInfo();
    }

    public class Vector2D : IVector, ICloneable, IComparable<Vector2D>
    {
        public double X { get; set; } = 0.0;
        public double Y { get; set; } = 0.0;

        public Vector2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public object Clone()
        {
            return new Vector2D(this.X, this.Y);
        }

        public int CompareTo(Vector2D other)
        {
            return this.Length().CompareTo(other.Length());
        }

        public Vector3D ConvertToVector3D()
        {
            Vector2D clone = (Vector2D)this.Clone();
            return new Vector3D(clone.X, clone.Y, 0.0);
        }

        public IVector Add(IVector vector)
        {
            Vector2D v = vector as Vector2D;
            return new Vector2D(Math.Round(X + v.X, 2), Math.Round(Y + v.Y, 2));
        }

        public IVector Subtract(IVector vector)
        {
            Vector2D v = vector as Vector2D;
            return new Vector2D(Math.Round(X - v.X, 2), Math.Round(Y - v.Y, 2));
        }

        public IVector Multiply(double scalar)
        {
            return new Vector2D(Math.Round(X * scalar, 2), Math.Round(Y * scalar, 2));
        }

        public IVector Divide(double scalar)
        {
            if (scalar == 0.0) throw new DivideByZeroException();
            return new Vector2D(Math.Round(X / scalar, 2), Math.Round(Y / scalar, 2));
        }

        public double DotProduct(IVector vector)
        {
            Vector2D v = vector as Vector2D;
            return Math.Round(X * v.X + Y * v.Y, 2);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public IVector Normalize()
        {
            double length = Length();
            return new Vector2D(Math.Round((X / length), 2), Math.Round((Y / length), 2));
        }

        public IVector CrossProduct(IVector vector)
        {
            Vector2D v = vector as Vector2D;
            return new Vector2D(Math.Round(X * v.Y, 2), Math.Round(Y * v.X, 2));
        }

        public string ShowInfo()
        {
            return ($"Vector2D: ({X}, {Y})");
        }
    }

    public class Vector3D : IVector, ICloneable, IComparable<Vector3D>
    {
        public double X { get; set; } = 0.0;
        public double Y { get; set; } = 0.0;
        public double Z { get; set; } = 0.0;

        public Vector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public object Clone()
        {
            return new Vector3D(this.X, this.Y, this.Z);
        }

        public int CompareTo(Vector3D other)
        {
            return this.Length().CompareTo(other.Length());
        }

        public IVector Add(IVector vector)
        {
            Vector3D v = vector as Vector3D;
            return new Vector3D(Math.Round(X + v.X, 2), Math.Round(Y + v.Y, 2), Math.Round(Z + v.Z, 2));
        }

        public IVector Subtract(IVector vector)
        {
            Vector3D v = vector as Vector3D;
            return new Vector3D(Math.Round(X - v.X, 2), Math.Round(Y - v.Y, 2), Math.Round(Z - v.Z, 2));
        }

        public IVector Multiply(double scalar)
        {
            return new Vector3D(Math.Round(X * scalar, 2), Math.Round(Y * scalar, 2), Math.Round(Z * scalar, 2));
        }

        public IVector Divide(double scalar)
        {
            if (scalar == 0.0) throw new DivideByZeroException();
            return new Vector3D(Math.Round(X / scalar, 2), Math.Round(Y / scalar, 2), Math.Round(Z / scalar, 2));
        }

        public double DotProduct(IVector vector)
        {
            Vector3D v = vector as Vector3D;
            return Math.Round(X * v.X + Y * v.Y + Z * v.Z, 2);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public IVector Normalize()
        {
            double length = Length();
            return new Vector3D(Math.Round((X / length), 2), Math.Round((Y / length), 2), Math.Round((Z / length), 2));
        }

        public IVector CrossProduct(IVector vector)
        {
            Vector3D v = vector as Vector3D;
            return new Vector3D(
                Math.Round(Y * v.Z - Z * v.Y, 2),
                Math.Round(Z * v.X - X * v.Z, 2),
                Math.Round(X * v.Y - Y * v.X, 2)
            );
        }

        public string ShowInfo()
        {
            return ($"Vector3D: ({X}, {Y}, {Z})");
        }
    }

    public class VectorLengthComparer : IComparer<IVector>
    {
        public int Compare(IVector x, IVector y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentException("Không thể so sánh 2 vector 0");
            }

            return x.Length().CompareTo(y.Length());
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            List<IVector> vectors = new List<IVector>();
            Random random = new Random();

            IVector a = new Vector2D(3.0, 4.0);
            IVector b = new Vector3D(2.0, 3.0, 4.0);
            // Tạo ngẫu nhiên các vector 2D và 3D
            for (int i = 0; i < 10; i++)
            {
                if (random.Next(2) == 0) // 50% khả năng tạo Vector2D
                {
                    vectors.Add(new Vector2D(Math.Round(random.NextDouble() * 10, 2), Math.Round(random.NextDouble() * 10, 2)));
                }
                else // 50% khả năng tạo Vector3D
                {
                    vectors.Add(new Vector3D(Math.Round(random.NextDouble() * 10, 2), Math.Round(random.NextDouble() * 10, 2), Math.Round(random.NextDouble() * 10, 2)));
                }
            }
            Console.WriteLine("Vector 2D và 3D mặc định:");
            Console.WriteLine(a.ShowInfo());
            Console.WriteLine(b.ShowInfo());
            Console.WriteLine();

            foreach (IVector vector in vectors)
            {
                Console.WriteLine();
                Console.WriteLine("Thông tin " + vector.ShowInfo());
                //Console.WriteLine($"Vector: {vector}");
                Console.WriteLine($"Chiều dài: {Math.Round(vector.Length(), 2)}");
                Console.WriteLine($"Chuẩn hóa {vector.Normalize().ShowInfo()}");

                // Đổi vector để tính toán tùy theo loại vector được random ra
                IVector otherVector = vector is Vector2D ? a : b;
                if (vector.GetType() == otherVector.GetType())
                {
                    Console.WriteLine("Cộng 2 vector " + vector.Add(otherVector).ShowInfo());
                    Console.WriteLine("Trừ 2 vector " + vector.Subtract(otherVector).ShowInfo());
                    Console.WriteLine("Nhân vector với 2 " + vector.Multiply(2).ShowInfo());
                    Console.WriteLine("Chia vector cho 2 " + vector.Divide(2).ShowInfo());
                    Console.WriteLine($"Tích vô hướng: {vector.DotProduct(otherVector)}");
                    try
                    {
                        Console.WriteLine("Tích có hướng " + vector.CrossProduct(otherVector).ShowInfo());
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (vector is Vector2D)
                    {
                        Vector3D v3D = ((Vector2D)vector).ConvertToVector3D();
                        Console.WriteLine("Chuyển đổi Vector2D sang " + v3D.ShowInfo());
                    }
                }
                else
                {
                    Console.WriteLine("Không thể thực hiện các phép toán trên các vector có loại khác nhau.");
                }
                Console.WriteLine("");
                Console.WriteLine();
            }
            // Sắp xếp danh sách các vector theo độ dài
            vectors.Sort(new VectorLengthComparer());

            // In ra các vector dưới dạng bảng tọa độ và độ dài
            Console.WriteLine("Bảng tọa độ và độ dài vector đã sắp xếp:");
            Console.WriteLine(" X\t Y\t Z\tĐộ dài");
            foreach (IVector vector in vectors)
            {
                string coordinates = vector is Vector3D ? $"{((Vector3D)vector).X}\t{((Vector3D)vector).Y}\t{((Vector3D)vector).Z}" : $"{((Vector2D)vector).X}\t{((Vector2D)vector).Y}\t ";
                Console.WriteLine($"{coordinates}\t{Math.Round(vector.Length(), 2)}");
            }
            Console.ReadLine();
        }
    }
}
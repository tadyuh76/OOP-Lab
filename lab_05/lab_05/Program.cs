using System.Text;

namespace lab_05
{
    public interface IVector
    {
        IVector Add(IVector vector);
        IVector Subtract(IVector vector);
        IVector Multiply(double scalar);
        IVector Divide(double scalar);
        double Length();
        IVector Normalize();
        double DotProduct(IVector vector);
        IVector CrossProduct(IVector vector);
    }

    public class Vector2D : IVector, ICloneable, IComparable
    {
        public double X { get; set; }
        public double Y { get; set; }


        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public IVector Add(IVector vector)
        {
            Vector2D v = (Vector2D)vector;
            return new Vector2D(X + v.X, Y + v.Y);
        }

        public IVector Subtract(IVector vector)
        {
            Vector2D v = (Vector2D)vector;
            return new Vector2D(X - v.X, Y - v.Y);
        }

        public IVector Multiply(double scalar)
        {
            return new Vector2D(X * scalar, Y * scalar);
        }

        public IVector Divide(double scalar)
        {
            return new Vector2D(X / scalar, Y / scalar);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public IVector Normalize()
        {
            double length = Length();
            return new Vector2D(X / length, Y / length);
        }

        public double DotProduct(IVector vector)
        {
            Vector2D v = (Vector2D)vector;
            return X * v.X + Y * v.Y;
        }

        public IVector CrossProduct(IVector vector)
        {
            throw new NotImplementedException("Vector 2D không có tích có hướng.");
        }

        public object Clone()
        {
            return new Vector2D(X, Y);
        }

        public int CompareTo(object obj)
        {
            Vector2D v = (Vector2D)obj;
            return Length().CompareTo(v.Length());
        }

        public Vector3D ConvertToVector3D()
        {
            Vector2D clone = (Vector2D)Clone();
            return new Vector3D(clone.X, clone.Y, 0);
        }
    }

    public class Vector3D : IVector, ICloneable, IComparable
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public IVector Add(IVector vector)
        {
            Vector3D v = (Vector3D)vector;
            return new Vector3D(X + v.X, Y + v.Y, Z + v.Z);
        }

        public IVector Subtract(IVector vector)
        {
            Vector3D v = (Vector3D)vector;
            return new Vector3D(X - v.X, Y - v.Y, Z - v.Z);
        }

        public IVector Multiply(double scalar)
        {
            return new Vector3D(X * scalar, Y * scalar, Z * scalar);
        }

        public IVector Divide(double scalar)
        {
            return new Vector3D(X / scalar, Y / scalar, Z / scalar);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public IVector Normalize()
        {
            double length = Length();
            return new Vector3D(X / length, Y / length, Z / length);
        }

        public double DotProduct(IVector vector)
        {
            Vector3D v = (Vector3D)vector;
            return X * v.X + Y * v.Y + Z * v.Z;
        }

        public IVector CrossProduct(IVector vector)
        {
            Vector3D v = (Vector3D)vector;
            return new Vector3D(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        }

        public object Clone()
        {
            return new Vector3D(X, Y, Z);
        }

        public int CompareTo(object obj)
        {
            Vector3D v = (Vector3D)obj;
            return Length().CompareTo(v.Length());
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Random rand = new Random();
            List<IVector> vectors = new List<IVector>();

            for (int i = 0; i < 10; i++)
            {
                if (rand.Next(2) == 0)
                    vectors.Add(new Vector2D(rand.NextDouble() * 10, rand.NextDouble() * 10));
                else
                    vectors.Add(new Vector3D(rand.NextDouble() * 10, rand.NextDouble() * 10, rand.NextDouble() * 10));
            }

            vectors.Sort((a, b) => a.Length().CompareTo(b.Length()));


            foreach (IVector vector in vectors)
            {
                if (vector is Vector2D v2)
                    Console.WriteLine($"({v2.X:F2}  {v2.Y:F2})  -  Length: {v2.Length():F2}");
                else if (vector is Vector3D v3)
                    Console.WriteLine($"({v3.X:F2}  {v3.Y:F2}  {v3.Z:F2})  -  Length: {v3.Length():F2}");
            }
        }
    }

}

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
        tích vô hướng, tích có hướng.)
    2. Khai báo một lớp Vector2D và Vector3D kế thừa từ IVector và thực thi các phương thức của IVector.
    3. Trong hàm main, tạo ra một List các IVector và triệu gọi hàm tạo của Vector2D và Vector3D ngẫu nhiên cho từng
    phần tử trong List. Sau đó triệu gọi các phương thức tương ứng của IVector cho từng phần tử trong List.
    4. Cho phép Vector2D và Vector3D thực thi bổ sung 2 interface ICloneable và IComparable. Với IComeparable, các vector
    có thể so sánh với nhau dựa trên độ dài của nó (thông qua Length). Sắp xếp các vector trong List của câu 3 theo
    thứ tự tăng dần của độ dài. In ra dưới dạng bảng toạ độ các vector (x, y, z) và độ dài tương ứng.
    5. Bổ sung vào lớp Vector2D một phương thức ConvertToVector3D() để chuyển đổi từ Vector2D sang Vector3D. Trong đó, có
    sử dụng đến phương thức Clone() để tạo ra một bản sao của Vector2D trước khi chuyển đổi.
*/
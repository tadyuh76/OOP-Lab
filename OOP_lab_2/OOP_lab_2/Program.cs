using System.Text;

namespace OOP_lab_2
{
    /*
    LAB 02
    Cho một lớp Vector2D gồm 2 dữ liệu thành viên x, y
    kiểu float đặc trưng cho toạ độ của vector 2 chiều.
    1. Xây dựng lớp Vector2D với các fields nói trên.
    2. Bổ sung getter và setter cho 2 fields nói trên.
    3. Khai báo constructor không tham số và có tham số.
    4. Khai báo phương thức Print() để in ra thông tin
    của vector 2D dưới dạng: Vector2D<x: 1.2, y: 3.4>
    5. Khai báo phương thức kiểm tra 2 vector trực giao.
    6. Khai báo phương thức tính độ dài của vector.
    7. Khai báo phương thức xác định góc (theo radian)
    giữa 2 vector.
    Trong chương trình chính: tạo ra một mảng
    (List, ArrayList hay bất kì cấu trúc collection nào, 
    sau đó kiểm tra tất cả các hàm chức năng
    */

    class Vector2D
    {
        private float x;
        private float y;

        // Constructor không tham số
        public Vector2D()
        {
            x = 0;
            y = 0;
        }

        // Constructor có tham số
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        // Getter và Setter
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }
        // Phương thức in thông tin vector
        public void Print()
        {
            Console.WriteLine($"Vector2D<x: {x}, y: {y}>");
        }

        // Kiểm tra hai vector có trực giao hay không
        public bool IsOrthogonal(Vector2D other)
        {
            return (this.x * other.x + this.y * other.y) == 0;
        }

        // Tính độ dài của vector
        public double GetLength()
        {
            return Math.Sqrt(x * x + y * y);
        }

        // Tính góc giữa hai vector (đơn vị radian)
        public double AngleBetween(Vector2D other)
        {
            double dotProduct = this.x * other.x + this.y * other.y;
            double mag1 = this.GetLength();
            double mag2 = other.GetLength();
            if (mag1 == 0 || mag2 == 0) return -1;
            return Math.Acos(dotProduct / (mag1 * mag2));
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<Vector2D> vectors = new List<Vector2D>
            {
                new Vector2D(1, 2),
                new Vector2D(3, 4),
                new Vector2D(-2, 1),
                new Vector2D(0, 0)
            };

            // In thông tin của tất cả vector
            foreach (Vector2D v in vectors)
            {
                v.Print();
            }

            // Kiểm tra vector trực giao
            Console.WriteLine("\nKiểm tra vector trực giao:");
            for (int i = 0; i < vectors.Count; i++)
            {
                for (int j = i + 1; j < vectors.Count; j++)
                {
                    if (vectors[i].IsOrthogonal(vectors[j]))
                    {
                        Console.WriteLine($"Vector {i} và Vector {j} trực giao.");
                    }
                }
            }

            // Tính độ dài các vector
            Console.WriteLine("\nĐộ dài của các vector:");
            foreach (Vector2D v in vectors)
            {
                Console.WriteLine($"Vector ({v.X}, {v.Y}) có độ dài: {v.GetLength()}");
            }

            // Tính góc giữa các vector
            Console.WriteLine("\nGóc giữa các vector:");
            for (int i = 0; i < vectors.Count; i++)
            {
                for (int j = i + 1; j < vectors.Count; j++)
                {

                    double angle = vectors[i].AngleBetween(vectors[j]);
                    if (angle == -1)
                    {
                        Console.WriteLine($"Không thể tính góc giữa 2 vector {i} và {j}.");
                    }
                    else { 
                        Console.WriteLine($"Góc giữa Vector {i} và Vector {j}: {angle} rad");
                    }
                   
                }
            }
        }
    }
}

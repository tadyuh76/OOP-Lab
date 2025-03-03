using System.Text;

namespace lab_04
{
    /* Lab 04
    Một lớp AVector có các phương thức:
    - ShowInfo: hiển thị thông tin của lớp
    - Add: cộng 2 AVector
    - Sub: trừ 2 AVector
    - Mul: nhân 2 AVector
    - Div: chia 2 AVector // không thể chia 2 vector với nhau => chia cho scalar
    - Dot: tích vô hướng 2 AVector
    - Module: độ dài của AVector
    - Angle: góc giữa 2 AVector
    Tất cả cả các phương thức ở trên đều đặc tả dưới dạng lớp trừu tượng abstract.

    Khai báo 2 lớp Vector2D và Vector3D kế thừa từ AVector. Cài đặt các phương thức ở lớp cha.
    Trong đó, lớp Vector2D chứa hai thuộc tính x và y (float); lớp Vector3D chứa ba thuộc tính
    Bổ sung các phương thức cần thiết khác cho mỗi lớp như constructor, getter, setter nếu cần

    Trong hàm main, tạo ra một List các Vector hỗn hợp (Vector2D và Vector3D).
    Thực hiện tất cả các phương thức đã được tạo ra giữa các phần tử trong List.
    */

    abstract class AVector
    {
        public abstract void ShowInfo();
        public abstract AVector Add(AVector v);
        public abstract AVector Sub(AVector v);
        public abstract AVector Mul(AVector v);
        public abstract AVector Div(float scalar);
        public abstract float Dot(AVector v);
        public abstract float Module();
        public abstract float Angle(AVector v);
    }

    class Vector2D : AVector
    {
        private float x, y;

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

        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Vector2D: ({X}, {Y})");
        }

        public override AVector Add(AVector v)
        {
            Vector2D vec = (Vector2D)v;
            return new Vector2D(X + vec.X, Y + vec.Y);
        }

        public override AVector Sub(AVector v)
        {
            Vector2D vec = (Vector2D)v;
            return new Vector2D(X - vec.X, Y - vec.Y);
        }

        public override AVector Mul(AVector v)
        {
            throw new NotImplementedException("Vector 2 chiều không có tích có hướng.");
        }

        public override AVector Div(float scalar)
        {
            if (scalar == 0) throw new DivideByZeroException("Không thể chia cho 0!");
            return new Vector2D(X / scalar, Y / scalar);
        }

        public override float Dot(AVector v)
        {
            Vector2D vec = (Vector2D)v;
            return X * vec.X + Y * vec.Y;
        }

        public override float Module()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public override float Angle(AVector v)
        {
            Vector2D vec = (Vector2D)v;
            float dotProduct = Dot(vec);
            float mod1 = Module();
            float mod2 = vec.Module();
            if (mod1 == 0 || mod2 == 0) throw new InvalidOperationException("Không thể tính góc với vector không");
            return (float)Math.Acos(dotProduct / (mod1 * mod2));
        }
    }

    class Vector3D : AVector
    {
        private float x, y, z;

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

        public float Z
        {
            get { return z; }
            set { z = value; }
        }


        public Vector3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Vector3D: ({X}, {Y}, {Z})");
        }

        public override AVector Add(AVector v)
        {
            Vector3D vec = (Vector3D)v;
            return new Vector3D(X + vec.X, Y + vec.Y, Z + vec.Z);
        }

        public override AVector Sub(AVector v)
        {
            Vector3D vec = (Vector3D)v;
            return new Vector3D(X - vec.X, Y - vec.Y, Z - vec.Z);
        }

        public override AVector Mul(AVector v)
        {
            Vector3D vec = (Vector3D)v;
            float cx = Y * vec.Z - Z * vec.Y;
            float cy = Z * vec.X - X * vec.Z;
            float cz = X * vec.Y - Y * vec.X;
            return new Vector3D(cx, cy, cz);
        }

        public override AVector Div(float scalar)
        {
            if (scalar == 0) throw new DivideByZeroException("Không thể chia cho 0!");
            return new Vector3D(X / scalar, Y / scalar, Z / scalar);
        }

        public override float Dot(AVector v)
        {
            Vector3D vec = (Vector3D)v;
            return X * vec.X + Y * vec.Y + Z * vec.Z;
        }

        public override float Module()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public override float Angle(AVector v)
        {
            Vector3D vec = (Vector3D)v;
            float dotProduct = Dot(vec);
            float mod1 = Module();
            float mod2 = vec.Module();
            if (mod1 == 0 || mod2 == 0) throw new InvalidOperationException("Không thể tính góc với vector không");
            return (float)Math.Acos(dotProduct / (mod1 * mod2));
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<AVector> vectors = new List<AVector>
            {
                new Vector2D(3, 4),
                new Vector2D(1, 2),
                new Vector3D(1, 2, 3),
                new Vector3D(4, 5, 6)
            };

            foreach (AVector vec in vectors)
            {
                vec.ShowInfo();
            }

            Console.WriteLine("\nTính toán với các vector:");
            Vector2D v1 = (Vector2D)vectors[0];
            Vector2D v2 = (Vector2D)vectors[1];
            Vector3D v3 = (Vector3D)vectors[2];
            Vector3D v4 = (Vector3D)vectors[3];

            Console.WriteLine("Toán tử trên v1 và v2:");
            try
            {
                Console.WriteLine($"- Dot: {v1.Dot(v3)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Console.WriteLine($"- Angle: {v1.Angle(v2)} rad");
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"- v1 Module: {v1.Module()}");
            Console.WriteLine($"- v2 Module: {v2.Module()}");
            Console.WriteLine("Add:");
            try
            {
                v1.Add(v2).ShowInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Sub:");
            try
            {
                v1.Sub(v3).ShowInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Mul:");
            try
            {
                v1.Mul(v2).ShowInfo();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Div:");
            v1.Div(2).ShowInfo();



            Console.WriteLine("\nToán tử trên v3 và v4:");
            try
            {
                Console.WriteLine($"- Dot: {v3.Dot(v4)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Console.WriteLine($"- Angle: {v3.Angle(v4)} rad");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"- v3 Module: {v3.Module()}");
            Console.WriteLine($"- v4 Module: {v4.Module()}");
            Console.WriteLine("Add:");
            try
            {
                v3.Add(v1).ShowInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Sub:");
            try
            {
                v3.Sub(v4).ShowInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Mul:");
            try
            {
                v3.Mul(v2).ShowInfo();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Div:");
            v3.Div(2).ShowInfo();
        }
    }
}

using System.Text;

namespace OOP_lab_3
{
    /*
    LAB 03: Lớp số phức
    Một lớp số phức gồm có 2 thuộc tính: phần thực (real) và phần ảo (imaginary).
    Hãy khai báo một lớp số phức với các yêu cầu sau:
    - Có 2 phương thức getter và setter cho mỗi field
    - Có 3 dạng constructor: không tham số, có 2 tham số và copy constructor
    - Khai báo các phép toán cộng, trừ, nhân, chia 2 số phức    
    - Khai báo phương thức tính module của số phức
    - Khai báo phương thức tính argument của số phức
    - Khai báo phương thức cộng số phức với một số thực, sử dụng default params (tối đa 3 số thực)
    - Khai báo phương thức cộng nhiều số phức, sử dụng rest params
    (Lưu ý: phương thức cộng 2 số phức và phương thức cộng số phức với một số thực và phương thức cộng các số phức phải
    sử dụng method overloading.)
    Hàm Main: test thử các phương thức nói trên. Lưu ý: tạo một mảng các số phức để thực thi kết quả.
    */

    class ComplexNumber
    {
        private double real;
        private double imaginary;

        // Constructor không tham số
        public ComplexNumber()
        {
            real = 0;
            imaginary = 0;
        }

        // Constructor có tham số
        public ComplexNumber(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        // Copy constructor
        public ComplexNumber(ComplexNumber other)
        {
            this.real = other.real;
            this.imaginary = other.imaginary;
        }

        // Getter và Setter
        public double Real
        {
            get { return real; }
            set { real = value; }
        }

        public double Imaginary
        {
            get { return imaginary; }
            set { imaginary = value; }
        }

        // Phép cộng hai số phức
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real + b.real, a.imaginary + b.imaginary);
        }

        // Phép trừ hai số phức
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real - b.real, a.imaginary - b.imaginary);
        }

        // Phép nhân hai số phức
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(
                a.real * b.real - a.imaginary * b.imaginary,
                a.real * b.imaginary + a.imaginary * b.real
            );
        }

        // Phép chia hai số phức
        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double denominator = b.real * b.real + b.imaginary * b.imaginary;
            return new ComplexNumber(
                (a.real * b.real + a.imaginary * b.imaginary) / denominator,
                (a.imaginary * b.real - a.real * b.imaginary) / denominator
            );
        }

        // Tính module của số phức
        public double Magnitude()
        {
            return Math.Sqrt(real * real + imaginary * imaginary);
        }

        // Tính argument của số phức
        public double Argument()
        {
            return Math.Atan2(imaginary, real);
        }

        // Cộng số phức với một số thực
        public ComplexNumber AddReal(double r1 = 0, double r2 = 0, double r3 = 0)
        {
            return new ComplexNumber(real + r1 + r2 + r3, imaginary);
        }

        // Cộng nhiều số phức
        public static ComplexNumber AddMultiple(params ComplexNumber[] numbers)
        {
            double sumReal = 0, sumImaginary = 0;
            foreach (var num in numbers)
            {
                sumReal += num.real;
                sumImaginary += num.imaginary;
            }
            return new ComplexNumber(sumReal, sumImaginary);
        }

        // In thông tin số phức
        public void Print()
        {
            Console.WriteLine($"{real} + {imaginary}i");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            List<ComplexNumber> complexNumbers = new List<ComplexNumber>
            {
                new ComplexNumber(2, 3),
                new ComplexNumber(1, -4),
                new ComplexNumber(-3, 2)
            };

            // In danh sách số phức
            Console.WriteLine("Danh sách số phức:");
            foreach (var c in complexNumbers)
            {
                c.Print();
            }

            // Kiểm tra phép toán
            Console.WriteLine("\nPhép toán cộng hai số phức:");
            (complexNumbers[0] + complexNumbers[1]).Print();

            Console.WriteLine("\nPhép toán trừ hai số phức:");
            (complexNumbers[0] - complexNumbers[1]).Print();

            Console.WriteLine("\nPhép toán nhân hai số phức:");
            (complexNumbers[0] * complexNumbers[2]).Print();

            Console.WriteLine("\nPhép toán chia hai số phức:");
            (complexNumbers[0] / complexNumbers[2]).Print();

            Console.WriteLine("\nModule của số phức đầu tiên:");
            Console.WriteLine(complexNumbers[0].Magnitude());

            Console.WriteLine("\nArgument của số phức đầu tiên:");
            Console.WriteLine(complexNumbers[0].Argument());

            Console.WriteLine("\nCộng số phức với số thực:");
            complexNumbers[0].AddReal(2, 3).Print();

            Console.WriteLine("\nCộng nhiều số phức:");
            ComplexNumber.AddMultiple(complexNumbers.ToArray()).Print();
        }
    }
}
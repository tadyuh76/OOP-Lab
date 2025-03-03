using System.Text;

namespace OOP_lab_3
{
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

        // Phương thức cộng hai số phức
        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(real + other.real, imaginary + other.imaginary);
        }

        // Phương thức trừ hai số phức
        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(real - other.real, imaginary - other.imaginary);
        }

        // Phương thức nhân hai số phức
        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber(
                real * other.real - imaginary * other.imaginary,
                real * other.imaginary + imaginary * other.real
            );
        }

        // Phương thức chia hai số phức
        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.real * other.real + other.imaginary * other.imaginary;
            return new ComplexNumber(
                (real * other.real + imaginary * other.imaginary) / denominator,
                (imaginary * other.real - real * other.imaginary) / denominator
            );
        }

        // Tính module của số phức
        public double Module()
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
            foreach (ComplexNumber num in numbers)
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
            foreach (ComplexNumber c in complexNumbers)
            {
                c.Print();
            }

            // Kiểm tra phép toán
            Console.WriteLine("\nPhép toán cộng hai số phức:");
            complexNumbers[0].Add(complexNumbers[1]).Print();

            Console.WriteLine("\nPhép toán trừ hai số phức:");
            complexNumbers[0].Subtract(complexNumbers[1]).Print();

            Console.WriteLine("\nPhép toán nhân hai số phức:");
            complexNumbers[0].Multiply(complexNumbers[2]).Print();

            Console.WriteLine("\nPhép toán chia hai số phức:");
            complexNumbers[0].Divide(complexNumbers[2]).Print();

            Console.WriteLine("\nModule của số phức đầu tiên:");
            Console.WriteLine(complexNumbers[0].Module());

            Console.WriteLine("\nArgument của số phức đầu tiên:");
            Console.WriteLine(complexNumbers[0].Argument());

            Console.WriteLine("\nCộng số phức với số thực:");
            complexNumbers[0].AddReal(2, 3).Print();

            Console.WriteLine("\nCộng nhiều số phức:");
            ComplexNumber.AddMultiple(complexNumbers.ToArray()).Print();
        }
    }
}

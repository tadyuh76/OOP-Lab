using System.Text;

namespace lab_06
{
    /*
    Một cửa hàng bán xe đạp có các đối tượng sau cần quản lý: 
    Xe đạp thường, xe đạp đua, xe đạp địa hình, với: 
    UsualBicycle (id, color, price, utility),
    SpeedBicycle (id, color, price, speedrate), 
    ClimpBicycle (id, color, price, climprate). Trong đó, id, 
    color, price lần lượt là mã, màu, giá của xe; utility là phụ 
    kiện tích hợp (yes/no), speedrate là số bước tăng tốc, 
    speedclimp là số bước leo dốc.
    1/ Xây dựng một lớp ABicycle dưới dạng lớp abstract
    để tạo bộ khung dùng chung có các phương thức: Print, 
    MakeDeal(id) để in thông tin và giảm giá theo mã.
    2/ Cài đặt các lớp UsualBycycle, SpeedBicycle, ClimpBicycle
    3/ Tạo một lớp Store chứa một List<ABicycle> và tạo 
    ngẫu nhiên 10 xe thuộc ba nhóm nói trên. Lớp Store cung
    cấp các phương thức sau:
    - Tìm kiếm xe theo bước giá (from - to)
    - Sắp xếp một danh sách xe theo tuỳ chọn (tăng/giảm) giá
    4/ Cài đặt chương trình chính Main để thực thi kq. 
    */

    public abstract class ABicycle
    {
        public int id;
        public string color;
        public double price;

        public ABicycle(int id, string color, double price)
        {
            this.id = id;
            this.color = color;
            this.price = price;
        }

        public abstract void Print();
        public abstract void MakeDeal(int id);
    }

    public class PriceAscendingComparer : IComparer<ABicycle>
    {
        public int Compare(ABicycle? x, ABicycle? y)
        {
            if (x == null || y == null) return 0;
            return x.price.CompareTo(y.price);
        }
    }

    public class PriceDescendingComparer : IComparer<ABicycle>
    {
        public int Compare(ABicycle? x, ABicycle? y)
        {
            if (x == null || y == null) return 0;
            return y.price.CompareTo(x.price);
        }
    }


    public class UsualBicycle : ABicycle
    {
        public string utility;

        public UsualBicycle(int id, string color, double price, string utility) : base(id, color, price)
        {
            this.utility = utility;
        }

        public override void Print()
        {
            Console.WriteLine($"UsualBicycle: id = {id}, color = {color}, price = {price}, utility = {utility}");
        }

        public override void MakeDeal(int id)
        {
            // Mặc định Giảm giá 20% 
            if (this.id == id)
            {
                this.price -= 0.2 * this.price;
            }

            // In ra thông tin xe sau giảm giá
            this.Print();
        }
    }

    public class SpeedBicycle : ABicycle
    {
        public int speedrate;

        public SpeedBicycle(int id, string color, double price, int speedrate) : base(id, color, price)
        {
            this.speedrate = speedrate;
        }

        public override void Print()
        {
            Console.WriteLine($"SpeedBicycle: id = {id}, color = {color}, price = {price}, speedrate = {speedrate}");
        }

        public override void MakeDeal(int id)
        {
            // Mặc định Giảm giá 5% 
            if (this.id == id)
            {
                this.price -= 0.05 * this.price;
            }

            // In ra thông tin xe sau giảm giá
            this.Print();
        }
    }

    public class ClimpBicycle : ABicycle
    {
        public int climprate;

        public ClimpBicycle(int id, string color, double price, int climprate) : base(id, color, price)
        {
            this.climprate = climprate;
        }

        public override void Print()
        {
            Console.WriteLine($"ClimpBicycle: id = {id}, color = {color}, price = {price}, climprate = {climprate}");
        }

        public override void MakeDeal(int id)
        {
            // Mặc định Giảm giá 10% 
            if (this.id == id)
            {
                this.price -= 0.1 * this.price;
            }

            // In ra thông tin xe sau giảm giá
            this.Print();
        }
    }


    public class Store
    {
        public List<ABicycle> bicycles;

        public Store()
        {
            bicycles = new List<ABicycle>();
        }

        public void AddBicycle(ABicycle bicycle)
        {
            bicycles.Add(bicycle);
        }

        public void SearchBicycle()
        {
            double from = 0;
            Console.Write("Từ mức giá: ");
            while (!double.TryParse(Console.ReadLine(), out from))
            {
                Console.WriteLine("Nhập sai, mời nhập lại: ");
            }

            double to = 0;
            Console.Write("Đến mức giá: ");
            while (!double.TryParse(Console.ReadLine(), out to))
            {
                Console.WriteLine("Nhập sai, mời nhập lại: ");
            }

            foreach (ABicycle bicycle in bicycles)
            {
                if (bicycle.price >= from && bicycle.price <= to)
                {
                    bicycle.Print();
                }
            }
        }

        public void SortBicycle(bool isAscending)
        {
            if (isAscending)
            {
                bicycles.Sort(new PriceAscendingComparer());
            }
            else
            {
                bicycles.Sort(new PriceDescendingComparer());
            }
        }

        public void PrintAllBicycles()
        {
            foreach (ABicycle bicycle in bicycles)
            {
                bicycle.Print();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            // add 10 random bicycles to the store
            Store store = new Store();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int type = random.Next(3);
                ABicycle bicycle = null!;
                switch (type)
                {
                    case 0:
                        bicycle = new UsualBicycle(i, "Irish Green", random.Next(100, 1000), random.Next(2) == 0 ? "yes" : "no");
                        break;
                    case 1:
                        bicycle = new SpeedBicycle(i, "Racing Yellow", random.Next(100, 1000), random.Next(10, 100));
                        break;
                    case 2:
                        bicycle = new ClimpBicycle(i, "Guards Red", random.Next(100, 1000), random.Next(10, 100));
                        break;
                }
                store.AddBicycle(bicycle!);
            }

            // print all bicycles and perform the methods on Store
            Console.WriteLine("Tất cả xe: ");
            store.PrintAllBicycles();
            Console.WriteLine();

            Console.WriteLine("Tìm xe: ");
            store.SearchBicycle();
            Console.WriteLine();

            Console.WriteLine("Sort tăng dần theo giá: ");
            store.SortBicycle(true);
            store.PrintAllBicycles();
            Console.WriteLine();

            Console.WriteLine("Sort giảm dần theo giá: ");
            store.SortBicycle(false);
            store.PrintAllBicycles();
            Console.WriteLine();

            int id = -1;
            Console.Write("Giảm giá cho id: ");
            while (!int.TryParse(Console.ReadLine(), out id)) {
                Console.WriteLine("id phải là một số nguyên.");
            }
            foreach (ABicycle bicycle in store.bicycles) {
                if (bicycle.id == id) {
                    bicycle.MakeDeal(id);
                }
            }
        }
    }
}

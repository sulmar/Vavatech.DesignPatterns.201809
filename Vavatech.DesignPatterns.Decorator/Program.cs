using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.DesignPatterns.Decorator.Models;

namespace Vavatech.DesignPatterns.Decorator
{
	class Program
	{
		static void Main(string[] args)
		{
			Order order = CreateOrder();

            ICanDiscountStrategy discountStrategy = 
                new HappyHoursDiscountStrategy(TimeSpan.FromHours(9.5), TimeSpan.FromHours(17));

            IApplyDiscountStrategy applyDiscountStrategy1 = new CustomerDiscountStrategy(20);
            IApplyDiscountStrategy applyDiscountStrategy2 = new CustomerDiscountStrategy(30);

            IOrderCalculator orderCalculator = 
                new CalculatorDecorator(
                    new CalculatorDecorator(
                        new NoDiscountCalculator(), discountStrategy, applyDiscountStrategy1),
                        discountStrategy, applyDiscountStrategy2);

            orderCalculator.CalculateDiscount(order);

            CompressAndDecompressTest();

			DecoratorTest();
		}

		private static void CompressAndDecompressTest()
		{
			string input = "Hello World!";

			Compress(input, "output.txt");

			string output = Decompress("output.txt");
		}

		private static void DecoratorTest()
		{
			IControl control = new BorderDecorator(ConsoleColor.Blue,
												new BorderDecorator(ConsoleColor.Yellow,
													new Button("Save")));

			control.Draw();
		}

		private static void Compress(string content, string filename)
		{
			Stream stream = new GZipStream(
							  new FileStream(filename, FileMode.Create), CompressionMode.Compress);
			using (stream)
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.WriteLine(content);
			}
		}

		private static string Decompress(string filename)
		{
			Stream stream = new GZipStream(
						   new FileStream(filename, FileMode.Open), CompressionMode.Decompress);

			using(stream)
			using (StreamReader streamReader = new StreamReader(stream))
			{
				string content = streamReader.ReadLine();
				return content;
			}

		}

		//private static void Compress(string content, string filename)
		//{
		//    Stream stream =  new GZipStream(
		//                        new FileStream(filename, FileMode.Create), CompressionMode.Compress);

		//    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(content);

		//    stream.Write(bytes, 0, bytes.Length);

		//    stream.Close();
		//}

		private static Order CreateOrder()
		{
			Customer customer = new Customer("Anna", "Smith");

			Order order = new Order
			{
				OrderDate = DateTime.Parse("2018-09-27 9:30"),
				Customer = customer
			};


			Product product = new Product
			{
				Id = 1,
				Name = "Mouse",
				UnitPrice = 100,
			};

			Product product2 = new Product
			{
				Id = 2,
				Name = "Keyboard",
				UnitPrice = 399m,
			};

			order.AddDetail(product, 5);
			order.AddDetail(product2, 3);
			return order;
		}


	}

	

	public interface IControl
	{
		void Draw();
	}

	public class BorderDecorator : Decorator
	{
		private ConsoleColor color;

		public BorderDecorator(ConsoleColor color, IControl control) : base(control)
		{
			this.color = color;
		}


		public override void Draw()
		{
			DrawBorder();

			base.Draw();

			DrawBorder();
		}

		private void DrawBorder()
		{
			Console.BackgroundColor = color;
			Console.WriteLine("Border Decorator");
			Console.ResetColor();
		}
	}

	public abstract class Decorator : IControl
	{
		private IControl control;

		protected Decorator(IControl control)
		{
			this.control = control;
		}

		public virtual void Draw()
		{
			control.Draw();
		}
	}

	public class Button : IControl
	{
		private readonly int width;
		private readonly string label;

		public Button(string label, int width = 10)
		{
			this.width = width;
			this.label = label;
		}

		public void Draw()
		{
			Console.WriteLine($"[ {label} ]");
		}
	}

	public interface ICanDiscountStrategy
	{
		bool CanDiscount(Order order);
	}

	public interface IApplyDiscountStrategy
	{
		void ApplyDiscount(Order order);
	}

	public interface IDiscountStrategy : ICanDiscountStrategy, 
										 IApplyDiscountStrategy
	{
	}



	public class CustomerDiscountStrategy : IApplyDiscountStrategy
    {
		// private readonly string lastChar;
		private readonly decimal discountAmount;

		public CustomerDiscountStrategy(decimal discountAmount)
		{
			//this.lastChar = lastChar;
			this.discountAmount = discountAmount;
		}

		public void ApplyDiscount(Order order)
		{
			if (order.TotalAmount <= discountAmount)
			{
				order.DiscountAmount = order.TotalAmount - 0.99m;
			}
			else
			{
				order.DiscountAmount = discountAmount;
			}
		}

		//public bool CanDiscount(Order order)
		//{
		//	return order.Customer.FirstName.EndsWith(lastChar);
		//}
	}

    


    public class HappyHoursDiscountStrategy : ICanDiscountStrategy
	{
		private readonly TimeSpan beginTime;
		private readonly TimeSpan endTime;

		public HappyHoursDiscountStrategy(TimeSpan beginTime, TimeSpan endTime)
		{
			this.beginTime = beginTime;
			this.endTime = endTime;
		}

		public bool CanDiscount(Order order)
		{
			return order.OrderDate.TimeOfDay >= beginTime
			  && order.OrderDate.TimeOfDay <= endTime;
		}

		//public void ApplyDiscount(Order order)
		//{
		//	order.DiscountAmount = order.TotalAmount * percentage;
		//}

	}

    public class HappyHoursDiscountDecorator : DiscountDecorator
    {
        public HappyHoursDiscountDecorator(IDiscountStrategy strategy) 
            : base(strategy)
        {
        }
    }

    public class CustomerDiscountDecorator : DiscountDecorator
    {
        public CustomerDiscountDecorator(IDiscountStrategy strategy) 
            : base(strategy)
        {
        }
    }

    public abstract class DiscountDecorator : IDiscountStrategy
	{
		private IDiscountStrategy strategy;

		public DiscountDecorator(IDiscountStrategy strategy)
		{
			this.strategy = strategy;
		}

		public virtual void ApplyDiscount(Order order)
		{
			strategy.ApplyDiscount(order);
		}

		public virtual bool CanDiscount(Order order)
		{
			return strategy.CanDiscount(order);
		}

	  
	}
}

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitmqTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			//创建连接工厂
			var factory = new ConnectionFactory
			{
				UserName = "admin",//用户名
				Password = "123456",//密码
				HostName = "192.168.0.115"//rabbitmq ip
			};

			//创建连接
			var connection = factory.CreateConnection();
			//创建通道
			var channel = connection.CreateModel();

			//事件基本消费者
			EventingBasicConsumer c1 = new EventingBasicConsumer(channel);
			//接收到消息事件
			c1.Received += (ch, ea) =>
			{
				var message = Encoding.UTF8.GetString(ea.Body.ToArray());

				Console.WriteLine($"Queue q1 收到消息： {message}");
				//确认该消息已被消费
				channel.BasicAck(ea.DeliveryTag, false);
			};
			//启动消费者 设置为手动应答消息
			channel.BasicConsume("q1", false, c1);

			//事件基本消费者
			EventingBasicConsumer c2= new EventingBasicConsumer(channel);
			//接收到消息事件
			c2.Received += (ch, ea) =>
			{
				var message = Encoding.UTF8.GetString(ea.Body.ToArray());

				Console.WriteLine($"Queue q2 收到消息： {message}");
				//确认该消息已被消费
				channel.BasicAck(ea.DeliveryTag, false);
			};

			//事件基本消费者
			EventingBasicConsumer c3 = new EventingBasicConsumer(channel);
			//接收到消息事件
			c3.Received += (ch, ea) =>
			{
				var message = Encoding.UTF8.GetString(ea.Body.ToArray());

				Console.WriteLine($"Queue q3 收到消息： {message}");
				//确认该消息已被消费
				channel.BasicAck(ea.DeliveryTag, false);
			};

			//启动消费者 设置为手动应答消息
			channel.BasicConsume("q1", false, c1);
			channel.BasicConsume("q2", false, c2);
			channel.BasicConsume("q3", false, c3);

			Console.WriteLine($"消费者已启动");
			Console.ReadLine();
		}
    }
}

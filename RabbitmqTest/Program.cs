using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitmqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

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
            //定义一个Direct类型交换机
            channel.ExchangeDeclare("ex1", ExchangeType.Direct, false, false, null);
            //定义一个队列
            channel.QueueDeclare("q1", false, false, false, null);
            channel.QueueDeclare("q2", false, false, false, null);
            channel.QueueDeclare("q3", false, false, false, null);

            //将队列绑定到交换机
            channel.QueueBind("q1", "ex1", "k1", null);
            channel.QueueBind("q2", "ex1", "k1", null);

            channel.QueueBind("q3", "ex1", "k2", null);


            string input;
            do
            {
                input = Console.ReadLine();

                var sendBytes = Encoding.UTF8.GetBytes(input);
                //发布消息
                channel.BasicPublish("ex1", "k1", null, sendBytes);
                channel.BasicPublish("ex1", "k2", null, sendBytes);

            } while (input.Trim().ToLower() != "exit");
            channel.Close();
            connection.Close();

        }
    }
}

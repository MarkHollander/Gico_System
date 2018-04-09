using Gico.Common;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using OtpSharp;
using Gico.CQRS.Service.Interfaces;

namespace Gico.ConsoleApp
{
    class Program
    {
        [Flags]
        public enum TypeEnum
        {
            IsEmployee = 1,
            IsCustomer = 2,
            IsCustomerVip1 = 4,
            IsCustomerVip2 = 8,
        }
        static void Main(string[] args)
        {
            


            var t = Console.ReadLine();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private bool TryParseNumeric(string input, out long number)
        {
            return long.TryParse(input, out number);
        }

        [HttpGet("add")]
        public IActionResult Add(string num1, string num2)
        {
            if (!TryParseNumeric(num1, out var n1) || !TryParseNumeric(num2, out var n2))
                return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                checked
                {
                    return Ok(n1 + n2);
                }
            }
            catch (OverflowException ex)
            {
                // Log the error
                Console.Error.WriteLine(ex);
                return BadRequest("An error occurred while performing the addition.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(string num1, string num2)
        {
            if (!TryParseNumeric(num1, out var n1) || !TryParseNumeric(num2, out var n2))
                return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                checked
                {
                    return Ok(n1 - n2);
                }
            }
            catch (OverflowException ex)
            {
                // Log the error
                Console.Error.WriteLine(ex);
                return BadRequest("An error occurred while performing the subtraction.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(string num1, string num2)
        {
            if (!TryParseNumeric(num1, out var n1) || !TryParseNumeric(num2, out var n2))
                return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                checked
                {
                    BigInteger result = n1 * n2;
                    return Ok(result);
                }
            }
            catch (OverflowException ex)
            {
                // Log the error
                Console.Error.WriteLine(ex);
                return BadRequest("An error occurred while performing the multiplication.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(string num1, string num2)
        {
            if (!TryParseNumeric(num1, out var n1) || !TryParseNumeric(num2, out var n2))
                return BadRequest("Invalid input. Only numeric values are allowed.");

            try
            {
                if (n2 == 0)
                    return BadRequest("Division by zero is not allowed.");

                return Ok(n1 / n2);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.Error.WriteLine(ex);
                return BadRequest("An error occurred while performing the division.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(string n)
        {
            if (!TryParseNumeric(n, out var num) || num < 0)
                return BadRequest("Invalid input. Only positive numeric values are allowed.");

            try
            {
                if (num > 20)
                {
                    return BadRequest("Input too large. Please try a smaller number.");
                }

                BigInteger result = 1;
                for (long i = 1; i <= num; i++)
                {
                    result *= i;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.Error.WriteLine(ex);
                return BadRequest("An error occurred while calculating the factorial.");
            }
        }
    }
}
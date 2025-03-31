using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private bool IsNumeric(string inp)
        {
            return double.TryParse(inp, out _);
        }

        [HttpGet("add")]
        public IActionResult Add(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");
            if (num1.Length > 10 || num2.Length > 10) return BadRequest("Input too large. Please provide smaller values.");

            try
            {
                if (int.TryParse(num1, out int n1) && int.TryParse(num2, out int n2))
                {
                    checked
                    {
                        return Ok(n1 + n2);
                    }
                }

                return BadRequest("Invalid numeric format.");
            }
            catch (OverflowException)
            {
                return BadRequest("An error occurred while performing the addition.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");
            if (num1.Length > 10 || num2.Length > 10) return BadRequest("Input too large. Please provide smaller values.");

            try
            {
                if (int.TryParse(num1, out int n1) && int.TryParse(num2, out int n2))
                {
                    checked
                    {
                        return Ok(n1 - n2);
                    }
                }

                return BadRequest("Invalid numeric format.");
            }
            catch (OverflowException)
            {
                return BadRequest("An error occurred while performing the subtraction.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");
            if (num1.Length > 10 || num2.Length > 10) return BadRequest("Input too large. Please provide smaller values.");

            try
            {
                if (int.TryParse(num1, out int n1) && int.TryParse(num2, out int n2))
                {
                    checked
                    {
                        long result = n1 * n2;
                        return Ok(result);
                    }
                }

                return BadRequest("Invalid numeric format.");
            }
            catch (OverflowException)
            {
                return BadRequest("An error occurred while performing the multiplication.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(string num1, string num2)
        {
            if (!IsNumeric(num1) || !IsNumeric(num2)) return BadRequest("Invalid input. Only numeric values are allowed.");
            if (num1.Length > 10 || num2.Length > 10) return BadRequest("Input too large. Please provide smaller values.");

            try
            {
                if (int.TryParse(num1, out int n1) && int.TryParse(num2, out int n2))
                {
                    if (n2 == 0) return BadRequest("Division by zero is not allowed.");
                    return Ok(n1 / n2);
                }

                return BadRequest("Invalid numeric format.");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while performing the division.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(string n)
        {
            if (!IsNumeric(n) || long.Parse(n) < 0) return BadRequest("Invalid input. Only positive numeric values are allowed.");

            try
            {
                if (int.TryParse(n, out int num) && num <= 15)
                {
                    long result = 1;
                    for (int i = 1; i <= num; i++)
                    {
                        result *= i;
                    }

                    return Ok(result);
                }

                return BadRequest("Input too large or invalid.");
            }
            catch (Exception)
            {
                return BadRequest("An error occurred while calculating the factorial.");
            }
        }
    }
}

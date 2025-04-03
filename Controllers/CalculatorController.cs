
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int FactorialMaxInput = 100;

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            try
            {
                return Ok(checked(num1 + num2));
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while calculating the sum.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            try
            {
                return Ok(checked(num1 - num2));
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while calculating the subtraction.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            try
            {
                long result = checked((long)num1 * (long)num2);
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while calculating the multiplication.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num2 == 0) return BadRequest("Division by zero is not allowed.");

            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero error occurred.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Negative numbers are not allowed.");
            if (n > FactorialMaxInput) return BadRequest($"Input exceeds maximum limit of {FactorialMaxInput}.");

            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while calculating the factorial.");
            }
        }

        private long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result = checked(result * i);
            }
            return result;
        }
    }
}
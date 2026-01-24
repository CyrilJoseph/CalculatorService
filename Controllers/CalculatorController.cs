using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int MaxInputLimit = 1000000;
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            if (Math.Abs((long)num1 + (long)num2) > int.MaxValue) return BadRequest("Result is out of bounds");
            if (num1 > MaxInputLimit || num2 > MaxInputLimit) return BadRequest("Input limit exceeded");
            try
            {
                return Ok(num1 + num2);
            }
            catch (Exception ex)
            {
                // Log error if necessary
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (num1 > MaxInputLimit || num2 > MaxInputLimit) return BadRequest("Input limit exceeded");
            try
            {
                return Ok(num1 - num2);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (num1 > MaxInputLimit || num2 > MaxInputLimit) return BadRequest("Input limit exceeded");
            try
            {
                long result = (long)num1 * (long)num2;
                if (result > int.MaxValue || result < int.MinValue) return BadRequest("Result is out of bounds");
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (num1 > MaxInputLimit || num2 > MaxInputLimit) return BadRequest("Input limit exceeded");
            if (num2 == 0) return BadRequest("Cannot divide by zero");
            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Input must be non-negative");
            if (n > 20) return BadRequest("Input too large; may cause overflow or excessive recursion");
            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (StackOverflowException)
            {
                return BadRequest("Input too large for factorial calculation");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        private long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}

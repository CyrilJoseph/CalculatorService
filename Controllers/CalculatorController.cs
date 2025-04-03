
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int MaxFactorialInput = 20;  // Limit to prevent stack overflow and excessive computation

        [HttpGet("add")]
        public IActionResult Add(int? num1, int? num2)
        {
            if (num1 == null || num2 == null) return BadRequest("Both num1 and num2 are required and must be valid integers.");

            try
            {
                return Ok(num1.Value + num2.Value);
            }
            catch (OverflowException)
            {
                return BadRequest("The addition result is too large.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int? num1, int? num2)
        {
            if (num1 == null || num2 == null) return BadRequest("Both num1 and num2 are required and must be valid integers.");

            return Ok(num1.Value - num2.Value);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int? num1, int? num2)
        {
            if (num1 == null || num2 == null) return BadRequest("Both num1 and num2 are required and must be valid integers.");

            try
            {
                checked
                {
                    long result = (long)num1.Value * (long)num2.Value;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("The multiplication result is too large.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int? num1, int? num2)
        {
            if (num1 == null || num2 == null) return BadRequest("Both num1 and num2 are required and must be valid integers.");
            if (num2.Value == 0) return BadRequest("Cannot divide by zero.");

            try
            {
                return Ok(num1.Value / num2.Value);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int? n)
        {
            if (n == null) return BadRequest("Input is required and must be a valid integer.");
            if (n.Value < 0) return BadRequest("Input must be a non-negative integer.");
            if (n.Value > MaxFactorialInput) return BadRequest($"Input exceeds maximum allowed value {MaxFactorialInput}.");

            try
            {
                return Ok(CalculateFactorial(n.Value));
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        private long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
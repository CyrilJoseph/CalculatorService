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
        private const int MaxValue = 1000000; // Arbitrary but reasonable limit to prevent overflow

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Numbers cannot be zero.");
            if (Math.Abs((long)num1 + num2) > MaxValue)
                return BadRequest($"Result exceeds maximum allowable value of {MaxValue}.");

            return Ok(num1 + num2);
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Numbers cannot be zero.");
            if (Math.Abs((long)num1 - num2) > MaxValue)
                return BadRequest($"Result exceeds maximum allowable value of {MaxValue}.");

            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Numbers cannot be zero.");
            long result = (long)num1 * (long)num2;
            if (Math.Abs(result) > MaxValue)
                return BadRequest($"Result exceeds maximum allowable value of {MaxValue}.");

            return Ok(result);
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0 || num2 == 0) return BadRequest("Numbers cannot be zero.");
            if (num2 == 0) return BadRequest("Cannot divide by zero.");

            return Ok((double)num1 / num2);
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Factorial is not defined for negative numbers.");
            if (n > 20) // Prevent large computations causing resource exhaustion
                return BadRequest("Input exceeds maximum allowable value for factorial computation.");

            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            return Ok(result);
        }
    }
}

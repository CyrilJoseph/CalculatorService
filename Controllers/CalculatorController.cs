
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
        private const int MAX_INPUT = 1000000; // Set specific thresholds to prevent abuse

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            if (Math.Abs(num1) > MAX_INPUT || Math.Abs(num2) > MAX_INPUT) return BadRequest("Input is too large.");

            try
            {
                return Ok(num1 + num2);
            }
            catch (Exception ex) // Improved error handling
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (Math.Abs(num1) > MAX_INPUT || Math.Abs(num2) > MAX_INPUT) return BadRequest("Input is too large.");

            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (Math.Abs(num1) > MAX_INPUT || Math.Abs(num2) > MAX_INPUT) return BadRequest("Input is too large.");

            long result = (long)num1 * (long)num2;
            if (result > long.MaxValue || result < long.MinValue) return BadRequest("Result is out of range.");
            return Ok(result);
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            if (Math.Abs(num1) > MAX_INPUT || Math.Abs(num2) > MAX_INPUT) return BadRequest("Input is too large.");
            if (num2 == 0) return BadRequest("Cannot divide by zero.");

            try
            {
                return Ok(num1 / num2);
            }
            catch (Exception ex) // Improved error handling
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0 || n > 12) return BadRequest("Input must be between 0 and 12.");

            try
            {
                int factorial = 1;
                for (int i = 1; i <= n; i++)
                {
                    checked
                    {
                        factorial *= i; // Handle overflow directly
                    }
                }
                return Ok(factorial);
            }
            catch (OverflowException ex)
            {
                return StatusCode(500, "Calculation caused a data overflow");
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}

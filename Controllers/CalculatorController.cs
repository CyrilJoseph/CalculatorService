using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int MaxInput = 1000000; // Define a maximum allowed input number

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (num1 <= 0 || num1 > MaxInput) return BadRequest("Num1 is required and must be between 1 and 1000000.");
            if (num2 <= 0 || num2 > MaxInput) return BadRequest("Num2 is required and must be between 1 and 1000000.");

            try
            {
                checked // Prevent integer overflow
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Inputs are too large.");
            }
            catch (Exception ex)
            {
                // Specific error handling
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 <= 0 || num1 > MaxInput) return BadRequest("Num1 is required and must be between 1 and 1000000.");
            if (num2 <= 0 || num2 > MaxInput) return BadRequest("Num2 is required and must be between 1 and 1000000.");

            try
            {
                return Ok(num1 - num2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 <= 0 || num1 > MaxInput) return BadRequest("Num1 is required and must be between 1 and 1000000.");
            if (num2 <= 0 || num2 > MaxInput) return BadRequest("Num2 is required and must be between 1 and 1000000.");

            try
            {
                checked // Prevent integer overflow
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Result is too large.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 <= 0 || num1 > MaxInput) return BadRequest("Num1 is required and must be between 1 and 1000000.");
            if (num2 <= 0 || num2 > MaxInput) return BadRequest("Num2 is required and must be between 1 and 1000000.");

            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n <= 0 || n > 20) return BadRequest("n must be between 1 and 20 to prevent stack overflow.");

            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
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
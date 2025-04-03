
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
        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            // Add input validation
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between -10000 and 10000.");
            
            try
            {
                return Ok(num1 + num2);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            // Add input validation
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between -10000 and 10000.");
            
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            // Add input validation
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between -10000 and 10000.");
            
            try
            {
                long result = checked((long)num1 * num2);
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Calculation result exceeds allowed range.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            // Add input validation
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between -10000 and 10000.");
            if (num2 == 0) return BadRequest("Division by zero is not allowed.");

            return Ok(num1 / num2);
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0 || n > 20) // Restrict to prevent overflow
                return BadRequest("Input should be non-negative and not greater than 20.");

            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred during factorial calculation.");
            }
        }

        private long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result = checked(result * i); // Prevent overflow
            }
            return result;
        }

        private bool IsValidInput(int num)
        {
            return num >= -10000 && num <= 10000;
        }
    }
}

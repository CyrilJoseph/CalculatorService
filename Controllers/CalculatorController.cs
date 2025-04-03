
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
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Invalid inputs provided.");

            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while adding the numbers.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Invalid inputs provided.");

            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while subtracting the numbers.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Invalid inputs provided.");

            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred while multiplying the numbers.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Invalid inputs provided.");
            if (num2 == 0) return BadRequest("Division by zero is not allowed.");

            try
            {
                return Ok(num1 / num2);
            }
            catch
            {
                return StatusCode(500, "An error occurred during division.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (!IsValidInput(n)) return BadRequest("Input is invalid or out of accepted range.");
            if (n > 20) return BadRequest("Factorial calculation is restricted to prevent overflow.");

            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch
            {
                return StatusCode(500, "An error occurred during calculation.");
            }
        }

        private bool IsValidInput(int value)
        {
            return value >= int.MinValue && value <= int.MaxValue;
        }

        private long CalculateFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                checked
                {
                    result *= i;
                }
            }
            return result;
        }
    }
}

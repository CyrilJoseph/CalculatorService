using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int MaxFactorialInput = 20;

        [HttpGet("add")]
        public IActionResult Add([Required] int num1, [Required] int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input format");

            try
            {
                return Ok(new { Result = num1 + num2 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub([Required] int num1, [Required] int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input format");
            return Ok(new { Result = num1 - num2 });
        }

        [HttpGet("multiply")]
        public IActionResult Multiply([Required] int num1, [Required] int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input format");

            try
            {
                long result = (long)num1 * (long)num2;
                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide([Required] int num1, [Required] int num2)
        {
            if (num2 == 0) return BadRequest("Denominator cannot be zero");

            try
            {
                return Ok(new { Result = num1 / num2 });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial([Required] int n)
        {
            if (!ModelState.IsValid || n < 0 || n > MaxFactorialInput)
                return BadRequest($"Input must be between 0 and {MaxFactorialInput}");

            try
            {
                return Ok(new { Result = ComputeFactorial(n) });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private long ComputeFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
                result *= i;

            return result;
        }
    }
}

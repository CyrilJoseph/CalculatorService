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
            if (!ModelState.IsValid || num1 < 0 || num2 < 0)
            {
                return BadRequest("Invalid input. Numbers must be non-negative.");
            }

            try
            {
                var result = checked(num1 + num2); // Prevent overflow
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Addition resulted in overflow.");
            }
            catch (Exception ex) // Logs for debugging purposes
            {
                // Log the exception (hypothetical logging mechanism)
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < 0 || num2 < 0)
            {
                return BadRequest("Invalid input. Numbers must be non-negative.");
            }

            try
            {
                var result = num1 - num2;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid || num1 < 0 || num2 < 0)
            {
                return BadRequest("Invalid input. Numbers must be non-negative.");
            }

            try
            {
                var result = checked((long)num1 * (long)num2); // Prevent overflow
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Multiplication resulted in overflow.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid || num2 == 0)
            {
                return BadRequest("Invalid input. Division by zero is not allowed.");
            }

            try
            {
                var result = num1 / num2;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (!ModelState.IsValid || n < 0 || n > 20) // Upper limit to prevent stack overflow
            {
                return BadRequest("Invalid input. Number must be between 0 and 20.");
            }

            long FactorialLogic(int value)
            {
                long result = 1;
                for (int i = 2; i <= value; i++)
                {
                    result *= i;
                }
                return result;
            }

            try
            {
                var result = FactorialLogic(n);
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Factorial calculation resulted in overflow.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}

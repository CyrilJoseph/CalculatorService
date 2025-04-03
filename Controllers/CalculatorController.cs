
ï»¿using Microsoft.AspNetCore.Http;
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
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0 || num2 == 0) return BadRequest("Inputs must be non-zero");
            
            try
            {
                return Ok(num1 + num2);
            }
            catch
            {
                return StatusCode(500, "Server error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0 || num2 == 0) return BadRequest("Inputs must be non-zero");
            
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0 || num2 == 0) return BadRequest("Inputs must be non-zero");
            
            long result = (long)num1 * (long)num2;
            return Ok(result);
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num2 == 0) return BadRequest("Cannot divide by zero");

            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Cannot divide by zero");
            }
            catch
            {
                return StatusCode(500, "Server error occurred");
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (n < 0) return BadRequest("Input must be non-negative");

            try
            {
                // Base case for recursion
                return Ok(FactorialRecursive(n));
            }
            catch
            {
                return StatusCode(500, "Server error occurred");
            }
        }
        
        private long FactorialRecursive(int n)
        {
            if (n <= 1) return 1;
            return n * FactorialRecursive(n - 1);
        }
    }
}


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
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between 1 and 10000");

            try
            {
                return Ok(num1 + num2);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "An overflow occurred while adding.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between 1 and 10000");
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between 1 and 10000");
            
            try
            {
                long result = checked((long)num1 * (long)num2);
                return Ok(result);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "An overflow occurred while multiplying.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2)) return BadRequest("Inputs must be between 1 and 10000");
            if (num2 == 0) return BadRequest("Cannot divide by zero");

            return Ok((double)num1 / num2);
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (!IsValidInput(n)) return BadRequest("Input must be between 1 and 10000");

            try
            {
                return Ok(IterativeFactorial(n));
            }
            catch (OverflowException)
            {
                return StatusCode(500, "An overflow occurred while calculating factorial.");
            }
        }

        private bool IsValidInput(int input)
        {
            return input >= 1 && input <= 10000;
        }

        private long IterativeFactorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result = checked(result * i);
            }
            return result;
        }
    }
}

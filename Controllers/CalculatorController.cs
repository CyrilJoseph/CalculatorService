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
            // Validate input range (avoid overflow)
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");
            try
            {
                checked {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
            try
            {
                checked {
                    long result = (long)num1 * (long)num2;
                    if (result > int.MaxValue || result < int.MinValue)
                        return BadRequest("Result overflow detected");
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");
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
                return StatusCode(500, "An error occurred");
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            // Prevent negative and too-large numbers (abuse protection)
            if (n < 0) return BadRequest("Input must not be negative");
            if (n > 20) return BadRequest("Input too large; must be 20 or less to prevent overflow and denial of service");
            try
            {
                return Ok(FactorialInternal(n));
            }
            catch (StackOverflowException)
            {
                return BadRequest("Input too large, recursion failed");
            }
            catch (OverflowException)
            {
                return BadRequest("Integer overflow detected");
            }
        }

        private long FactorialInternal(int n)
        {
            if (n <= 1) return 1;
            checked
            {
                return n * FactorialInternal(n - 1);
            }
        }
    }
}


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
        public IActionResult Add(string num1, string num2)
        {
            if (!int.TryParse(num1, out int parsedNum1) || !int.TryParse(num2, out int parsedNum2))
                return BadRequest("Inputs must be valid integers");

            try
            {
                return Ok(parsedNum1 + parsedNum2);
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred in calculation");
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(string num1, string num2)
        {
            if (!int.TryParse(num1, out int parsedNum1) || !int.TryParse(num2, out int parsedNum2))
                return BadRequest("Inputs must be valid integers");

            try
            {
                return Ok(parsedNum1 - parsedNum2);
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred in calculation");
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(string num1, string num2)
        {
            if (!int.TryParse(num1, out int parsedNum1) || !int.TryParse(num2, out int parsedNum2))
                return BadRequest("Inputs must be valid integers");  
            
            try
            {
                long result = (long)parsedNum1 * (long)parsedNum2;
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred in calculation");
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(string num1, string num2)
        {
            if (!int.TryParse(num1, out int parsedNum1) || !int.TryParse(num2, out int parsedNum2) || parsedNum2 == 0)
                return BadRequest("Inputs must be valid integers and divisor cannot be zero");

            try
            {
                return Ok((double)parsedNum1 / parsedNum2);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(string n)
        {
            if (!int.TryParse(n, out int parsedN) || parsedN < 0 || parsedN > 12)  // Restrict to avoid overflow in recursion
                return BadRequest("Input must be a valid non-negative integer less than or equal to 12");

            try
            {
                int Factorial(int number)
                {
                    if (number <= 1) return 1;
                    return number * Factorial(number - 1);
                }

                return Ok(Factorial(parsedN));
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred in calculation");
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
    }
}

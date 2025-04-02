
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
            if (num1 < 0 || num2 < 0) return BadRequest("Numbers must be non-negative");
            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Input values are too large to process");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (num1 < 0 || num2 < 0) return BadRequest("Numbers must be non-negative");
            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 < 0 || num2 < 0) return BadRequest("Numbers must be non-negative");

            try
            {
                long result = checked((long)num1 * (long)num2);
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Input values are too large to process");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 < 0 || num2 <= 0) return BadRequest("Numbers must be non-negative, and num2 cannot be zero");

            return Ok(num1 / num2);
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0) return BadRequest("Input must be non-negative");
            if (n > 20) return BadRequest("Input is too large to safely compute factorial");

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                try
                {
                    result = checked(result * i);
                }
                catch (OverflowException)
                {
                    return BadRequest("Result too large to compute factorial");
                }
            }
            return Ok(result);
        }
    }
}
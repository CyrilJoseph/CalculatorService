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
            if (num1 == 0) return BadRequest("Num1 is required");
            if (num2 == 0) return BadRequest("Num2 is required");

            try
            {
                return Ok(num1 + num2);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "Arithmetic overflow occurred");
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
                return Ok(num1 - num2);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "Arithmetic overflow occurred");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

            try
            {
                long result = checked((long)num1 * (long)num2);
                return Ok(result);
            }
            catch (OverflowException)
            {
                return StatusCode(500, "Arithmetic overflow occurred");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num1 == 0) return BadRequest("Num1 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Num2 is required and cannot be zero");

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
            if (n < 0)
                return BadRequest("Input must be a non-negative integer");

            if (n <= 1)
                return Ok(1);

            try
            {
                return Ok(checked(n * Factorial(n - 1).Value));
            }
            catch (OverflowException)
            {
                return StatusCode(500, "Arithmetic overflow occurred");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}

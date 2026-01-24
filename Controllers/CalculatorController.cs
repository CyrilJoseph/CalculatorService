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
        private const int MaxFactorialInput = 20;
        private const int MinInput = -1000000;
        private const int MaxInput = 1000000;

        private bool IsValidInput(int num)
        {
            return num >= MinInput && num <= MaxInput;
        }

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input values out of valid range.");
            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Addition resulted in an overflow.");
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input values out of valid range.");
            try
            {
                checked
                {
                    return Ok(num1 - num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Subtraction resulted in an overflow.");
            }
            catch
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input values out of valid range.");
            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    if (result < long.MinValue || result > long.MaxValue)
                        throw new OverflowException();
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Multiplication resulted in an overflow.");
            }
            catch
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!IsValidInput(num1) || !IsValidInput(num2))
                return BadRequest("Input values out of valid range.");
            if (num2 == 0)
                return BadRequest("Division by zero is not allowed.");
            try
            {
                return Ok((double)num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero error.");
            }
            catch
            {
                return StatusCode(500, "An error occurred.");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0)
                return BadRequest("Factorial input must be >= 0.");
            if (n > MaxFactorialInput)
                return BadRequest($"Factorial input must be less than or equal to {MaxFactorialInput} for resource protection.");
            try
            {
                return Ok(CalculateFactorial(n));
            }
            catch (OverflowException)
            {
                return BadRequest("Factorial calculation resulted in an overflow.");
            }
            catch
            {
                return StatusCode(500, "An error occurred.");
            }
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

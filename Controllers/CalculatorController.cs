
using Microsoft.AspNetCore.Mvc;
using System.Numerics; // Added for advanced mathematical computations.
namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private const int MaxInputValue = 100000; // Input validation limit.

        [HttpGet("add")]
        public IActionResult Add(int num1, int num2)
        {
            if (!ValidateInput(num1) || !ValidateInput(num2))
            {
                return BadRequest("Inputs must be between -100000 and 100000.");
            }

            try
            {
                return Ok(num1 + num2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ValidateInput(num1) || !ValidateInput(num2))
            {
                return BadRequest("Inputs must be between -100000 and 100000.");
            }

            return Ok(num1 - num2);
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ValidateInput(num1) || !ValidateInput(num2))
            {
                return BadRequest("Inputs must be between -100000 and 100000.");
            }

            try
            {
                BigInteger result = (BigInteger)num1 * num2;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (num2 == 0) return BadRequest("Division by zero is not allowed.");
            if (!ValidateInput(num1) || !ValidateInput(num2))
            {
                return BadRequest("Inputs must be between -100000 and 100000.");
            }

            try
            {
                return Ok(num1 / num2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (n < 0 || n > 50)
            {
                return BadRequest("Input must be a non-negative integer less than or equal to 50.");
            }

            try
            {
                BigInteger result = 1;
                for (int i = 1; i <= n; i++)
                {
                    result *= i;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private bool ValidateInput(int input)
        {
            return input >= -MaxInputValue && input <= MaxInputValue;
        }
    }
}
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
        private IActionResult ValidateInput(string paramName, string value)
        {
            int parsedValue;

            if (!int.TryParse(value, out parsedValue))
            {
                return BadRequest(paramName + " must be a valid integer.");
            }

            return null;
        }

        [HttpGet("add")]
        public IActionResult Add(string num1, string num2)
        {
            var validationResult1 = ValidateInput(nameof(num1), num1);
            var validationResult2 = ValidateInput(nameof(num2), num2);

            if (validationResult1 != null) return validationResult1;
            if (validationResult2 != null) return validationResult2;

            try
            {
                int operand1 = int.Parse(num1);
                int operand2 = int.Parse(num2);

                return Ok(operand1 + operand2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(string num1, string num2)
        {
            var validationResult1 = ValidateInput(nameof(num1), num1);
            var validationResult2 = ValidateInput(nameof(num2), num2);

            if (validationResult1 != null) return validationResult1;
            if (validationResult2 != null) return validationResult2;

            try
            {
                int operand1 = int.Parse(num1);
                int operand2 = int.Parse(num2);

                return Ok(operand1 - operand2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(string num1, string num2)
        {
            var validationResult1 = ValidateInput(nameof(num1), num1);
            var validationResult2 = ValidateInput(nameof(num2), num2);

            if (validationResult1 != null) return validationResult1;
            if (validationResult2 != null) return validationResult2;

            try
            {
                long operand1 = long.Parse(num1);
                long operand2 = long.Parse(num2);

                long result = operand1 * operand2;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(string num1, string num2)
        {
            var validationResult1 = ValidateInput(nameof(num1), num1);
            var validationResult2 = ValidateInput(nameof(num2), num2);

            if (validationResult1 != null) return validationResult1;
            if (validationResult2 != null) return validationResult2;

            try
            {
                int operand1 = int.Parse(num1);
                int operand2 = int.Parse(num2);

                if (operand2 == 0)
                {
                    return BadRequest("Division by zero is not allowed.");
                }

                return Ok(operand1 / operand2);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet("factorial")]
        public IActionResult Factorial(string n)
        {
            var validationResult = ValidateInput(nameof(n), n);

            if (validationResult != null) return validationResult;

            try
            {
                int number = int.Parse(n);

                long result = 1;
                for (int i = 2; i <= number; i++)
                {
                    result *= i;
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }
    }
}

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
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0 || num2 == 0) return BadRequest("Num1 and Num2 are required and cannot be zero");
            
            try
            {
                checked
                {
                    return Ok(num1 + num2);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred");
            }
            catch
            {
                return StatusCode(500, "An internal error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0 || num2 == 0) return BadRequest("Num1 and Num2 are required and cannot be zero");
            
            try
            {
                return Ok(num1 - num2);
            }
            catch
            {
                return StatusCode(500, "An internal exception occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
           if(num1 == 0 || num2 == 0){
             return BadRequest( "Ensure Both valid integers")    
           }
            
            try
            {
                checked
                {
                    long result = (long)num1 * (long)num2;
                    return Ok(result);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide(int num1, int num2)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (num1 == 0 || num2 == 0) return BadRequest("Num1 and Num2 are required and cannot be zero");

            try
            {
                return Ok(num1 / num2);
            }
            catch (DivideByZeroException)
            {
                return BadRequest("Division by zero is not allowed");
            }
            catch
            {
                return StatusCode(500, "An internal error occurred");
            }
        }
        
        [HttpGet("factorial")]
        public IActionResult Factorial(int n)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            if (n < 0) return BadRequest("Negative numbers are not allowed");
            if (n <= 1)
                return Ok(1);

            try
            {
                checked
                {
                    return Ok(n * Factorial(n - 1).Value);
                }
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow occurred");
            }
        }
    }
}

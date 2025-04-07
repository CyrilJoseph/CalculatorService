using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] OperationRequest request)
        {
            if (request == null || !ModelState.IsValid) return BadRequest("Invalid input");

            try
            {
                return Ok(request.Num1 + request.Num2);
            }
            catch
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpPost("sub")]
        public IActionResult Sub([FromBody] OperationRequest request)
        {
            if (request == null || !ModelState.IsValid) return BadRequest("Invalid input");

            return Ok(request.Num1 - request.Num2);
        }

        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] OperationRequest request)
        {
            if (request == null || !ModelState.IsValid) return BadRequest("Invalid input");

            try
            {
                long result = (long)request.Num1 * (long)request.Num2;
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow detected");
            }
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] OperationRequest request)
        {
            if (request == null || !ModelState.IsValid) return BadRequest("Invalid input");
            if (request.Num2 == 0) return BadRequest("Division by zero is not allowed");

            return Ok(request.Num1 / request.Num2);
        }

        [HttpPost("factorial")]
        public IActionResult Factorial([FromBody] FactorialRequest request)
        {
            if (request == null || !ModelState.IsValid) return BadRequest("Invalid input");
            if (request.Number < 0) return BadRequest("Negative numbers are not allowed");

            try
            {
                long result = 1;
                for (int i = 1; i <= request.Number; i++)
                {
                    result *= i;
                }
                return Ok(result);
            }
            catch (OverflowException)
            {
                return BadRequest("Overflow detected during factorial calculation");
            }
        }

        public class OperationRequest
        {
            public int Num1 { get; set; }
            public int Num2 { get; set; }
        }

        public class FactorialRequest
        {
            public int Number { get; set; }
        }
    }
}
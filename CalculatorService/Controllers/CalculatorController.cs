using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;

namespace CalculatorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;
        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("add")]
        public IActionResult Add([FromQuery] int? num1, [FromQuery] int? num2)
        {
            if (num1 == null) return BadRequest("Num1 is required");
            if (num2 == null) return BadRequest("Num2 is required");
            try
            {
                checked
                {
                    return Ok(num1.Value + num2.Value);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in Add operation");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("sub")]
        public IActionResult Sub([FromQuery] int? num1, [FromQuery] int? num2)
        {
            if (num1 == null) return BadRequest("Num1 is required");
            if (num2 == null) return BadRequest("Num2 is required");
            try
            {
                checked
                {
                    return Ok(num1.Value - num2.Value);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in Sub operation");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("multiply")]
        public IActionResult Multiply([FromQuery] int? num1, [FromQuery] int? num2)
        {
            if (num1 == null) return BadRequest("Num1 is required");
            if (num2 == null) return BadRequest("Num2 is required");
            try
            {
                checked
                {
                    long result = (long)num1.Value * (long)num2.Value;
                    return Ok(result);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in Multiply operation");
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("divide")]
        public IActionResult Divide([FromQuery] int? num1, [FromQuery] int? num2)
        {
            if (num1 == null) return BadRequest("Num1 is required");
            if (num2 == null) return BadRequest("Num2 is required and cannot be zero");
            if (num2 == 0) return BadRequest("Division by zero is not allowed");
            try
            {
                checked
                {
                    return Ok((double)num1.Value / num2.Value);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception in Divide operation");
                return StatusCode(500, "An error occurred");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using Core.DTOs;
using Engines;
using Configs;
using Factories;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly ILogger<PaymentsController> _logger;
    private readonly IFraudRuleEngine _fraudRuleEngine;
    private readonly IPaymentRuleFactory _ruleFactory;

public PaymentsController(
    ILogger<PaymentsController> logger,
    IFraudRuleEngine fraudRuleEngine,
    IPaymentRuleFactory ruleFactory)
{
    _logger = logger;
    _fraudRuleEngine = fraudRuleEngine;
    _ruleFactory = ruleFactory;
}


    [HttpPost("process")]
    public IActionResult ProcessPayment([FromBody] PaymentDto payment)
    {
        if (payment == null)
        {
            _logger.LogWarning("Received null payment.");
            return BadRequest("Payment cannot be null.");
        }

        var paymentType = payment.GetType().Name;
        _logger.LogInformation("Processing payment of type {PaymentType}", paymentType);

        // Assign fraud flags based on config
        var enriched = _ruleFactory.AssignRules(payment);
        var results = _fraudRuleEngine.RunRules(enriched);


        var allPassed = results.All(r => r.Passed);

        _logger.LogInformation("Fraud check result for {PaymentType}: {Result}", paymentType, allPassed ? "Passed" : "Failed");

         // Run the fraud rules using the FraudRuleEngine
        var result = _fraudRuleEngine.RunRules(payment);

        if (allPassed)
        {
            _logger.LogInformation("Payment passed fraud checks.");
            return Ok(new
            {
                Message = "Payment failed fraud checks.",
                PaymentType = paymentType,
                Passed = allPassed,
                RuleResults = results
            });
        }
        else
        {
            _logger.LogWarning("Payment failed fraud checks.");
            return BadRequest(new
            {
                Message = "Payment failed fraud checks.",
                PaymentType = paymentType,
                Passed = allPassed,
                RuleResults = results
            });
        }
    }
}

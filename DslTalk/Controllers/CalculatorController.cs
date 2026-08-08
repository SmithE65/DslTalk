using Calculator;
using DslTalk.Models;
using Microsoft.AspNetCore.Mvc;

namespace DslTalk.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    [HttpPost("interpret")]
    public IActionResult Interpret([FromBody] InterpretRequestDto request)
    {
        var scanner = new Scanner(request.DslText);
        var tokens = scanner.Scan();
        var parser = new Parser(tokens);
        var expression = parser.Parse();
        var interpreter = new Interpreter(expression);
        var result = interpreter.Interpret();

        return Ok(new InterpretResultDto(
            DslText: request.DslText,
            InputText: request.Input,
            Tokens: [.. tokens.Select(ToTokenDto)],
            Ast: ToAstNodeDto(expression),
            ResultText: result.ToString(),
            Diagnostic: null));
    }

    private static TokenDto ToTokenDto(Token token)
    {
        return new TokenDto(token.Type.ToString(), token.GetLexeme(), null, token.Start, token.Length);
    }

    private static AstNodeDto? ToAstNodeDto(Expression expression)
    {
        return expression switch
        {
            BinaryExpression binary => new AstNodeDto(
                Kind: "BinaryExpression",
                Label: binary.Token.GetLexeme(),
                Properties: [],
                Children: [ToAstNodeDto(binary.Left), ToAstNodeDto(binary.Right)]),
            LiteralExpression literal => new AstNodeDto(
                Kind: "LiteralExpression",
                Label: null,
                Properties: [new AspPropertyNode("Value", literal.Value.ToString())],
                Children: []),
            GroupingExpression grouping => new AstNodeDto(
                Kind: "GroupingExpression",
                Label: null,
                Properties: [],
                Children: [ToAstNodeDto(grouping.Expression)]),
            _ => null
        };
    }
}

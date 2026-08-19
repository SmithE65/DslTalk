using Calculator;
using DslTalk.Models;
using Microsoft.AspNetCore.Mvc;

namespace DslTalk.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CalculatorController : ControllerBase
{
    [HttpPost]
    public IActionResult Interpret([FromBody] InterpretRequestDto request)
    {
        var resultDto = new InterpretResultDto(
            DslText: request.DslText,
            InputText: request.Input,
            Tokens: [],
            Ast: null,
            ResultText: null,
            Diagnostic: null);

        IEnumerable<TokenDto> tokenDtos = [];
        AstNodeDto? astNodeDto = null;
        double? result = null;

        try
        {
            var scanner = new Scanner(request.DslText);
            var tokens = scanner.Scan();
            tokenDtos = tokens.Select(ToTokenDto);
            var parser = new Parser(tokens);
            var expression = parser.Parse();
            astNodeDto = ToAstNodeDto(expression);
            var interpreter = new Interpreter(expression);
            result = interpreter.Interpret();
        }
        catch (ScanException scanEx)
        {
            var diag = new DiagnosticDto(
                Phase: "Scanning",
                Message: scanEx.Message);

            resultDto = resultDto with { Diagnostic = diag };
        }

        resultDto = resultDto with
        {
            Tokens = [.. tokenDtos],
            Ast = astNodeDto,
            ResultText = result?.ToString(),
        };

        return Ok(resultDto);
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
            UnaryExpression unary => new AstNodeDto(
                Kind: "UnaryExpression",
                Label: unary.Token.GetLexeme(),
                Properties: [],
                Children: [ToAstNodeDto(unary.Right)]),
            _ => null
        };
    }
}

using DslTalk.Models.Slides;
using Microsoft.AspNetCore.Mvc;

namespace DslTalk.Controllers;

[Route("slides")]
public class SlidesController : Controller
{
    private readonly SlideDefinition[] _deck;

    public SlidesController()
    {
        var slides = new List<(string ViewName, string Title)>
        {
            ("Title", "DSL Talk"),
            ("AboutMe", "About Me"),
            ("Agenda", "Agenda"),
            ("Motivation", "Motivation for this Talk"),
            ("Thesis", "In Short..."),
            ("DslDefinition", "DSL Definition"),
            ("FamiliarDsls", "Familiar DSLs"),
            ("CalculatorLanguage", "Calculator Language"),
            ("ScannerAnimation", "Scanner Animation"),
            ("ParserAnimation", "Parser Animation"),
            ("CalculatorDemo", "Calculator Demo")
        };

        _deck = [.. slides.Select((s, i) => new SlideDefinition(i + 1, s.ViewName, s.Title))];
    }

    [HttpGet("{pageNumber:int:min(1)}")]
    public IActionResult GetSlide(int pageNumber)
    {
        var index = pageNumber - 1;

        if (index >= _deck.Length)
        {
            return NotFound();
        }

        var slide = _deck[index];

        var vm = new SlideViewModel(_deck, index);
        return View(slide.ViewName, vm);
    }
}
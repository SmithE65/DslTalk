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
            ("SectionWhyDsls", ("Why DSLs?")), // Title slide
            ("Motivation", "Motivation for this Talk"),
            ("Thesis", "In Short..."),
            ("DslDefinition", "DSL Definition"),
            ("FamiliarDsls", "Familiar DSLs"),
            ("SectionInterpreter", "Anatomy of an Interpreter"), // Title slide
            ("ScanParseInterpret", "Parts of an Interpreter"),
            ("CalculatorLanguage", "Calculator Language"),
            ("BNF", "BNF"),
            ("ScannerAnimation", "Scanner Animation"),
            ("ParserAnimation", "Parser Animation"),
            ("CalculatorDemo", "Calculator Demo"),
            ("SectionAccidentalDsls", "You May Already Have a Language"), // Title slide
            ("FamiliarDslsRevisited", "Familiar DSLs Revisited"),
            ("AccidentalDsl", "Accidental DSL"),
            ("DontReinventTheWheel", "Don't Reinvent the Wheel"),
            ("DontReinventTheWheel2", "Don't Reinvent the Wheel"),
            ("SectionCreatingADsl", "Creating A DSL"), // Title slide
            ("WhereToStart", "Where To Start"),
            ("Summary", "Summary")
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
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
            ("BNF", "EBNF"),
            ("ScannerAnimation", "Scanner Animation"),
            ("ParserAnimation", "Parser Animation"),
            ("Diagnostics", "Diagnostics"),
            ("CalculatorDemo", "Calculator Demo"),
            ("SectionAccidentalDsls", "Languages Hiding in Plain Sight"), // Title slide
            ("FamiliarDslsRevisited", "Familiar DSLs Revisited"),
            ("AccidentalDsl", "Accidental DSL"),
            ("DontReinventTheWheel", "Don't Reinvent the Wheel"),
            ("DontReinventTheWheel2", "We Like Reinventing"),
            ("SectionCreatingADsl", "Creating A DSL"), // Title slide
            ("WhereToStart", "Where To Start"),
            ("KnowYourUsers", "Know Your Users"),
            ("Grammar", "Make Grammar Boring"),
            ("Types", "Types Exist"),
            ("SchemaShiftTypes", "Schema Shift Types"),
            ("ScopeCreep", "Language Evolution"),
            ("Summary", "Summary"),
            ("SearchDemo", "Search Demo")
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
using DslTalk.Models.Slides;
using Microsoft.AspNetCore.Mvc;

namespace DslTalk.Controllers;

[Route("slides")]
public class SlidesController : Controller
{
    private readonly SlideDefinition[] _deck =
    [
        new(1, "Title", "DSL Talk"),
        new(2, "AboutMe", "About Me")
    ];

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
namespace DslTalk.Models.Slides;

public sealed record SlideDefinition(
    int Number,
    string ViewName,
    string Title);

public sealed record SlideViewModel(
    IReadOnlyList<SlideDefinition> Slides,
    int CurrentIndex)
{
    public SlideDefinition CurrentSlide => Slides[CurrentIndex];

    public SlideDefinition? PreviousSlide => CurrentIndex > 0
        ? Slides[CurrentIndex - 1]
        : null;

    public SlideDefinition? NextSlide => CurrentIndex < Slides.Count - 1
        ? Slides[CurrentIndex + 1]
        : null;
}
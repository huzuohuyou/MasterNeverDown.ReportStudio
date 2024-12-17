using ICSharpCode.AvalonEdit.Rendering;

namespace AakStudio.Shell.UI.Showcase.Editors;

public class TruncateLongLines : VisualLineElementGenerator
{
    const int maxLength = 200;
    const string ellipsis = "...";
    const int charactersAfterEllipsis = 100;

    public override int GetFirstInterestedOffset(int startOffset)
    {
        var line = CurrentContext.VisualLine.LastDocumentLine;
        if (line.Length > maxLength) {
            var ellipsisOffset = line.Offset + maxLength - charactersAfterEllipsis - ellipsis.Length;
            if (startOffset <= ellipsisOffset)
                return ellipsisOffset;
        }
        return -1;
    }

    public override VisualLineElement ConstructElement(int offset)
    {
        return new FormattedTextElement(ellipsis, CurrentContext.VisualLine.LastDocumentLine.EndOffset - offset - charactersAfterEllipsis);
    }
}
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

namespace BlazorBindings.Core;

/// <summary>
/// Helper class for types that accept inline text spans. This type collects text spans
/// and returns the string represented by the contained text spans.
/// </summary>
public class TextSpanContainer
{
    private readonly List<string> _textSpans = new();

    public TextSpanContainer(bool trimWhitespace = true)
    {
        TrimWhitespace = trimWhitespace;
    }

    public bool TrimWhitespace { get; }

    /// <summary>
    /// Updates the text spans with the new text at the new index and returns the new
    /// string represented by the contained text spans.
    /// </summary>
    /// <param name="isAdd">If text should be added or replaced</param>
    /// <param name="index">the index of the string within a group of text strings.</param>
    /// <param name="text">The text to handle. This text may contain whitespace at the start and end of the string.</param>
    /// <returns></returns>
    public string GetUpdatedText(bool isAdd, int index, string text)
    {
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        if ((isAdd && index > _textSpans.Count) ||
            (!isAdd && index >= _textSpans.Count))
        {
            // Expand the list to allow for the new text's index to exist
            _textSpans.AddRange(new string[index - _textSpans.Count]);
        }

        if (text is not null)
        {
            if (isAdd)
            {
                _textSpans.Insert(index, text);
            }
            else
            {
                _textSpans[index] = text;
            }
        }
        else
        {
            // null means "remove text"

            _textSpans.RemoveAt(index);
        }

        var allText = string.Concat(_textSpans);
        return TrimWhitespace
            ? allText.Trim()
            : allText;
    }
}

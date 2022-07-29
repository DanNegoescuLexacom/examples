using System;

namespace copilot
{
    public class Dictation
    {
        public long DurationMilliseconds { get; set; }
        public string Text { get; set; }
        public string Language { get; set; }
        public string Locale { get; set; }

        // method to clone a Dictation
        public Dictation Clone()
        {
            return new Dictation
            {
                DurationMilliseconds = this.DurationMilliseconds,
                Text = this.Text,
                Language = this.Language,
                Locale = this.Locale
            };
        }

        // method to emit durationmilliseconds as a TimeSpan
        public TimeSpan Duration()
        {
            return TimeSpan.FromMilliseconds(this.DurationMilliseconds);
        }

        // method to emit the text in the correct language
        public string TextInLanguage(string language)
        {
            if (this.Language == language)
            {
                return this.Text;
            }
            else
            {
                return "";
            }
        }
    }
}
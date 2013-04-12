using System;
using System.Linq;

namespace ComposableRegex.Controllers.RegexWorkers
{
    public class Regexer
    {
        public String Regex { get; private set; }

        public Regexer(string regex, bool includesComments = true)
        {
            if (String.IsNullOrEmpty(regex))
            {
                throw new ArgumentNullException("regex", "Regex cannot be null");
            }
            Parse(regex, includesComments);
        }

        private void Parse(string regex, bool includeComments)
        {
            var splits = regex.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Count() == 1)
            {
                Regex = splits.First();
                return;
            }

            var groups = (from item in splits.Take(splits.Count() - 1)
                          let keyValueSplit = item.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries)
                          let key = keyValueSplit.First().Trim()
                          let value = keyValueSplit.Last().Trim()
                          where !String.IsNullOrEmpty(key)
                          where !String.IsNullOrEmpty(value)
                          let regexWithoutComments = includeComments ? value.Split(new[] { "##" }, 2, StringSplitOptions.RemoveEmptyEntries).First().Trim() : value
                          select new { Key = key, Value = regexWithoutComments }).ToList();

            var final = splits.Last().Trim();

            Regex = final;

            for (int i = groups.Count - 1; i >= 0; i--)
            {
                var item = groups[i];
                Regex = Regex.Replace(item.Key, item.Value);
            }
        }
    }
}
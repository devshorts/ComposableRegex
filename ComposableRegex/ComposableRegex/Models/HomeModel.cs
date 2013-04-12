using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComposableRegex.Models
{
    public class HomeModel
    {
        [Display(Name = "Composable Regex")]
        public string ComposableRegex { get; set; }

        [Display(Name = "Source Text")]
        public string TargetText { get; set; }

        [Display(Name = "Transformed Regex")]
        public string TransformedRegex { get; set; }

        public Boolean IsMatch { get; set; }

        public List<String> Trace { get; set; }
    }
}
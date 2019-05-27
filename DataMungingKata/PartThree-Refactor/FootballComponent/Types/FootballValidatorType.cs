﻿using System.Collections.Generic;

namespace FootballComponentV2.Types
{
    public class FootballValidatorType
    {
        public Football Football { get; set; }
        public bool IsValid { get; set; }
        public List<string> ErrorList { get; }

        public FootballValidatorType()
        {
            Football = new Football();
            IsValid = false;
            ErrorList = new List<string>();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Voat.Utilities;

namespace Voat.Voting.Outcomes
{
    public abstract class ModeratorOutcome : VoteOutcome, ISubverse
    {
        [Required]
        [RegularExpression(CONSTANTS.USER_NAME_REGEX, ErrorMessage = "User name does not appear valid")]
        public string UserName { get; set; }

        [Required]
        public string Subverse { get; set; }

    }
}

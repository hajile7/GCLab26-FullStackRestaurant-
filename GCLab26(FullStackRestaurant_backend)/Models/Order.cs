﻿using System;
using System.Collections.Generic;

namespace GCLab26_FullStackRestaurant_backend_.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string Restaurant { get; set; } = null!;

    public int? Rating { get; set; }

    public bool OrderAgain { get; set; }
}

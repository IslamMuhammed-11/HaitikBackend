using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaitikBackend.Domain.ValueObjects;

public sealed record GeoLocation(Point CurrentLocation);

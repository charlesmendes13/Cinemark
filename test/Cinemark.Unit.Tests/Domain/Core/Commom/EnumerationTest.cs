﻿using Cinemark.Domain.Core.Commom;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Domain.Core.Commom
{
    public class EnumerationTest
    {
        [Fact]
        public void Enumeration()
        {
            var enumeration = new Mock<Enumeration>(1, "Enum");

            enumeration.Object.Id.Should().Be(1);
            enumeration.Object.Name.Should().Be("Enum");
        }
    }
}

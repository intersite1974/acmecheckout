## Shopping Cart / Checkout Kata

Repository for a sample checkout / shopping cart kata.

Constructed using:

- Microsoft Visual Studio 2019
- [Microsoft ASP.NET Core 3.1](https://www.microsoft.com/net/core#windows)
- [XUnit](https://xunit.github.io/)
- [NSubstitute](http://nsubstitute.github.io/)
- [Fluent Assertions](http://www.fluentassertions.com/)

Lots to add if this was to be productionised.. e.g.

- Discount calculation algorithms could be via a Strategy Pattern and selected as appropriate (validation included)
- Decorator pattern could be used to restructure things to allow for furture amendements and add-ins as a business grows - price is a classic, more as things progress
- Ardalis Guard Clauses could protect methods instead of the simplistic checks currently present

# Calculator application

## How to use

Run _htncalc.exe_ and input expressions for calculation. For exit input "q" or "quit" command.

## Supported features

* Integer and decimal positive numbers
* Arithmetic operations `+`, `-`, `*`, `/`
* Brackets
* `abs()` function

## How it works

1. `ExpressionParser` performs lexical analysis of an input string expression.
1. `PermutationsBuilder` builds a terms tree considering sub-expressions priority with different "permutations services" which implements `ITermPermuter` interface. The order is important:
    1. `BracketTermPermuter` - distinguishes brackets
    1. `FunctionTermPermuter` - works with functions like `abs(...)`
    1. `MulDivTermPermuter` - for multiplication and division operations
    1. `AddSubTermPermuter` - for addition and subtraction operations
1. `ExpressionTreeBuilder` builds an expression tree, consists of `IExpressionNode` elements which can calculate their own value.

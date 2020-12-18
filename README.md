# PietSharp

C# implementation of the Piet esoteric programming language. The original specifications for Piet can be found [here](https://www.dangermouse.net/esoteric/piet.html).

## Blazor client

You can try PietSharp in your browser [here](https://matthewmooreza.github.io/PietSharp/) - powered by client-side Blazor.

I've tried to stay to true to the specifications, while allowing for future extensions.

## Interpretations

### Non-standard colours

Any colours in the input image which are not one of the 20 standard colours in Piet are treated as if they are white.

### Stack pops

If an operation (add for example) requires to items on the stack to make sense the stack depth will be evaluated before the pops are attempted.  If there are insufficient items on the stack to complete the operation it is treated as a noop.  As such if there is only one item on the stack and the operation requires two items, the one item will remain on the stack.

### Error handling

In the event of an error Piet will ignore the error and proceed with the next operation.  If any items were popped off the stack in the process these will not be pushed back.  For example if a divide by zero would occur we would pop the two inputs off the stack, but effectively a noop will be processed.

## Cycle detection

Currently there is no cycle detection implemented.  Infite loops are handled through a crude max operations limit.  However something more sophosticated is on the todo list.

## Codel size detection

Piet has the concept of a codel, which is basically an NxN square of pixels.  This allows for the creation of larger images without blowing up the size of inputs for push operations.  PietSharp currently scans each row in the input image to estimate the minimum codel size.  


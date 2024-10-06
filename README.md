Module 1 : 
Exercises:
#1 - Leap year
Write a function that returns true or false depending on whether its input integer is a leap year or not. A leap year is defined as one that is divisible by 4, but is not otherwise divisible by 100 unless it is also divisible by 400. For example, 2001 is a typical common year and 1996 is a typical leap year, whereas 1900 is an atypical common year and 2000 is an atypical leap year.

#2 Nth - Fibonacci
Write some code to generate the Fibonacci number for the nth position ex:
int Fibonacci(int position)
First Fibonacci numbers in the sequence are: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34

Module 2 :
String Calculator
1. Create a simple String calculator with a method int Add(string numbers)
2. The method can take 0, 1 or 2 numbers, and will return their sum (for an empty string it will return 0) for example “” or “1” or “1,2”
3. Allow the Add method to handle an unknown amount of numbers
4. Allow the Add method to handle new lines between numbers (instead of commas).
a. the following input is ok: “1\n2,3” (will equal 6)
b. the following input is NOT ok: “1,\n” (not need to prove it - just clarifying)
5. Support different delimiters
1. to change a delimiter, the beginning of the string will contain a separate line that looks like this: “//[delimiter]\n[numbers…]” for example “//;\n1;2” should return three where the default delimiter is ‘;’. The first line is optional. all existing scenarios should still be supported
2. Calling Add with a negative number will throw an exception “negatives not allowed” - and the negative that was passed.if there are multiple negatives, show all of them in the exception message

Module 3 : 
Change calculator
Modify the GetChange() function to accept for input a total cost and total paid that returns a sorted array of the bills and coins to give as change
Valid Change:
100 Dollar Bill
50 Dollar Bill
20 Dollar Bill
10 Dollar Bill
5 Dollar Bill
1 Dollar Bill
Half Dollar (50 cents)
Quarter (25 cents)
Dime (10 cents)
Nickel (5 cents)
Cent (1 cent)
Example:
$500 was given, to pay for $224.99 of bills
Expected change total: $225.01
Change Denominations: [100, 100, 50, 25, 0.01]
